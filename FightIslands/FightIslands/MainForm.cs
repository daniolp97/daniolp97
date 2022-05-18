using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightIslands
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Performances.Initilize();
            Globals.Initialize(this);
            SetStartBuildings();
            panelHost.Visible = false;
            panelJoin.Visible = false;
            this.Size = new Size(324, 125);
        }

        private void SetStartBuildings()
        {
            for (int i = 0; i < Globals.gameData.buildings.Count; i++)
            {
                Building b = Globals.gameData.buildings[i];
                checkedListBoxStartBuildings.Items.Add(b.buildingName);
                if (b.type == BuildingTypes.Wioska) checkedListBoxStartBuildings.SetItemChecked(i, true);
                if (b.type == BuildingTypes.TransportLadowy) checkedListBoxStartBuildings.SetItemChecked(i, true);
            }
        }

        private void ButtonTypeClick(object sender, EventArgs e)
        {
            Button clicked = ((Button)sender);
            if(clicked == buttonHostGame) ChangeWindow("HOST");
            else ChangeWindow("JOIN");
        }

        public void ChangeWindow(string type)
        {
            panelHost.Visible = type == "HOST";
            panelJoin.Visible = type == "JOIN";
            if(type == "HOST")
            {
                GetIP();
                panelHost.Location = new System.Drawing.Point(12, 80);
                this.Size = new Size(324, 599);
            }
            else
            {
                panelJoin.Location = new System.Drawing.Point(12, 80);
                this.Size = new Size(324, 268);
            }
        }

        public void GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    labelYourIP.Text = ip.ToString();
                    break;
                }
            }
        }

        private bool SprawdzDane(string type)
        {
            if(type == "JOIN")
            {
                IPAddress ipAdr;
                if(!IPAddress.TryParse(textBoxGameIP.Text, out ipAdr))
                {
                    MessageBox.Show("Błędny adres gry!");
                    return false;
                }
                if (textBoxYourNickJoin.Text.Length > -1)
                {
                    if(textBoxYourNickJoin.Text.Length < 3)
                    {
                        MessageBox.Show("Nazwa gracza jest za krótka! Minimum 3 znaki!");
                        return false;
                    }
                    for(int i = 0; i < textBoxYourNickJoin.Text.Length; i++)
                    {
                        if(!Char.IsLetter(textBoxYourNickJoin.Text[0]))
                        {
                            MessageBox.Show("Nazwa gracza może zawierać tylko litery!");
                            return false;
                        }
                    }
                }
            }
            else if(type == "HOST")
            {
                if (textBoxYourNickHost.Text.Length > -1)
                {
                    if (textBoxYourNickHost.Text.Length < 3)
                    {
                        MessageBox.Show("Nazwa gracza jest za krótka! Minimum 3 znaki!");
                        return false;
                    }
                    for (int i = 0; i < textBoxYourNickHost.Text.Length; i++)
                    {
                        if (!Char.IsLetter(textBoxYourNickHost.Text[0]))
                        {
                            MessageBox.Show("Nazwa gracza może zawierać tylko litery!");
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (!SprawdzDane("HOST")) return;
            Globals.localIp = IPAddress.Parse(labelYourIP.Text);
            Globals.localPlayerName = textBoxYourNickHost.Text;
            Globals.spyCellShowedTurns = numericUpDownTurnSpyCells.Value.ToInt();
            Player p = new Player();
            p.playerName = textBoxYourNickHost.Text;
            p.playerId = Globals.rand.Next(1000, 9999);
            p.isLocal = true;
            Globals.players.Add(p);
            Lobby lobby = new Lobby();
            lobby.Text = "Lobby (Host)";
            List<int> startBuildings = new List<int>();
            for (int i = 0; i < checkedListBoxStartBuildings.Items.Count; i++)
            {
                if (checkedListBoxStartBuildings.GetItemCheckState(i) != CheckState.Checked) continue;
                foreach(Building b in Globals.gameData.buildings)
                {
                    if (b.buildingName != checkedListBoxStartBuildings.Items[i].ToString()) continue;
                    startBuildings.Add(b.buildingIndex);
                }
            }
            lobby.SetMode("HOST", nudEnergy.Value.ToInt(), nudCash.Value.ToInt(), nudPeople.Value.ToInt(), nudSize.Value.ToInt(), checkBoxSameMap.Checked, startBuildings);
            Globals.lobbyWnd = lobby;
            Globals.ChangeWnd(this, Globals.lobbyWnd);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!SprawdzDane("JOIN")) return;
            Globals.localPlayerName = textBoxYourNickJoin.Text;
            Lobby lobby = new Lobby();
            Globals.lobbyWnd = lobby;
            lobby.Text = "Lobby (Join)";
            Globals.ChangeWnd(this, Globals.lobbyWnd);
            lobby.TryToConnect(textBoxGameIP.Text);
            Thread.Sleep(2000);
            if(!lobby.connected)
            {
                lobby.Close();
                lobby.Dispose();
                MessageBox.Show("Nie udało się połączyć!");
            }
        }

        private void buttonSingle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tu jeszcze nic nie ma");
        }
    }
}
