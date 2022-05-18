using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightIslands
{
    public partial class Game : Form
    {
        public int cellSize = 5;
        public bool movingMap = false;
        public int mouseX = 0;
        public int mouseY = 0;

        public System.Drawing.Point pictureBoxPoint;

        public int zoomPower = 0;
        public Graphics g;
        public Bitmap mapBitmap;

        List<CellsMap> cells;

        List<CheckBox> buildButtons;
        List<CheckBox> weaponButtons;

        Point cellMouseDown;
        Point selectedCell;

        Point cellForTooltip;
        int timerInterval = 0;
        bool mapTooltipShowed = false;
        bool mouseOverMap = false;

        int panelShowedIndex;
        int buildingBuildIndexSelected;
        int weaponSelectedIndex;
        bool blockChecking = false;
        bool blockCheckingWeapon = false;

        bool attackMode = false;

        int animFrame = 0;
        int animFrameCount = 0;

        Graphics animExplGraph;
        Bitmap animPartBitmap;
        public bool localTurn;

        List<BuildingCellSpy> everSpyPoints;

        public Game()
        {
            buildingBuildIndexSelected = 0;
            InitializeComponent();
            this.Size = new Size(900, 750);
            cellMouseDown = new Point(0, 0);
            selectedCell = new Point(0, 0);
            buildButtons = new List<CheckBox>();
            weaponButtons = new List<CheckBox>();
            everSpyPoints = new List<BuildingCellSpy>();
            cells = new List<CellsMap>();
            zoomPower = cellSize;
            cellForTooltip = new Point(-1, -1);
            AddBuildingsToPanel();
            panelBuildings.HorizontalScroll.Visible = false;
            AddWeaponsToPanel();
            pictureBoxPoint = new System.Drawing.Point(0, 0);
            ShowRightPanel("I");
            CountResources();
        }

        public void ChangeTurn()
        {
            foreach(Player p in Globals.players)
            {
                if(p.playerTurn)
                {
                    localTurn = p.isLocal;
                    break;
                }
            }
        }

        public void CountResources()
        {
            Player p = Globals.GetLocalPlayer();
            labelEnergy.Text = "Energia : " + p.energy;
            labelCash.Text = "Fundusze : " + p.cash;
            labelPeople.Text = "Ludzie : " + p.peopleUsing + " / " + p.people;
            if(!attackMode && g != null) RedrawBuildingsForUpgrade();
        }


        private bool Between(float val, float from, float to)
        {
            if (val >= from && val < to) return true;
            return false;
        }

        private void ShowRightPanel(string tag)
        {
            panelBuildings.Visible = tag == "I";
            panelCellInfo.Visible = tag == "C";
            panelIslandInfo.Visible = tag == "I";
            panelWeapons.Visible = tag == "A";
            attackMode = false;
            panelPlayerColor.BackColor = tag == "A" ? Color.Red : Color.Lime;
            weaponSelectedIndex = 0;

            foreach (CheckBox ch in weaponButtons)
            {
                ch.Checked = false;
            }
            if(tag == "C") // pole z budynkiem
            {
                PlayerBuilding pb = Globals.GetLocalPlayer().GetBuildingAtPoint(selectedCell.X, selectedCell.Y);
                Building b = Globals.gameData.GetBuildingBy(pb.buildingIndex, "I");
                StringBuilder str = new StringBuilder();
                string helpStr = "";
                buttonUpgradeBuilding.BackColor = SystemColors.Control;
                if (pb.isDestroyed)
                {
                    str.AppendLine("Budynek : (Poziom " + pb.buildingLevel + ")");
                    str.AppendLine(b.buildingName);
                    str.AppendLine("");
                    str.AppendLine("Zniszczony");
                    labelInfoCell.Text = str.ToString();
                }
                else
                {
                    str.AppendLine("Budynek : (Poziom " + pb.buildingLevel + ")");
                    str.AppendLine(b.buildingName);
                    str.AppendLine("");
                    if (b.weaponCreator == BuildingWeapon.NotCreator)
                    {
                        switch (pb.groundLevel)
                        {
                            case -1: helpStr = "Niska"; break;
                            case 0: helpStr = "Normalna"; break;
                            case 1: helpStr = "Dobra"; break;
                            default: helpStr = "Normalna"; break;
                        }
                        str.AppendLine("Wydajność : " + helpStr);
                        str.AppendLine("");
                    }
                    str.AppendLine("Koordynaty : " + pb.x + " x " + pb.y);
                    str.AppendLine("");
                    if (b.weaponCreator == BuildingWeapon.NotCreator)
                    {
                        str.AppendLine("Aktualna produkcja : ");
                        if (pb.isWorking)
                        {
                            str.AppendLine("Energia : " + pb.addEnergy);
                            str.AppendLine("Fundusze : " + pb.addCash);
                            str.AppendLine("Ludzie : " + pb.addPeople);
                        }
                        else
                        {
                            str.AppendLine("Produkjca wstrzymana : Budynek wyłączony");
                        }
                        str.AppendLine("");
                        str.AppendLine("Bazowa produkcja : ");
                        str.AppendLine("Energia : " + b.buildingAddEnergy);
                        str.AppendLine("Fundusze : " + b.buildingAddCash);
                        str.AppendLine("Ludzie : " + b.buildingAddPeople);
                    }
                    else
                    {
                        str.AppendLine("Aktualna produkcja : ");
                        if(pb.isWorking)
                        {
                            if (b.weaponCreation == WeaponType.Rakieta) str.AppendLine("Rakieta : 1");
                        }
                        else
                        {
                            str.AppendLine("Produkjca wstrzymana : Budynek wyłączony");
                        }
                        str.AppendLine("");
                    }
                    str.AppendLine("");
                    str.AppendLine("Pobór co turę : ");
                    str.AppendLine("Energia : " + b.energyPerTurn);
                    str.AppendLine("Fundusze : " + b.cashPerTurn);
                    labelInfoCell.Text = str.ToString();

                    str.Clear();
                    if (pb.buildingLevel < 3)
                    {
                        str.AppendLine("Ulepsz " + b.buildingName + " do poziomu " + (pb.buildingLevel + 1));
                        str.AppendLine("Ulepszenie zapewni wzrost produkji budynku");
                        str.AppendLine("");
                        str.AppendLine("Koszt ulepszenia : ");
                        str.AppendLine("Energia : " + pb.upgradeCostEnergy + " | Fundusze : " + pb.upgradeCostCash);
                        str.AppendLine("");
                        str.AppendLine("Aktualna produkcja : ");
                        str.AppendLine("Energia : " + pb.addEnergy + " | Fundusze : " + pb.addCash + " | Ludzie : " + pb.addPeople);
                        str.AppendLine("");
                        str.AppendLine("Po ulepszeniu : ");
                        str.AppendLine("Energia : " + pb.nextLevelAddEnergy + " | Fundusze : " + pb.nextLevelAddCash + " | Ludzie : " + pb.nextLevelAddPeople);
                    }
                    else if(pb.buildingLevel >= 3)
                    {
                        str.AppendLine("Osiągnięto maksymalny poziom budynku : " + b.buildingName);
                        str.AppendLine("Dalsze ulepszanie niedostępne");
                    }
                    if(!pb.isWorking)
                    {
                        str.AppendLine("Budynek " + b.buildingName + " jest aktualnie wyłączony");
                        str.AppendLine("Włącz budynek alby ulepszyć");
                    }
                    toolTip.SetToolTip(buttonUpgradeBuilding, str.ToString());

                    str.Clear();
                    str.AppendLine("Wyłącz działanie budynku");
                    str.AppendLine("Wyłącznie pozwoli na zminimalizowanie kosztów i późniejsze włączenie działania");
                    str.AppendLine("Brak kosztów wyłączenia");
                    str.AppendLine("");
                    str.AppendLine("Koszt późniejszego włączenia : ");
                    str.AppendLine("Energia : " + pb.turnOnCostEn + " | Fundusze : " + pb.turnOnCostCash + " | Ludzie : " + pb.turnOnCostPeo);
                    toolTip.SetToolTip(buttonTurnOffBuilding, str.ToString());

                    str.Clear();
                    str.AppendLine("Zniszcz budynek");
                    str.AppendLine("Zniszczenie budynku pozwoli na odzyskanie surowców z niepotrzebnych budynków");
                    str.AppendLine("");
                    str.AppendLine("Surowce odzyskane w przpadku zniszczenia");
                    str.AppendLine("Energia : " + pb.destroyAddEn + " | Fundusze : " + pb.destroyAddCash + " | Ludzie : " + pb.destroyAddPeo);
                    toolTip.SetToolTip(buttonDestroyBuilding, str.ToString());
                }

                panelShowedIndex = 4;

                if(pb.isDestroyed)
                {
                    buttonTurnOffBuilding.Enabled = false;
                    buttonUpgradeBuilding.Enabled = false;
                    buttonDestroyBuilding.Enabled = false;
                    return;
                }
                buttonTurnOffBuilding.Enabled = true;
                buttonDestroyBuilding.Enabled = true;

                buttonUpgradeBuilding.Enabled = pb.isWorking;
                if(pb.buildingLevel >= 3 || b.weaponCreator == BuildingWeapon.Creator)
                {
                    buttonUpgradeBuilding.Enabled = false;
                    buttonUpgradeBuilding.BackColor = Color.DimGray;
                }

                Player p = Globals.GetLocalPlayer();

                if (b.weaponCreator == BuildingWeapon.NotCreator && pb.buildingLevel < 3)
                {
                    if (p.cash >= pb.upgradeCostCash && p.energy >= pb.upgradeCostEnergy)
                        buttonUpgradeBuilding.BackColor = Globals.canColor;
                    else buttonUpgradeBuilding.BackColor = Globals.cantColor;
                }

                buttonUpgradeBuilding.ForeColor = Color.Black;
                buttonTurnOffBuilding.ForeColor = Color.Black;
                buttonDestroyBuilding.ForeColor = Color.Black;

                if (!localTurn)
                {
                    buttonUpgradeBuilding.BackColor = SystemColors.Control;
                    buttonUpgradeBuilding.ForeColor = Color.DimGray;
                    buttonTurnOffBuilding.BackColor = SystemColors.Control;
                    buttonTurnOffBuilding.ForeColor = Color.DimGray;
                    buttonDestroyBuilding.BackColor = SystemColors.Control;
                    buttonDestroyBuilding.ForeColor = Color.DimGray;
                }

                buttonTurnOffBuilding.Text = pb.isWorking ? "Wyłącz budynek" : "Włącz budynek";
            }
            else if(tag == "I") // info o wyspie
            {
                if (panelShowedIndex == 1) buttonInfo.Checked = true;
                panelShowedIndex = 1;
                string data = Globals.gameData.GetResourcesData();
                labelInfoResources.Text = data;

                Player p = Globals.GetLocalPlayer();
                foreach (CheckBox ch in buildButtons)
                {
                    Building b = Globals.gameData.GetBuildingBy(ch.Tag.ToString(), "T");
                    ch.ForeColor = Color.Black;
                    if (b.buildingCostCash > p.cash || b.buildingCostEnergy > p.energy || (b.buildingCostPeople > p.people - p.peopleUsing))
                    {
                        ch.BackColor = Globals.cantColor;
                        string desc = b.buildingDescription;
                        string toLow = "";
                        if (b.buildingCostEnergy > p.energy)
                        {
                            toLow += "Energii : " + (b.buildingCostEnergy - p.energy) + " | ";
                        }
                        if (b.buildingCostCash > p.cash)
                        {
                            toLow += "Funduszy : " + (b.buildingCostCash - p.cash) + " | ";
                        }
                        if (b.buildingCostPeople > 0)
                        {
                            if (b.buildingCostPeople > p.people - p.peopleUsing)
                            {
                                int peop = b.buildingCostPeople - (p.people - p.peopleUsing);
                                toLow += "Ludzi : " + peop + " | ";
                            }
                        }
                        string result = desc + Environment.NewLine +
                                        "BRAK ZASOBÓW DO BUDOWY!" + Environment.NewLine +
                                        "BRAKUJE : " + toLow.Substring(0, toLow.Length - 3);
                        toolTip.SetToolTip(ch, result);
                    }
                    else
                    {
                        ch.BackColor = Globals.canColor;
                        toolTip.SetToolTip(ch, b.buildingDescription);
                    }
                    if(!localTurn)
                    {
                        ch.BackColor = SystemColors.Control;
                        ch.ForeColor = Color.DimGray;
                    }
                }
            }
            else if (tag == "A") // attack
            {
                if (panelShowedIndex == 3) buttonAttack.Checked = true;
                foreach(CheckBox chb in weaponButtons)
                {
                    chb.BackColor = Globals.cantColor;
                    chb.Enabled = false;
                    foreach(Weapon wea in Globals.GetLocalPlayer().playerWeapons)
                    {
                        if(chb.Tag.ToString() == wea.weaponTag.ToString())
                        {
                            if(wea.weaponCount > 0)
                            {
                                chb.BackColor = Globals.canColor;
                                chb.Enabled = true;
                            }
                            chb.Text = wea.weaponName + " x" + wea.weaponCount;
                            break;
                        }
                    }
                }
                panelShowedIndex = 3;
                attackMode = true;
            }
        }

        public void AddBuildingsToPanel()
        {
            for (int i = 0; i < Globals.gameData.buildings.Count; i++)
            {
                Building b = Globals.gameData.buildings[i];
                CheckBox buildingButton = new CheckBox();
                buildingButton.AutoSize = false;
                buildingButton.Appearance = Appearance.Button;
                buildingButton.Size = new Size(163, 24);
                buildingButton.Name = "ButtonBuilding_" + b.buildingTag;
                buildingButton.Text = b.buildingName;
                buildingButton.Tag = b.buildingTag;
                buildingButton.TextAlign = ContentAlignment.MiddleCenter;
                buildingButton.Parent = panelBuildings;
                buildingButton.Location = new Point(3, i * 30 + 3);
                buildingButton.CheckedChanged += buildingButton_CheckedChanged;
                buildButtons.Add(buildingButton);
                toolTip.SetToolTip(buildingButton, b.buildingDescription);
            }
        }

        public void AddWeaponsToPanel()
        {
            int i = 0;
            foreach(Weapon w in Globals.GetLocalPlayer().playerWeapons)
            {
                CheckBox weaponButton = new CheckBox();
                weaponButton.AutoSize = false;
                weaponButton.Appearance = Appearance.Button;
                weaponButton.Size = new Size(163, 24);
                weaponButton.Name = "ButtonWeapon_" + w.weaponTag;
                weaponButton.Text = w.weaponName + " x" + w.weaponCount;
                weaponButton.Tag = w.weaponTag;
                weaponButton.TextAlign = ContentAlignment.MiddleCenter;
                weaponButton.Parent = panelWeapons;
                weaponButton.Location = new Point(3, i * 30 + 3);
                weaponButton.CheckedChanged += weaponButton_CheckedChanged;
                weaponButtons.Add(weaponButton);
                toolTip.SetToolTip(weaponButton, w.weaponDesc);
                i++;
            }
        }

        public string SetToolTipToMap(int x, int y)
        {
            Player p = Globals.GetLocalPlayer();
            string tip = "Teren : ";
            if (Between(p.map[x,y], 0.0f, 0.1f)) 
                tip += "Woda" + Environment.NewLine + "Nie można budować na wodzie";
            else if (Between(p.map[x, y], 0.1f, 0.3f)) 
                tip += "Piasek" + Environment.NewLine + "Można budować, zwykle zapewnia normalną wydajość budynków";
            else if (Between(p.map[x, y], 0.3f, 0.8f)) 
                tip += "Trawa" + Environment.NewLine + "Można budować, w większości zapewnia dobrą wydajność budynków";
            else if(Between(p.map[x, y], 0.8f, 2f)) 
                tip += "Góry" + Environment.NewLine + "Można budować, w większości zapwenia gorsza wydajność";
            else tip += "Zniszczony teren" + Environment.NewLine + "Nie można budować...";
            return tip;
        }

        public void DrawMap(bool isNew, int cell = -1)
        {
            if (cell == -1) cell = cellSize;
            if (cell < 1) cell = 1;
            mapBitmap = new Bitmap(Globals.mapSize * cell, Globals.mapSize * cell);
            g = Graphics.FromImage(mapBitmap);
            g.Clear(Color.Black);

            Player p = Globals.GetLocalPlayer();

            if(attackMode)
            {
                foreach(Player pl in Globals.players)
                {
                    if(!pl.isLocal)
                    {
                        p = pl;
                        break;
                    }
                }
            }

            for (int x = 0; x < Globals.mapSize; ++x)
            {
                for (int y = 0; y < Globals.mapSize; ++y)
                {
                    Color c = Performances.GetColorByPerf(p.map[x, y]);
                    if (attackMode)
                    {
                        float colorMultiplier = 2.5f;
                        int R = Math.Round(c.R / colorMultiplier).ToInt();//c.R - 64;
                        int G = Math.Round(c.G / colorMultiplier).ToInt();//c.G - 64;
                        int B = Math.Round(c.B / colorMultiplier).ToInt();//c.B - 64;
                        Color newCol = Color.FromArgb(R >= 0 ? R : 0, G >= 0 ? G : 0, B >= 0 ? B : 0);
                        bool found = false;
                        g.FillRectangle(new SolidBrush(newCol), x * cell, y * cell, cell, cell);
                        foreach (BuildingCellSpy bcs in everSpyPoints)
                        {
                            if (bcs.x == x && bcs.y == y)
                            {
                                g.FillRectangle(new SolidBrush(c), x * cell, y * cell, cell, cell);
                                if (bcs.buildingIndex > 0)
                                {
                                    Building b = Globals.gameData.GetBuildingBy(bcs.buildingIndex, "I");
                                    g.DrawImage(b.buildingImage, x * cell, y * cell, cell, cell);
                                }
                                if(bcs.buildingIndex == -2)
                                {
                                    Color tmpC = Performances.GetColorByPerf(-1f);
                                    g.FillRectangle(new SolidBrush(tmpC), x * cell, y * cell, cell, cell);
                                    g.DrawImage(Properties.Resources.WeaponHole, x * cell, y * cell, cell, cell);
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(c), x * cell, y * cell, cell, cell);
                        foreach (PlayerBuilding pb in p.playerBuildings)
                        {
                            if (pb.x == x && pb.y == y)
                            {
                                if (pb.isDestroyed)
                                {
                                    Color tmpC = Performances.GetColorByPerf(-1f);
                                    g.FillRectangle(new SolidBrush(tmpC), x * cell, y * cell, cell, cell);
                                    g.DrawImage(Properties.Resources.WeaponHole, x * cell, y * cell, cell, cell);
                                }
                                else
                                {
                                    Building b = Globals.gameData.GetBuildingBy(pb.buildingIndex, "I");
                                    g.DrawImage(b.buildingImage, x * cell, y * cell, cell, cell);
                                }
                            }
                        }
                    }
                }
            }
            DrawNet(cell);
            if (!attackMode) RedrawBuildingsForUpgrade();
            pictureBoxMap.Size = mapBitmap.Size;
            pictureBoxMap.Image = mapBitmap;
            pictureBoxMap.Location = SetAtCenter(isNew);
        }

        public void RedrawCell(int x, int y, bool upgrade = false)
        {
            //Point point = new Point(x * zoomPower, y * zoomPower);
            Bitmap newBmpForCell = new Bitmap(zoomPower, zoomPower);
            Bitmap oldBmp = new Bitmap(zoomPower, zoomPower);
            CellsMap thisCell = new CellsMap();
            foreach (CellsMap cell in cells)
            {
                if (cell.x == x && cell.y == y)
                {
                    oldBmp = cell.oldBmp;
                    thisCell = cell;
                    break;
                }
            }
            Player p = Globals.GetLocalPlayer();
            if(attackMode)
            {
                p = Globals.GetNotLocalPlayer();
            }
            int buildingIndex = 0;
            foreach(PlayerBuilding pb in p.playerBuildings)
            {
                if(pb.x == x && pb.y == y)
                {
                    buildingIndex = pb.buildingIndex;
                    if(pb.isDestroyed) buildingIndex = -2;
                }
            }
            Graphics gr = Graphics.FromImage(newBmpForCell);
            if(buildingIndex == 0)
            {
                Color c = Performances.GetColorByPerf(p.map[x, y]);
                gr.Clear(c);
                thisCell.oldBmp = newBmpForCell;
                g.DrawImage(newBmpForCell, x * zoomPower, y * zoomPower, zoomPower, zoomPower);
            }
            else
            {
                if(buildingIndex == -2)
                {
                    Color c = Performances.GetColorByPerf(-1f);
                    gr.Clear(c);
                    gr.DrawImage(Properties.Resources.WeaponHole, 0, 0, newBmpForCell.Width, newBmpForCell.Height);
                    thisCell.oldBmp = newBmpForCell;
                    g.DrawImage(newBmpForCell, x * zoomPower, y * zoomPower, zoomPower, zoomPower);
                }
                else
                {
                    Building b = Globals.gameData.GetBuildingBy(buildingIndex, "I");
                    Color c = Performances.GetColorByPerf(p.map[x, y]);
                    gr.Clear(c);
                    gr.DrawImage(oldBmp, 0, 0);
                    gr.DrawImage(b.buildingImage, 0, 0, zoomPower, zoomPower);
                    thisCell.oldBmp = newBmpForCell;
                    g.DrawImage(oldBmp, x * zoomPower, y * zoomPower, zoomPower, zoomPower);
                    g.DrawImage(b.buildingImage, x * zoomPower, y * zoomPower, zoomPower, zoomPower);
                    if (upgrade) g.DrawImage(Properties.Resources.UpgradeYes, x * zoomPower, y * zoomPower, zoomPower, zoomPower);
                }
            }
            pictureBoxMap.Refresh();
        }

        public void DrawNet(int zoom)
        {
            if (zoom < 15) return;
            Pen p = new Pen(Color.DimGray);
            Point p1 = new Point(0,0);
            Point p2 = new Point(0,0);
            for(int x = zoom; x < (Globals.mapSize * zoom); x += zoom)
            {
                p1 = new Point(x, 0);
                p2 = new Point(x, (Globals.mapSize * zoom) - 1);
                g.DrawLine(p, p1, p2);
                p1 = new Point(0, x);
                p2 = new Point((Globals.mapSize * zoom) - 1, x);
                g.DrawLine(p, p1, p2);
            }
        }

        public void DrawBuilding(int x1, int y1, int buildIndex)
        {
            Building b = Globals.gameData.GetBuildingBy(buildIndex, "I");
            Bitmap newBmpForCell = new Bitmap(zoomPower, zoomPower);
            Bitmap oldBmp = new Bitmap(zoomPower, zoomPower);
            CellsMap thisCell = new CellsMap();
            foreach(CellsMap cell in cells)
            {
                if(cell.x == x1 && cell.y == y1)
                {
                    oldBmp = cell.oldBmp;
                    thisCell = cell;
                    break;
                }
            }
            Graphics gr = Graphics.FromImage(newBmpForCell);
            gr.DrawImage(oldBmp, 0, 0);
            gr.DrawImage(b.buildingImage, 0, 0, zoomPower, zoomPower);
            thisCell.oldBmp = newBmpForCell;
            g.DrawImage(b.buildingImage, x1 * zoomPower, y1 * zoomPower, zoomPower, zoomPower);
            pictureBoxMap.Refresh();
        }

        private float[] GetPercentToBounds()
        {
            float[] result = new float[2];
            if (panelScrollMap.Size.Width >= pictureBoxMap.Size.Width || panelScrollMap.Size.Height >= pictureBoxMap.Size.Width)
            {
                result[0] = 0f;
                result[1] = 0f;
            }
            else
            {
                result[0] = (Math.Abs(pictureBoxMap.Location.X) / (float)pictureBoxMap.Size.Width);
                result[1] = (Math.Abs(pictureBoxMap.Location.Y) / (float)pictureBoxMap.Size.Height);
            }
            return result;
        }

        public Point SetAtCenter(bool isNew, int diffZoom = 0, float[] percent = null)
        {
            if (isNew || (panelScrollMap.Size.Width >= pictureBoxMap.Size.Width && panelScrollMap.Size.Height >= pictureBoxMap.Size.Width))
            {
                Size size = pictureBoxMap.Size;
                int centerX = Math.Round(size.Width / 2f).ToInt() - Math.Round(panelScrollMap.Size.Width / 2f).ToInt();
                int centerY = Math.Round(size.Height / 2f).ToInt() - Math.Round(panelScrollMap.Size.Height / 2f).ToInt();
                return new Point(-centerX, -centerY);
            }
            else
            {
                if (percent == null) percent = new float[2];
                int posX = Math.Round(pictureBoxMap.Size.Width * percent[0]).ToInt();
                int posY = Math.Round(pictureBoxMap.Size.Height * percent[1]).ToInt();

                int pX = Math.Round(posX / (float)diffZoom).ToInt();
                int pY = Math.Round(posY / (float)diffZoom).ToInt();

                return new Point(-posX + pX, -posY + pY);
            }
        }

        private void RestoreOldCells(Point cell = new Point(), bool attMode = false)
        {  
            if (cells.Count <= 0) return; 
            for (int i = cells.Count - 1; i >= 0; i--)
            {
                if(!attMode)
                {
                    if (cells[i].x == cell.X && cells[i].y == cell.Y) continue;
                    g.DrawImage(cells[i].oldBmp, cells[i].x * zoomPower, cells[i].y * zoomPower);
                    cells.RemoveAt(i);
                }
                else
                {
                    g.DrawImage(cells[i].oldBmp, cells[i].x * zoomPower, cells[i].y * zoomPower);
                    cells.RemoveAt(i);
                }
            }
        }

        private void DrawHoverCell(Point p, bool onlyClear = false)
        {
            Point cell = new Point(Math.Floor(p.X / (float)zoomPower).ToInt(), Math.Floor(p.Y / (float)zoomPower).ToInt());
            if (cell.X >= Globals.mapSize || cell.Y > Globals.mapSize) return;
            if(!attackMode) RestoreOldCells(cell);
            if (cellForTooltip.X < 0)
            {
                timerInterval = 0;
                cellForTooltip = cell;
            }
            else if(cellForTooltip != cell)
            {
                timerInterval = 0;
                cellForTooltip = cell;
            }
            if (onlyClear)
            {
                pictureBoxMap.Refresh();
                return;
            } 
            bool red = false;
            if(!attackMode)
            {
                foreach (CellsMap ce in cells)
                {
                    if (cell.X == ce.x && cell.Y == ce.y) return;
                }
                Bitmap bmp = new Bitmap(zoomPower, zoomPower);
                Graphics graph = Graphics.FromImage(bmp);
                graph.DrawImage(mapBitmap, new Rectangle(0, 0, zoomPower, zoomPower), new Rectangle(cell.X * zoomPower, cell.Y * zoomPower, zoomPower, zoomPower), GraphicsUnit.Pixel);
                CellsMap cM = new CellsMap();
                cM.oldBmp = bmp;
                cM.x = cell.X;
                cM.y = cell.Y;
                cM.cellPoint = new Point(cM.x, cM.y);
                cells.Add(cM);
                Player pl = Globals.GetLocalPlayer();
                PerfTypes type = Performances.GetTypeByPerf(pl.map[cM.x, cM.y]);
                if(buildingBuildIndexSelected == 0)
                    g.DrawImage(FightIslands.Properties.Resources.Alpha, cM.x * zoomPower, cM.y * zoomPower, zoomPower, zoomPower);
                else if (buildingBuildIndexSelected != 0)
                {
                    if (type == PerfTypes.Woda || type == PerfTypes.Zniszczone)
                        g.DrawImage(FightIslands.Properties.Resources.RedAlpha, cM.x * zoomPower, cM.y * zoomPower, zoomPower, zoomPower);
                    else
                    {
                        foreach (PlayerBuilding pb in pl.playerBuildings)
                        {
                            if (pb.x == cM.x && pb.y == cM.y)
                            {
                                g.DrawImage(FightIslands.Properties.Resources.RedAlpha, cM.x * zoomPower, cM.y * zoomPower, zoomPower, zoomPower);
                                red = true;
                                break;
                            }
                        }
                        if (!red) g.DrawImage(FightIslands.Properties.Resources.GreenAlpha, cM.x * zoomPower, cM.y * zoomPower, zoomPower, zoomPower);
                    }
                }
            }
            else
            {
                if (weaponSelectedIndex == 0)
                {
                    foreach (CellsMap ce in cells)
                    {
                        if (cell.X == ce.x && cell.Y == ce.y) return;
                    }
                    RestoreOldCells(cell);
                    Bitmap bmp = new Bitmap(zoomPower, zoomPower);
                    Graphics graph = Graphics.FromImage(bmp);
                    graph.DrawImage(mapBitmap, new Rectangle(0, 0, zoomPower, zoomPower), new Rectangle(cell.X * zoomPower, cell.Y * zoomPower, zoomPower, zoomPower), GraphicsUnit.Pixel);
                    CellsMap cM = new CellsMap();
                    cM.oldBmp = bmp;
                    cM.x = cell.X;
                    cM.y = cell.Y;
                    cM.cellPoint = new Point(cM.x, cM.y);
                    cells.Add(cM);
                    g.DrawImage(FightIslands.Properties.Resources.Alpha, cM.x * zoomPower, cM.y * zoomPower, zoomPower, zoomPower);
                }
                else
                {
                    RestoreOldCells(new Point(0, 0), true);
                    Player pl = Globals.GetNotLocalPlayer();
                    Weapon w = Globals.gameData.GetWeaponByIndex(weaponSelectedIndex);
                    for (int i = 0; i < w.weaponRangePoints.Count; i++)
                    {
                        Point newPoint = new Point(w.weaponRangePoints[i].X + cell.X, w.weaponRangePoints[i].Y + cell.Y);
                        if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X > Globals.mapSize - 1 || newPoint.Y > Globals.mapSize - 1) continue;

                        Bitmap bmp = new Bitmap(zoomPower, zoomPower);
                        Graphics graph = Graphics.FromImage(bmp);
                        graph.DrawImage(mapBitmap, new Rectangle(0, 0, zoomPower, zoomPower), new Rectangle(newPoint.X * zoomPower, newPoint.Y * zoomPower, zoomPower, zoomPower), GraphicsUnit.Pixel);

                        CellsMap cM = new CellsMap();
                        cM.oldBmp = bmp;
                        cM.x = newPoint.X;
                        cM.y = newPoint.Y;
                        cM.cellPoint = new Point(cM.x, cM.y);
                        cells.Add(cM);

                        PerfTypes type = Performances.GetTypeByPerf(pl.map[newPoint.X, newPoint.Y]);
                        if (type == PerfTypes.Woda || type == PerfTypes.Zniszczone)
                        {
                            g.DrawImage(FightIslands.Properties.Resources.RedAlpha, newPoint.X * zoomPower, newPoint.Y * zoomPower, zoomPower, zoomPower);
                        }
                        else
                        {
                            g.DrawImage(FightIslands.Properties.Resources.GreenAlpha, newPoint.X * zoomPower, newPoint.Y * zoomPower, zoomPower, zoomPower);
                        }
                    }
                }
            }
            pictureBoxMap.Refresh();
        }

        private void pictureBoxMap_MouseDown(object sender, MouseEventArgs e)
        {
            RedrawBuildingsForUpgrade();
            DrawHoverCell(e.Location);
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            Point p = pictureBoxMap.PointToScreen(e.Location);
            cellMouseDown = new Point(Math.Floor(p.X / (float)zoomPower).ToInt(), Math.Floor(p.Y / (float)zoomPower).ToInt());
            mouseX = p.X - pictureBoxMap.Location.X;
            mouseY = p.Y - pictureBoxMap.Location.Y;
            movingMap = true;
        }

        private void pictureBoxMap_MouseUp(object sender, MouseEventArgs e)
        {
            DrawHoverCell(e.Location);
            Point actPoint = e.Location;
            Point actCell = new Point(Math.Floor(actPoint.X / (float)zoomPower).ToInt(), Math.Floor(actPoint.Y / (float)zoomPower).ToInt());
            Point pointer = pictureBoxMap.PointToScreen(e.Location);
            Point cellMouseUp = new Point(Math.Floor(pointer.X / (float)zoomPower).ToInt(), Math.Floor(pointer.Y / (float)zoomPower).ToInt());
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!attackMode)
                {
                    PerfTypes type = Performances.GetTypeByPerf(Globals.GetLocalPlayer().map[actCell.X, actCell.Y]);
                    if(type == PerfTypes.Woda || type == PerfTypes.Zniszczone)
                    {
                        return;
                    }
                    if (buildingBuildIndexSelected == 0) return;
                    blockChecking = true;
                    foreach (CheckBox ch in buildButtons)
                    {
                        ch.Checked = false;
                    }
                    Point realPoint = new Point(Math.Floor(e.Location.X / (float)zoomPower).ToInt(), Math.Floor(e.Location.Y / (float)zoomPower).ToInt());
                    Player pla = Globals.GetLocalPlayer();
                    foreach (PlayerBuilding pbb in pla.playerBuildings)
                    {
                        if (pbb.x == realPoint.X && pbb.y == realPoint.Y) return;
                    }
                    Globals.GetLocalPlayer().SetBuilding(realPoint.X, realPoint.Y, buildingBuildIndexSelected);
                    RedrawCell(realPoint.X, realPoint.Y);
                    CountResources();
                    buildingBuildIndexSelected = 0;
                    ShowRightPanel("I");
                    blockChecking = false;
                }
                else
                {
                    if (weaponSelectedIndex == 0) return;
                    cells.Clear();
                    Globals.gameData.DestroyCells(actCell, weaponSelectedIndex);
                    Player p = Globals.GetLocalPlayer();
                    foreach (Weapon w in p.playerWeapons)
                    {
                        if (w.weaponIndex == weaponSelectedIndex)
                        {
                            foreach (Point po in w.weaponRangePoints)
                            {
                                RedrawCell(actCell.X + po.X, actCell.Y + po.Y);
                            }
                            w.weaponCount--;
                            foreach (CheckBox ch in weaponButtons)
                            {
                                if (ch.Tag == w.weaponTag)
                                {
                                    ch.Text = w.weaponName + " x" + w.weaponCount;
                                    if (w.weaponCount <= 0)
                                    {
                                        weaponSelectedIndex = 0;
                                        ch.BackColor = Globals.cantColor;
                                        ch.Enabled = false;
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            if (cellMouseUp == cellMouseDown && !attackMode)
            {
                blockChecking = true;
                Point realPoint = new Point(Math.Floor(e.Location.X / (float)zoomPower).ToInt(), Math.Floor(e.Location.Y / (float)zoomPower).ToInt());
                PlayerBuilding pb = Globals.GetLocalPlayer().GetBuildingAtPoint(realPoint.X, realPoint.Y);
                if (pb != null)
                {
                    selectedCell = realPoint;
                    foreach(CheckBox ch in buildButtons)
                    {
                        ch.Checked = false;
                    }
                    buttonInfo.Checked = false;
                    ShowRightPanel("C");
                }
                else
                {
                    foreach (CheckBox ch in buildButtons)
                    {
                        ch.Checked = false;
                    }
                    selectedCell = new Point(-1, -1);
                    buttonInfo.Checked = true;
                    buildingBuildIndexSelected = 0;
                    ShowRightPanel("I");
                    DrawHoverCell(realPoint);
                }
                blockChecking = false;
            }
            movingMap = false;
            mouseX = 0;
            mouseY = 0;
        }

        private void pictureBoxMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseOverMap) return;
            if(!movingMap)
            {
                DrawHoverCell(e.Location);
            }
            if (panelScrollMap.Size.Width >= pictureBoxMap.Size.Width && panelScrollMap.Size.Height >= pictureBoxMap.Size.Height) return;
            if(movingMap)
            {
                DrawHoverCell(e.Location, true);
                Point actPoint = pictureBoxMap.PointToScreen(e.Location);
                int diffX = mouseX - actPoint.X;
                int diffY = mouseY - actPoint.Y;
                Point changedPoint = new Point(-diffX, -diffY);

                if (changedPoint.X > 0) changedPoint.X = 0;
                if (changedPoint.Y > 0) changedPoint.Y = 0;
                if (pictureBoxMap.Size.Width < -changedPoint.X + panelScrollMap.Width)
                {
                    changedPoint.X = -(pictureBoxMap.Size.Width - panelScrollMap.Size.Width - 1);
                }
                if (pictureBoxMap.Size.Height < -changedPoint.Y + panelScrollMap.Height)
                {
                    changedPoint.Y = -(pictureBoxMap.Size.Height - panelScrollMap.Size.Height - 1);
                }
                if (panelScrollMap.Size.Width >= pictureBoxMap.Size.Width || panelScrollMap.Size.Height >= pictureBoxMap.Size.Height)
                {
                    if(panelScrollMap.Size.Width >= pictureBoxMap.Size.Width)
                    {
                        Point p = SetAtCenter(false, 0, new float[] { -0.0f, 0.0f});
                        changedPoint.X = p.X;
                    }
                    else if (panelScrollMap.Size.Height >= pictureBoxMap.Size.Height)
                    {
                        Point p = SetAtCenter(false, 0, new float[] { 0.0f, 0.2f});
                        changedPoint.Y = p.Y;
                    }
                }
                pictureBoxMap.Location = new Point(changedPoint.X, changedPoint.Y);
            }
        }

        private void weaponButton_CheckedChanged(object sender, EventArgs e)
        {
            if (blockCheckingWeapon) return;
            blockCheckingWeapon = true;
            CheckBox box = (CheckBox)sender;
            string tag = ((CheckBox)sender).Tag.ToString();
            Weapon w = Globals.gameData.GetWeaponByTag(tag);
            foreach (CheckBox ch in weaponButtons)
            {
                if (weaponSelectedIndex == Globals.gameData.GetWeaponByTag(ch.Tag.ToString()).weaponIndex)
                {
                    ch.Checked = false;
                    weaponSelectedIndex = 0;
                }
                else
                {
                    if (ch != (CheckBox)sender) ch.Checked = false;
                    else
                    {
                        weaponSelectedIndex = Globals.gameData.GetWeaponByTag(ch.Tag.ToString()).weaponIndex;
                    }
                }
            }
            panelScrollMap.Focus();
            blockCheckingWeapon = false;
        }

        private void buildingButton_CheckedChanged(object sender, EventArgs e)
        {
            if (blockChecking) return;
            if(!localTurn)
            {
                blockChecking = true;
                ((CheckBox)sender).Checked = false;
                blockChecking = false;
                return;
            }
            blockChecking = true;
            CheckBox box = (CheckBox)sender;
            if (box.BackColor == Color.Red)
            {
                box.Checked = false;
                blockChecking = false;
                return;
            }
            foreach(CheckBox ch in buildButtons)
            {
                if (buildingBuildIndexSelected == Globals.gameData.GetBuildingBy(ch.Tag.ToString(), "T").buildingIndex)
                {
                    ch.Checked = false;
                    buildingBuildIndexSelected = 0;
                }
                else
                {
                    if (ch != (CheckBox)sender) ch.Checked = false;
                    else
                    {
                        buildingBuildIndexSelected = Globals.gameData.GetBuildingBy(ch.Tag.ToString(), "T").buildingIndex;
                    }
                }
            }
            panelScrollMap.Focus();
            blockChecking = false;
        }

        private void ZoomClick(object sender, EventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            SetZoom(tag);
        }

        private void SetZoom(string tag, int newZoom = 0)
        {
            if (newZoom == 0)
            {
                int oldZoom = zoomPower;
                float[] percent = GetPercentToBounds();
                if (tag == "-") zoomPower -= 5;
                else zoomPower += 5;
                if (zoomPower > -1)
                {
                    if (zoomPower <= 4) zoomPower = 5;
                    if (zoomPower > 50) zoomPower = 50;
                }
                DrawMap(false, zoomPower);
                pictureBoxMap.Location = SetAtCenter(false, oldZoom - zoomPower, percent);
            }
            else
            {
                zoomPower = newZoom;
                DrawMap(false, zoomPower);
                pictureBoxMap.Location = SetAtCenter(true);
            }
            panelScrollMap.Focus();
            CalculateZoom();
        }

        public void CalculateZoom()
        {
            int size = Globals.mapSize * (zoomPower - 5);
            if (panelScrollMap.Width > size || panelScrollMap.Height > size)
                buttonZoomMinus.Enabled = false;
            else
                buttonZoomMinus.Enabled = true;
        }

        public int GetMinimalZoom()
        {
            int lastGood = 50;
            for(int i = 50; i >= 5; i -= 5)
            {
                int size = Globals.mapSize * i;
                if (panelScrollMap.Width < size || panelScrollMap.Height < size) lastGood = i;
                else break;
            }
            return lastGood;
        }

        private void timerMap_Tick(object sender, EventArgs e)
        {
            if (!mouseOverMap)
            {
                timerInterval = 0;
                return;
            }
            if (cellForTooltip.X < 0) return;
            timerInterval++;
            if (timerInterval < 10)
            {
                toolTipMap.SetToolTip(pictureBoxMap, "");
                mapTooltipShowed = false;
            }
            else if (timerInterval >= 10 && !mapTooltipShowed)
            {
                string tip = SetToolTipToMap(cellForTooltip.X, cellForTooltip.Y);
                mapTooltipShowed = true;
                Point p = new Point(cellForTooltip.X * zoomPower + zoomPower, cellForTooltip.Y * zoomPower + zoomPower);
                toolTipMap.Show(tip, pictureBoxMap, p, 20000);
            }
        }

        private void pictureBoxMap_MouseLeave(object sender, EventArgs e)
        {
            mouseOverMap = false;
            DrawHoverCell(new Point(0, 0), true);
        }

        private void pictureBoxMap_MouseEnter(object sender, EventArgs e)
        {
            mouseOverMap = true;
        }

        private void ButtonMainCheck(object sender, EventArgs e)
        {
            string tag = ((CheckBox)sender).Tag.ToString();
            if (tag == "BUILD")
            {
                buttonAttack.Checked = false;
                buttonInfo.Checked = false;
                ShowRightPanel("B");
            }
            else
            {
                foreach(CheckBox ch in buildButtons)
                {
                    ch.Checked = false;
                }
                if (tag == "INFO")
                {
                    buttonAttack.Checked = false;
                    if(attackMode)
                    {
                        attackMode = false;
                        int z = GetMinimalZoom();
                        zoomPower = z;
                        DrawMap(false, zoomPower);
                    }
                    ShowRightPanel("I");
                }
                else if (tag == "ATTACK")
                {
                    if (attackMode)
                    {
                        blockChecking = true;
                        buttonAttack.Checked = true;
                        blockChecking = false;
                        panelScrollMap.Focus();
                        return;
                    }
                    cells.Clear();
                    buttonInfo.Checked = attackMode;
                    selectedCell = new Point(0, 0);
                    buildingBuildIndexSelected = 0;
                    ShowRightPanel("A");
                    int z = GetMinimalZoom();
                    zoomPower = z;
                    DrawMap(false, zoomPower);
                }
            }
            panelScrollMap.Focus();
        }

        #region NIE UZYTE!
        public void SetAnim(int multiplier, Point pos)
        {
            Point tmpPoint = new Point(pos.X * zoomPower, pos.Y * zoomPower);
            Point result = new Point(tmpPoint.X - pictureBoxMap.Location.X, tmpPoint.Y - pictureBoxMap.Location.Y);

            RestoreOldCells(new Point(0, 0), true);
            DrawHoverCell(result);
            animFrameCount = 15;
            animFrame = 1;
            timerExplosionAnim.Enabled = true;
        }
        #endregion

        private void timerExplosionAnim_Tick(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)Properties.Resources.ResourceManager.GetObject("Explosion__" + animFrame + "_");
            Bitmap tmpBmp = new Bitmap(bmp.Size.Width, bmp.Size.Height);
            Graphics g = Graphics.FromImage(tmpBmp);
            g.DrawImage(animPartBitmap, 0,0, tmpBmp.Size.Width, tmpBmp.Size.Height);
            g.DrawImage(bmp, 0, 0, tmpBmp.Size.Width, tmpBmp.Size.Height);
            animFrame++;
            if (animFrame == animFrameCount)
            {
                animFrame = 1;
                timerExplosionAnim.Enabled = false;
            }
        }

        private void CellInfoButton(object sender, EventArgs e)
        {
            if (!localTurn) return;
            Button bu = (Button)sender;
            string tag = bu.Tag.ToString();
            Player p = Globals.GetLocalPlayer();
            PlayerBuilding pb = Globals.GetLocalPlayer().GetBuildingAtPoint(selectedCell.X, selectedCell.Y);
            if(tag == "U") // ulepsz
            {
                if (bu.BackColor == Globals.cantColor) return;
                p.people -= pb.addPeople;
                p.cash -= pb.upgradeCostCash;
                p.energy -= pb.upgradeCostEnergy;
                pb.UpgradeBuilding();
                p.people += pb.addPeople;
                CountResources();
            }
            else if(tag == "W") // wyłącz
            {
                if (pb.isWorking)
                {
                    p.cash += pb.destroyAddCash;
                    p.energy += pb.destroyAddEn;
                    pb.TurnOffBuilding();
                }
                else
                {
                    p.cash += pb.turnOnCostCash;
                    p.energy += pb.turnOnCostEn;
                    pb.TurnOnBuilding();
                }
            }
            else if(tag == "Z") // zniszcz
            {
                Building b = Globals.gameData.GetBuildingBy(pb.buildingIndex, "I");
                p.cash += pb.destroyAddCash;
                p.energy += pb.destroyAddEn;
                p.people -= pb.addPeople;
                p.peopleUsing -= b.buildingCostPeople;
                int x = pb.x;
                int y = pb.y;
                pb.DestroyBuilding();
                p.map[x, y] = -1f;
                CountResources();
                RedrawCell(x, y);
            }
            ShowRightPanel("C");
        }

        private void Game_ResizeEnd(object sender, EventArgs e)
        {
            CalculateZoom();
            SetZoomAuto();
        }

        public void SetZoomAuto()
        {
            int size = Globals.mapSize * (zoomPower - 5);
            if(size < panelScrollMap.Width || size < panelScrollMap.Height)
            {
                int sizer = zoomPower;
                while(true)
                {
                    sizer += 5;
                    int tmpSize = Globals.mapSize * sizer;
                    if (tmpSize > panelScrollMap.Width && tmpSize > panelScrollMap.Height)
                    {
                        break;
                    }
                    if (zoomPower > 45)
                    {
                        zoomPower = 50;
                        break;
                    }
                    zoomPower = sizer + 5;
                }
                if (zoomPower < 5) return;
                SetZoom("-", zoomPower);
            }
        }

        private void Game_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Maximized)
            {
                CalculateZoom();
                SetZoomAuto();
            }
        }

        public void ButtonActive(bool activate)
        {
            buttonAttack.Enabled = activate;
            buttonInfo.Enabled = true;
        }

        public void UncheckAllButtons()
        {
            blockChecking = true;
            buttonInfo.Checked = false;
            buttonAttack.Enabled = false;
            foreach(CheckBox ch in buildButtons)
            {
                ch.Checked = false;
            }
            foreach(CheckBox ch in weaponButtons)
            {
                ch.Checked = false;
            }
            blockChecking = false;
        }

        public void BeginTurn(bool first, string report)
        {
            Globals.GetLocalPlayer().playerTurn = true;
            localTurn = true;
            UncheckAllButtons();
            blockChecking = true;
            buttonInfo.Checked = true;
            buttonEndTurn.BackColor = SystemColors.Control;
            buttonEndTurn.Text = "Zakończ turę";
            ButtonActive(true);
            buildingBuildIndexSelected = 0;
            if (attackMode)
            {
                attackMode = false;
                DrawMap(false, zoomPower);
                attackMode = false;
            }
            selectedCell = new Point(0, 0);
            cellMouseDown = new Point(0, 0);
            weaponSelectedIndex = 0;
            ShowRightPanel("I");
            blockChecking = false;
            CountResources();
            ShowRightPanel("I");
            if (!first)
            {
                if (CheckForEnd())
                {
                    List<string> list = new List<string>();
                    list.Add("PLAYER_LOST");
                    Globals.Send(ParserCreator.Compress(Globals.GetLocalPlayer().playerId, 4, list));
                    MessageBox.Show("Przeciwnik zwyciężył!");
                    Environment.Exit(0);
                }
                Globals.gameData.BeginTurn(report);
            }
            panelScrollMap.Focus();
        }

        public bool CheckForEnd()
        {
            Player p = Globals.GetLocalPlayer();
            int goodBuild = 0;
            foreach(PlayerBuilding pb in p.playerBuildings)
            {
                if (!pb.isDestroyed)
                {
                    goodBuild++;
                    break;
                }
            }
            if (goodBuild == 0 && p.cash <= 5 && p.energy <= 5 && p.people - p.peopleUsing <= 5) return true;
            return false;
        }

        public void EndTurn(bool first = false)
        {
            Globals.GetLocalPlayer().playerTurn = false;
            localTurn = false;
            UncheckAllButtons();
            blockChecking = true;
            buttonInfo.Checked = true;
            buttonEndTurn.BackColor = Color.Red;
            buttonEndTurn.Text = "Tura przeciwnika...";
            ButtonActive(false);
            buildingBuildIndexSelected = 0;
            if (attackMode)
            {
                attackMode = false;
                DrawMap(false, zoomPower);
                attackMode = false;
            }
            selectedCell = new Point(0, 0);
            cellMouseDown = new Point(0, 0);
            weaponSelectedIndex = 0;
            ShowRightPanel("I");
            blockChecking = false;
            if (!first)
            {
                AddResources();
                CountResources();
                Globals.gameData.EndTurn();
            }
            panelScrollMap.Focus();
        }

        public void AddResources()
        {
            Player p = Globals.GetLocalPlayer();
            foreach(PlayerBuilding pb in p.playerBuildings)
            {
                if (pb.isDestroyed || !pb.isWorking) continue;
                Building b = Globals.gameData.GetBuildingBy(pb.buildingIndex, "I");
                if (b.weaponCreator == BuildingWeapon.NotCreator)
                {
                    p.energy += pb.addEnergy;
                    p.energy -= b.energyPerTurn;
                    p.cash -= b.cashPerTurn;
                    p.cash += pb.addCash;
                }
                else
                {
                    foreach(Weapon w in p.playerWeapons)
                    {
                        if(w.weaponType == b.weaponCreation)
                        {
                            w.weaponCount++;
                            break;
                        }
                    }
                }
            }
        }

        public void NewTurn(List<BuildingCellSpy> spyPoints, List<Point> shootedPoints)
        {
            StringBuilder report = new StringBuilder();
            bool foundCellSpy = false;
            bool spyNewInfo = false;
            for (int i = everSpyPoints.Count - 1; i >= 0; i--)
            {
                BuildingCellSpy bcsEver = everSpyPoints[i];
                bcsEver.turnLeft--;
                if (bcsEver.turnLeft <= 0) everSpyPoints.RemoveAt(i);
            }
            if(spyPoints.Count > 0)
            {
                foreach(BuildingCellSpy bcs in spyPoints)
                {
                    foundCellSpy = false;
                    for (int i = everSpyPoints.Count - 1; i >= 0; i--)
                    {
                        BuildingCellSpy bcsEver = everSpyPoints[i];
                        if (bcs.x == bcsEver.x && bcs.y == bcsEver.y) // BYŁO JUŻ ODKRYTE WCZEŚNIEJ, SPRAWDZANIE CZY SIĘ ZMIENIŁO
                        {
                            foundCellSpy = true;
                            if (bcs.buildingIndex != bcsEver.buildingIndex)
                            {
                                if (bcs.buildingIndex > 0)
                                {
                                    Building b = Globals.gameData.GetBuildingBy(bcs.buildingIndex, "I");
                                    report.AppendLine("Szpieg zauważył zmiany w odkrytym miejscu! : " + bcs.x + "x" + bcs.y + " | " + b.buildingName);
                                }
                                else
                                {
                                    Player notLocal = Globals.GetNotLocalPlayer();
                                    notLocal.map[bcs.x, bcs.y] = -1f;
                                    bcsEver.buildingIndex = -2;
                                    report.AppendLine("Szpieg zauważył zmiany w odkrytym miejscu! : " + bcs.x + "x" + bcs.y + " | Budynek zniszczony");
                                }
                                spyNewInfo = true;
                            }
                        }
                    }
                    if(!foundCellSpy) // NOWO ODKRYTE
                    {
                        if(bcs.buildingIndex > 0)
                        {
                            if (!spyNewInfo) spyNewInfo = true;
                            Building b = Globals.gameData.GetBuildingBy(bcs.buildingIndex, "I");
                            report.AppendLine("Szpieg dostarczył informacje o nowym miejscu! : " + bcs.x + "x" + bcs.y + " | " + b.buildingName);
                        }
                        BuildingCellSpy bu = new BuildingCellSpy();
                        bu.x = bcs.x;
                        bu.y = bcs.y;
                        bu.point = new Point(bu.x, bu.y);
                        bu.buildingIndex = bcs.buildingIndex;
                        everSpyPoints.Add(bu);
                    }
                }
                if(!spyNewInfo)
                {
                    report.AppendLine("Szpieg nie dostarczył żadnych znaczących informacji!");
                }
                report.AppendLine("");
            }
            if(shootedPoints.Count > 0)
            {
                if(shootedPoints.Count == 1) report.AppendLine("Przeciwnik zaatakował pole : ");
                else if (shootedPoints.Count > 1) report.AppendLine("Przeciwnik zaatakował pola : ");
                Player p = Globals.GetLocalPlayer();
                foreach(Point po in shootedPoints)
                {
                    bool cellBuilding = false;
                    foreach(PlayerBuilding pb in p.playerBuildings)
                    {
                        if(pb.x == po.X && pb.y == po.Y)
                        {
                            Building b = Globals.gameData.GetBuildingBy(pb.buildingIndex, "I");
                            report.AppendLine("(" + po.X + " x " + po.Y + ") : " + b.buildingName + ", poziom : " + pb.buildingLevel);
                            p.people -= pb.addPeople;
                            p.peopleUsing -= b.buildingCostPeople;
                            pb.isDestroyed = true;
                            pb.isWorking = false;
                            cellBuilding = true;
                        }
                    }
                    if(!cellBuilding) report.AppendLine("Pole (" + po.X + " x " + po.Y + ") : brak budynku.");
                    p.map[po.X, po.Y] = -1.0f;
                    RedrawCell(po.X, po.Y);
                }
            }
            else if(shootedPoints.Count == 0)
            {
                report.AppendLine("Przeciwnik nie użył broni w tej turze.");
            }
            BeginTurn(false, report.ToString());
        }

        private bool CheckCanEndTurn()
        {
            int tmpCash = 0;
            int tmpEn = 0;

            bool badCash = false;
            bool badEn = false;

            Player p = Globals.GetLocalPlayer();

            foreach(PlayerBuilding pb in p.playerBuildings)
            {
                if (pb.isDestroyed || !pb.isWorking) continue;
                Building b = Globals.gameData.GetBuildingBy(pb.buildingIndex, "I");
                tmpCash += pb.addCash;
                tmpCash -= b.cashPerTurn;
                tmpEn += pb.addEnergy;
                tmpEn -= b.energyPerTurn;
            }
            badCash = (tmpCash + p.cash) < 0;
            badEn = (tmpEn + p.energy) < 0;

            if(badCash || badEn)
            {
                string endText = "Nie można zakończyć tury ";
                if (badCash && badEn) endText += ", zabraknie funduszy i energii na jej zakończenie";
                else if (badEn) endText += ", zabraknie energii na jej zakończenie";
                else if (badCash) endText += ", zabraknie funduszy na jej zakończenie";
                MessageBox.Show(endText);
                return false;
            }
            return true;
        }

        private void buttonEndTurn_Click(object sender, EventArgs e)
        {
            if (!Globals.GetLocalPlayer().playerTurn) return;
            if (!CheckCanEndTurn()) return;
            blockChecking = true;
            blockCheckingWeapon = true;
            buttonAttack.Checked = false;
            buttonInfo.Checked = true;
            foreach (CheckBox ch in weaponButtons)
            {
                ch.Checked = false;
            }
            foreach (CheckBox ch in buildButtons)
            {
                ch.Checked = false;
            }
            blockChecking = false;
            blockCheckingWeapon = false;
            EndTurn();
        }

        public void EnemyLost()
        {
            MessageBox.Show("Przeciwnik został zniszczony!");
            Environment.Exit(0);
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.CloseConnections();
        }

        private void RedrawBuildingsForUpgrade()
        {
            Player p = Globals.GetLocalPlayer();
            foreach(PlayerBuilding pb in p.playerBuildings)
            {
                //if(pb.upgradeCostCash <= p.cash && pb.upgradeCostEnergy <= p.energy) RedrawCell(pb.x, pb.y, true);
                //else RedrawCell(pb.x, pb.y, false);
            }
        }
    }
}
