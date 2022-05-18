using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightIslands
{
    public enum WeaponType
    {
        Null,
        Rakieta,
        Bomba
    }

    public class BuildingCellSpy
    {
        public int x;
        public int y;
        public Point point;
        public int buildingIndex;
        public int turnLeft;

        public BuildingCellSpy()
        {
            turnLeft = Globals.spyCellShowedTurns;
        }
    }

    public class CellsMap
    {
        public Bitmap oldBmp;
        public int x;
        public int y;
        public Point cellPoint;
    }

    public class Weapon
    {
        public int weaponIndex;
        public string weaponName;
        public int weaponCount;
        public WeaponType weaponType;
        public string weaponTag;
        public string weaponDesc;
        public List<Point> weaponRangePoints;
        public int multiplierAnimSize;
    }

    public class Building
    {
        public int buildingIndex;
        public BuildingTypes type;
        public BuildingWeapon weaponCreator;
        public WeaponType weaponCreation;
        public string buildingName;
        public string buildingTag;
        public int cashPerTurn;
        public int energyPerTurn;
        public int buildingCostCash;
        public int buildingCostEnergy;
        public int buildingCostPeople;
        public string buildingDescription;
        public int buildingAddEnergy;
        public int buildingAddCash;
        public int buildingAddPeople;
        public Image buildingImage;
        public int[] performanceLevels;

        public Building(int index, BuildingTypes t, BuildingWeapon bC, WeaponType wC, string name, string tag, int cashPerT, int enPerT, int cashCost, int enCost, int peoCost, int addEn, int addPeo, int addCash, int[] perf)
        {
            buildingIndex = index;
            buildingName = name;
            type = t;
            weaponCreator = bC;
            weaponCreation = wC;
            buildingTag = tag;
            cashPerTurn = cashPerT;
            energyPerTurn = enPerT;
            buildingCostCash = cashCost;
            buildingCostEnergy = enCost;
            buildingCostPeople = peoCost;
            buildingAddEnergy = addEn;
            buildingAddCash = addCash;
            buildingAddPeople = addPeo;
            performanceLevels = perf;
            buildingImage = GetImage();
        }

        public void SetDescription(string desc)
        {
            buildingDescription = desc;
        }

        private Image GetImage()
        {
            switch(buildingIndex)
            {
                case 1: return new Bitmap(FightIslands.Properties.Resources.City);
                case 2: return new Bitmap(FightIslands.Properties.Resources.Village);
                case 3: return new Bitmap(FightIslands.Properties.Resources.Ship);
                case 4: return new Bitmap(FightIslands.Properties.Resources.Train);
                case 5: return new Bitmap(FightIslands.Properties.Resources.Elektrownia);
                case 6: return new Bitmap(FightIslands.Properties.Resources.Wind);
                case 7: return new Bitmap(FightIslands.Properties.Resources.Spy);
                case 8: return new Bitmap(FightIslands.Properties.Resources.RocketPlatform);
                case 9: return new Bitmap(FightIslands.Properties.Resources.Fabryka);
            }
            return new Bitmap(1, 1);
        }
    }

    public class PlayerBuilding
    {
        public int buildingId;
        public int buildingIndex;
        public int x;
        public int y;
        public bool isWorking;
        public bool isDestroyed;
        public int addCash;
        public int addEnergy;
        public int addPeople;
        public int buildingLevel;
        public int groundLevel; // -1 - nie pasuje (pracuje najgorzej), 0 - pasuje (pracuje dobrze), 1 - najlepiej (pracuje git)

        public int nextLevelAddCash;
        public int nextLevelAddEnergy;
        public int nextLevelAddPeople;

        public int upgradeCostCash;
        public int upgradeCostEnergy;

        public int turnOnCostEn;
        public int turnOnCostCash;
        public int turnOnCostPeo;

        public int destroyAddCash;
        public int destroyAddEn;
        public int destroyAddPeo;

        public void UpgradeBuilding()
        {
            buildingLevel++;
            CountAllData();
        }

        public void TurnOffBuilding()
        {
            isWorking = false;
        }

        public void TurnOnBuilding()
        {
            isWorking = true;
        }

        public void DestroyBuilding()
        {
            isWorking = false;
            isDestroyed = true;
        }

        public void CountAllData()
        {
            Building initialBuilding = Globals.gameData.buildings[0];
            foreach (Building b in Globals.gameData.buildings)
            {
                if(b.buildingIndex == buildingIndex)
                {
                    initialBuilding = b;
                    break;
                }
            }
            CountResources(initialBuilding);
            CountNextLevel(initialBuilding);
            CountDestroy(initialBuilding);
            CountTurnOn(initialBuilding);
        }

        public void CountResources(Building b)
        {
            int helper = 0;
            helper = b.buildingAddCash * buildingLevel;
            addCash = Math.Floor(helper + ((groundLevel / 10f * 2) * helper)).ToInt();
            helper = b.buildingAddEnergy * buildingLevel;
            addEnergy = Math.Floor(helper + ((groundLevel / 10f * 2) * helper)).ToInt();
            helper = b.buildingAddPeople * buildingLevel;
            addPeople = Math.Floor(helper + ((groundLevel / 10f * 2) * helper)).ToInt();
        }

        public void CountNextLevel(Building b)
        {
            int helper = 0;
            helper = b.buildingAddCash * (buildingLevel + 1);
            nextLevelAddCash = Math.Floor(helper + ((groundLevel / 10f * 2) * helper)).ToInt();
            helper = b.buildingAddEnergy * (buildingLevel + 1);
            nextLevelAddEnergy = Math.Floor(helper + ((groundLevel / 10f * 2) * helper)).ToInt();
            helper = b.buildingAddPeople * (buildingLevel + 1);
            nextLevelAddPeople = Math.Floor(helper + ((groundLevel / 10f * 2) * helper)).ToInt();

            upgradeCostCash = b.buildingCostCash + Math.Ceiling(b.buildingCostCash * 0.15f).ToInt();
            upgradeCostEnergy = b.buildingCostEnergy + Math.Ceiling(b.buildingCostEnergy * 0.15f).ToInt();
        }

        public void CountTurnOn(Building b)
        {
            turnOnCostCash = Math.Ceiling(b.buildingCostCash * 0.8f).ToInt();
            turnOnCostEn = Math.Ceiling(b.buildingCostEnergy * 0.8f).ToInt();
            turnOnCostPeo = b.buildingCostPeople;
        }

        public void CountDestroy(Building b)
        {
            destroyAddCash = Math.Ceiling(b.buildingCostCash * 0.5f).ToInt();
            destroyAddEn = Math.Ceiling(b.buildingCostEnergy * 0.5f).ToInt();
            destroyAddPeo = b.buildingCostPeople;
        }
    }

    public class GameData
    {
        public List<Building> buildings;
        public List<Point> destroyedCellsInActTurn;
        public List<BuildingCellSpy> showedPoints;

        public GameData()
        {
            destroyedCellsInActTurn = new List<Point>();
            showedPoints = new List<BuildingCellSpy>();
            InitilizeBuildings();
        }

        public void InitilizeBuildings()
        {
            buildings = BuildingInitializer.CreateBuildings();
        }
        
        public Building GetBuildingBy(object val, string type)
        {
            if(type == "I")
            {
                foreach(Building b in buildings)
                {
                    if (b.buildingIndex == val.ToInt()) return b;
                }
            }
            else if(type == "T")
            {
                foreach (Building b in buildings)
                {
                    if (b.buildingTag == val.ToString()) return b;
                }
            }
            MessageBox.Show("Error in get building by type : " + type + " | val : " + val.ToString());
            return buildings[0];
        }

        public Weapon GetWeaponByTag(string tag)
        {
            Player p = Globals.GetLocalPlayer();
            foreach(Weapon w in p.playerWeapons)
            {
                if(w.weaponTag == tag)
                {
                    return w;
                }
            }
            return new Weapon();
        }

        public Weapon GetWeaponByIndex(int index)
        {
            Player p = Globals.GetLocalPlayer();
            foreach (Weapon w in p.playerWeapons)
            {
                if (w.weaponIndex == index)
                {
                    return w;
                }
            }
            return new Weapon();
        }

        public void DestroyCells(Point initialCell, int wIndex)
        {
            Weapon w = GetWeaponByIndex(wIndex);
            Player p = Globals.GetNotLocalPlayer();
            foreach(Point poi in w.weaponRangePoints)
            {
                Point po = new Point(poi.X + initialCell.X, poi.Y + initialCell.Y);
                destroyedCellsInActTurn.Add(po);
                p.map[po.X, po.Y] = -1f;
            }
            Point animPoint = new Point(initialCell.X,initialCell.Y);
            if(animPoint.X < 0) animPoint.X = 0;
            if(animPoint.Y < 0) animPoint.Y = 0;
        }

        public void BeginTurn(string reportText)
        {
            string line = Environment.NewLine;
            string report = "Raport z tury : " + line + "KONIEC TURY!" + line + line + reportText;
            Reporter rep = new Reporter();
            rep.SetReportText(report);
            rep.SetMiddlePoint(Globals.gameWnd.Location, Globals.gameWnd.Size);
            rep.ShowDialog();
        }

        public void EndTurn()
        {
            List<string> list = new List<string>();
            list.Add("YOUR_TURN");
            Player localP = Globals.GetLocalPlayer();
            Player otherP = Globals.GetNotLocalPlayer();
            int enemyHasSpy = otherP.hasSpyLevel;
            int spyIndex = GetBuildingBy("Ss", "T").buildingIndex;
            int hasSpy = -1;
            foreach(PlayerBuilding plp in localP.playerBuildings)
            {
                if(plp.buildingIndex == spyIndex)
                {
                    if (plp.isWorking && !plp.isDestroyed)
                    {
                        hasSpy = plp.buildingLevel * 20;
                        break;
                    }
                }
            }
            list.Add("HAS_SPY:" + hasSpy);
            StringBuilder spyData = new StringBuilder();
            spyData.Append("Spy:");
            if(enemyHasSpy > 0) // WYSLIJ DO PRZECIWNIKA ODKRYTE PUNKTY
            {
                Random tmpRand = new Random();
                List<Point> tmpShowed = new List<Point>();
                bool all = localP.mapForSpy.Count <= enemyHasSpy;
                for (int i = 0; i < (all ? localP.mapForSpy.Count : enemyHasSpy); i++) // WYSLIJ NOWO ODKRYTE
                {
                    int index = (all ? i : tmpRand.Next(0, localP.mapForSpy.Count));
                    Point poi = new Point(localP.mapForSpy[index].X, localP.mapForSpy[index].Y);
                    tmpShowed.Add(new Point(poi.X, poi.Y));
                    spyData.Append(poi.X + "x" + poi.Y);
                    bool isThereBuild = false;
                    foreach(PlayerBuilding pb in localP.playerBuildings)
                    {
                        if(pb.x == poi.X && pb.y == poi.Y)
                        {
                            isThereBuild = true;
                            int indexer = pb.buildingIndex;
                            if (pb.isDestroyed) indexer = -2;
                            spyData.Append("x" + indexer);
                            break;
                        }
                    }
                    if (!isThereBuild)
                    {
                        if (localP.map[poi.X, poi.Y] == -1f) spyData.Append("x-2|");
                        else spyData.Append("x-1|");
                    }
                    if (i != (all ? localP.mapForSpy.Count - 1 : enemyHasSpy - 1)) spyData.Append("|");
                }
                if (spyData.ToString().Length > 5 && showedPoints.Count > 0) spyData.Append("|");
                for (int i = showedPoints.Count - 1; i >= 0; i--) // WYSLIJ POPRZEDNIO ODKRYTE
                {
                    showedPoints[i].turnLeft--;
                    if(showedPoints[i].turnLeft <= 0)
                    {
                        showedPoints.RemoveAt(i);
                        if (i < showedPoints.Count - 1) spyData.Append("|");
                        continue;
                    }
                    Point poi = new Point(showedPoints[i].x, showedPoints[i].y);
                    spyData.Append(poi.X + "x" + poi.Y);
                    bool isThereBuild = false;
                    foreach (PlayerBuilding pb in localP.playerBuildings)
                    {
                        if (pb.x == poi.X && pb.y == poi.Y)
                        {
                            isThereBuild = true;
                            int index = pb.buildingIndex;
                            if (pb.isDestroyed) index = -2;
                            spyData.Append("x" + index + "|");
                            break;
                        }
                    }
                    if (!isThereBuild)
                    {
                        if (localP.map[poi.X, poi.Y] == -1f) spyData.Append("x-2|");
                        else spyData.Append("x-1|");
                    }
                    if (i != showedPoints.Count - 1) spyData.Append("|");
                }
                foreach(Point po in tmpShowed)
                {
                    BuildingCellSpy bc = new BuildingCellSpy();
                    bc.x = po.X;
                    bc.y = po.Y;
                    showedPoints.Add(bc);
                }
            }
            list.Add(spyData.ToString());
            StringBuilder cellsData = new StringBuilder();
            cellsData.Append("Cells:");
            if(destroyedCellsInActTurn.Count > 0)
            {
                for(int i = 0; i < destroyedCellsInActTurn.Count; i++)
                {
                    Point poi = new Point(destroyedCellsInActTurn[i].X, destroyedCellsInActTurn[i].Y);
                    cellsData.Append(poi.X + "x" + poi.Y);
                    if (i < destroyedCellsInActTurn.Count - 1) cellsData.Append("|");
                }
                destroyedCellsInActTurn.Clear();
            }
            list.Add(cellsData.ToString());
            Globals.Send(ParserCreator.Compress(Globals.GetLocalPlayer().playerId, 3, list));
        }

        public string GetResourcesData()
        {
            string enPerT = "";
            string cashPerT = "";

            string result = "";

            int enPerTAdd = 0;
            int cashPerTAdd = 0;

            int enPerTMin = 0;
            int cashPerTMin = 0;

            Player p = Globals.GetLocalPlayer();
            foreach(PlayerBuilding pb in p.playerBuildings)
            {
                if (!pb.isWorking || pb.isDestroyed) continue;
                foreach(Building b in buildings)
                {
                    if(pb.buildingIndex == b.buildingIndex)
                    {
                        enPerTMin += b.energyPerTurn;
                        cashPerTMin += b.cashPerTurn;
                        enPerTAdd += pb.addEnergy;
                        cashPerTAdd += pb.addCash;
                    }
                }
            }

            string en = "Energia :" + Environment.NewLine +
                "Łącznie na turę : " + (enPerTAdd - enPerTMin) + Environment.NewLine +
                "Uzyskiwana : " + enPerTAdd + Environment.NewLine +
                "Utracana : " + enPerTMin;

            string cash = "Fundusze :" + Environment.NewLine +
                "Łącznie na turę : " + (cashPerTAdd - cashPerTMin) + Environment.NewLine +
                "Uzyskiwane : " + cashPerTAdd + Environment.NewLine +
                "Utracane : " + cashPerTMin;

            result = "Zasoby : " + Environment.NewLine + Environment.NewLine +
                en + Environment.NewLine + Environment.NewLine +
                cash;

            return result;
        }
    }
}
