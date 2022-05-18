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
    public partial class Lobby : Form
    {
        public string mode;
        public bool connected;

        public int startEnergy;
        public int startCash;
        public int startPeople;
        public int mapSize;
        public bool sameMap;

        public List<int> startBuildings;

        public Lobby()
        {
            InitializeComponent();
        }

        public void SetPlayerList()
        {
            Globals.mapGenerated = false;
            listBoxPlayers.Items.Clear();
            listBoxPlayers.Items.Add("Gracze : ");
            for (int i = 0; i < Globals.players.Count; i++)
            {
                listBoxPlayers.Items.Add(Globals.players[i].playerName);
            }
            if (Globals.players.Count > 1 && mode == "HOST")
            {
                buttonStart.Enabled = true;
            }
        }

        public void SetMode(string m, int startEn = 0, int startC = 0, int startPeo = 0, int mapS = 0, bool sameM = false, List<int> startB = null)
        {
            buttonStart.Enabled = false;
            mode = m;
            if(m == "HOST")
            {
                startEnergy = startEn;
                startCash = startC;
                startPeople = startPeo;
                mapSize = mapS;
                sameMap = sameM;
                startBuildings = startB;
                IPEndPoint localEndPoint = new IPEndPoint(Globals.localIp, Globals.port);
                Socket soc = new Socket(Globals.localIp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    soc.Bind(localEndPoint);
                    soc.Listen(5);
                    soc.BeginAccept(new AsyncCallback(Globals.AcceptCallback), soc); 
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        public void TryToConnect(string ip)
        {
            try
            {
                IPAddress ipAddr = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddr, Globals.port);
                Globals.socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Globals.socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), Globals.socket);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket s = (Socket)ar.AsyncState;
                s.EndConnect(ar);
                connected = true;
                List<string> list = new List<string>();
                list.Add(Globals.localPlayerName);
                Thread.Sleep(500);
                Globals.Receive();
                Globals.Send(ParserCreator.Compress(0, 0, list));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }  

        private void Lobby_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.FormClosed(this);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStart.Text = "Rozpoczynanie gry...";
            GenerateMapForPlayers();
            int serverSeed = Globals.GetLocalPlayer().mapSeed;
            foreach(Player p in Globals.players)
            {
                if (p.isLocal)
                {
                    p.cash = startCash;
                    p.energy = startEnergy;
                    p.people = startPeople;
                    p.peopleUsing = 0;
                    Globals.mapSize = mapSize;
                    p.map = GenerateMap();
                    p.mapForSpy = p.GenerateMapForSpy();
                    p.GenerateBuildings(startBuildings);
                    p.playerTurn = true;
                    continue;
                }
                p.map = GenerateMap(p.mapSeed);
                List<string> list = new List<string>();
                list.Add(startEnergy.ToString());
                list.Add(startCash.ToString());
                list.Add(startPeople.ToString());
                list.Add(mapSize.ToString());
                list.Add(p.mapSeed.ToString());
                list.Add(serverSeed.ToString());
                list.Add(Globals.spyCellShowedTurns.ToString());
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < startBuildings.Count; i++)
                {
                    if (i != startBuildings.Count - 1) str.Append(startBuildings[i] + "|");
                    else str.Append(startBuildings[i]);
                }
                list.Add(str.ToString());
                Globals.Send(ParserCreator.Compress(Globals.GetLocalPlayer().playerId, 2, list));
            }
            Globals.playerIdTurn = Globals.GetLocalPlayer().playerId;
            Game g = new Game();
            Globals.gameWnd = g;
            Globals.ChangeWnd(this, Globals.gameWnd);
            Globals.gameWnd.BeginTurn(true, "");
            Globals.gameWnd.DrawMap(true);
            Globals.gameWnd.SetAtCenter(true);
            Globals.gameWnd.CalculateZoom();
            Globals.gameWnd.SetZoomAuto();
        }

        public void StartGame()
        {
            Globals.ChangeWnd(this, Globals.gameWnd);
            Globals.gameWnd.ChangeTurn();
            Globals.gameWnd.EndTurn(true);
            Globals.gameWnd.DrawMap(true);
            Globals.gameWnd.SetAtCenter(true);
            Globals.gameWnd.CalculateZoom();
            Globals.gameWnd.SetZoomAuto();
        }

        private void GenerateMapForPlayers()
        {
            if(sameMap)
            {
                int mapSeed = Globals.rand.Next(1000,9999);
                for (int i = 0; i < Globals.players.Count; i++)
                {
                    Globals.players[i].mapSeed = mapSeed;
                }
            }
            else
            {
                for(int i = 0; i < Globals.players.Count; i++)
                {
                    int mapSeed = Globals.rand.Next(1000, 9999);
                    Globals.players[i].mapSeed = mapSeed;
                }
            }
        }

        public float[,] GenerateMap(int seed = 0)
        {
            Random newRand = new Random(seed == 0 ? Globals.GetLocalPlayer().mapSeed : seed);
            float[,] map1 = MapGenerator.Generate(Globals.mapSize, Globals.mapSize, newRand.Next(), 1, 5);
            float[,] map2 = MapGenerator.Generate(Globals.mapSize, Globals.mapSize, newRand.Next(), 1, 5);

            for (int x = 0; x < Globals.mapSize; ++x)
                for (int y = 0; y < Globals.mapSize; ++y)
                {
                    map1[x, y] = (map1[x, y] + map2[x, y]);
                    if (map1[x, y] > 1f)
                        map1[x, y] = 1f;
                }
            Globals.mapGenerated = true;
            return map1;
        }
    }
}
