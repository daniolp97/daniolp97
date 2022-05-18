using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightIslands
{
    public static class Commander
    {
        public static Dictionary<int, System.Action<DataObject>> methodList;

        public static void Initialize()
        {
            methodList = new Dictionary<int, System.Action<DataObject>>();
            methodList.Add(0, FirstClientData);
            methodList.Add(1, FirstServerData);
            methodList.Add(2, AllDataToClient);
            methodList.Add(3, TurnDataToAll);
            methodList.Add(4, GameEnded);
        }

        public static void ExecuteCommand(DataObject dataO)
        {
            int commId = dataO.commandId;
            for (int i = 0; i < methodList.Keys.Count; i++)
            {
                if (methodList.ElementAt(i).Key == commId)
                {
                    methodList[i](dataO);
                    break;
                }
            }
        }

        public static void FirstClientData(DataObject dataObject)
        {
            Player p = new Player();
            p.playerName = dataObject.parameters[0];
            p.playerId = Globals.rand.Next(1000,9999);
            p.isLocal = false;
            Globals.players.Add(p);
            List<string> list = new List<string>();
            Player pS = Globals.GetLocalPlayer();
            list.Add(pS.playerName);
            list.Add(pS.playerId.ToString());
            list.Add(p.playerId.ToString());
            Globals.Send(ParserCreator.Compress(Globals.GetLocalPlayer().playerId, 1, list));
            Globals.lobbyWnd.Invoke((MethodInvoker)delegate
            {
                Globals.lobbyWnd.SetPlayerList();
            });
        }

        public static void FirstServerData(DataObject dataObject)
        {
            Player p = new Player();
            p.playerName = dataObject.parameters[0];
            p.playerId = dataObject.parameters[1].ToInt();
            p.isLocal = false;
            Globals.players.Add(p);
            Player pL = new Player();
            pL.playerName = Globals.localPlayerName;
            pL.playerId = dataObject.parameters[2].ToInt();
            pL.isLocal = true;
            Globals.players.Add(pL);
            Globals.lobbyWnd.Invoke((MethodInvoker)delegate
            {
                Globals.lobbyWnd.SetMode("JOIN");
                Globals.lobbyWnd.SetPlayerList();
            });
        }

        public static void AllDataToClient(DataObject dataObject)
        {
            Player p = Globals.GetLocalPlayer();
            p.energy = dataObject.parameters[0].ToInt();
            p.cash = dataObject.parameters[1].ToInt();
            p.people = dataObject.parameters[2].ToInt();
            int mapSize = dataObject.parameters[3].ToInt();
            p.mapSeed = dataObject.parameters[4].ToInt();
            foreach(Player pl in Globals.players)
            {
                if(!pl.isLocal)
                {
                    pl.mapSeed = dataObject.parameters[5].ToInt();
                    break;
                }
            }
            Globals.spyCellShowedTurns = dataObject.parameters[6].ToInt();
            Globals.mapSize = mapSize;
            string[] buildings = dataObject.parameters[7].Split('|');
            List<int> startBuild = new List<int>();
            foreach(string s in buildings)
            {
                startBuild.Add(s.ToInt());
            }
            Globals.lobbyWnd.Invoke((MethodInvoker)delegate
            {
                p.map = Globals.lobbyWnd.GenerateMap();
                p.mapForSpy = p.GenerateMapForSpy();
                foreach (Player pl in Globals.players)
                {
                    if (!pl.isLocal)
                    {
                        pl.map = Globals.lobbyWnd.GenerateMap(pl.mapSeed);
                        break;
                    }
                }
                p.GenerateBuildings(startBuild);
                Game g = new Game();
                Globals.gameWnd = g;
                Globals.lobbyWnd.StartGame();
            });
        }

        public static void TurnDataToAll(DataObject dataObject)
        {
            Player p = Globals.GetLocalPlayer();
            Player otherP = Globals.GetNotLocalPlayer();
            if(dataObject.parameters[0] == "YOUR_TURN")
            {
                otherP.playerTurn = false;
                p.playerTurn = true;
            }
            otherP.hasSpyLevel = dataObject.parameters[1].Replace("HAS_SPY:", "").ToInt();
            List<BuildingCellSpy> spyPoints = new List<BuildingCellSpy>();
            if(dataObject.parameters[2].Length > 5)
            {
                string[] splited = dataObject.parameters[2].Replace("Spy:", "").Split('|');
                foreach(string str in splited)
                {
                    string[] dataS = str.Split('x');
                    if (dataS.Length < 2) continue;
                    BuildingCellSpy bcs = new BuildingCellSpy();
                    bcs.x = dataS[0].ToInt();
                    bcs.y = dataS[1].ToInt();
                    bcs.point = new Point(bcs.x, bcs.y);
                    bcs.buildingIndex = dataS[2].ToInt();
                    if(bcs.buildingIndex == -2)
                    {

                    }
                    spyPoints.Add(bcs);
                }
            }
            List<Point> shootedPoints = new List<Point>();
            if(dataObject.parameters[3].Length > 7)
            {
                string[] splited = dataObject.parameters[3].Replace("Cells:", "").Split('|');
                foreach (string str in splited)
                {
                    string[] dataS = str.Split('x');
                    Point poi = new Point(dataS[0].ToInt(), dataS[1].ToInt());
                    shootedPoints.Add(poi);
                }
            }
            Globals.gameWnd.Invoke((MethodInvoker)delegate
            {
                Globals.gameWnd.NewTurn(spyPoints, shootedPoints);
            });
        }

        public static void GameEnded(DataObject dataObject)
        {
            if(dataObject.parameters[0].ToString() == "PLAYER_LOST")
            {
                Globals.gameWnd.Invoke((MethodInvoker)delegate
                {
                    Globals.gameWnd.EnemyLost();
                });
            }
        }
    }
}
