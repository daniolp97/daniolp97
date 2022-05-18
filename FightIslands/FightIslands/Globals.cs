using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightIslands
{
    public class Player
    {
        public int playerId;
        public string playerName;
        public bool isLocal;
        public int mapSeed;

        public float[,] map;
        public int energy;
        public int cash;
        public int people;
        public int peopleUsing;
        public bool playerTurn;
        public int hasSpyLevel;

        public List<Weapon> playerWeapons;
        public List<PlayerBuilding> playerBuildings;
        public List<Point> mapForSpy;

        public Player()
        {
            playerBuildings = new List<PlayerBuilding>();
            playerWeapons = WeaponInitializer.CreateWeapons();
        }

        public List<Point> GenerateMapForSpy()
        {
            List<Point> list = new List<Point>();
            for(int x = 0; x < Globals.mapSize; x++)
            {
                for(int y = 0; y < Globals.mapSize; y++)
                {
                    if(map[x,y] > 0.1f && map[x,y] < 2f)
                    {
                        list.Add(new Point(x, y));
                    }
                }
            }
            return list;
        }

        public int GetUniqueId()
        {
            if (playerBuildings.Count == 0) return 1000;
            return playerBuildings[playerBuildings.Count - 1].buildingId++;
        }

        public PlayerBuilding GetBuildingAtPoint(int x, int y)
        {
            foreach(PlayerBuilding pb in playerBuildings)
            {
                if (pb.x == x && pb.y == y) return pb;
            }
            return null;
        }

        public void SetBuilding(int x, int y, int buildIndex, bool atStart = false)
        {
            PlayerBuilding pb = new PlayerBuilding();
            Building b = Globals.gameData.GetBuildingBy(buildIndex, "I");
            pb.buildingId = GetUniqueId();
            pb.buildingIndex = buildIndex;
            pb.x = x;
            pb.y = y;
            pb.isWorking = true;
            pb.isDestroyed = false;
            pb.buildingLevel = 1;
            peopleUsing += b.buildingCostPeople;
            int perfLv = Performances.GetPerformanceLevel(map[x,y]);
            for(int i = 0; i < b.performanceLevels.Length; i++)
            {
                if (b.performanceLevels[i] == perfLv)
                {
                    pb.groundLevel = i - 1;
                    break;
                }
            }
            pb.CountAllData();
            if (!atStart)
            {
                cash -= b.buildingCostCash;
                energy -= b.buildingCostEnergy;
            }
            people += pb.addPeople;
            playerBuildings.Add(pb);
        }

        public void GenerateBuildings(List<int> build)
        {
            Random rand = new Random();
            int x = -1;
            int y = -1;
            for (int i = 0; i < build.Count; i++)
            {
                while (true)
                {
                    bool samePos = false;
                    x = rand.Next(0, map.GetLength(0));
                    y = rand.Next(0, map.GetLength(1));
                    PerfTypes type = Performances.GetTypeByPerf(map[x, y]);
                    if (type == PerfTypes.Woda) continue;
                    foreach(PlayerBuilding pb in playerBuildings)
                    {
                        if (pb.x == x && pb.y == y) samePos = true;
                    }
                    if (samePos) continue;
                    SetBuilding(x, y, build[i], true);
                    break;
                }
            }
        }
    }

    public static class Globals
    {
        public static int port = 50010;
        public static IPAddress localIp;
        public static Random rand;
        public static bool mapGenerated;
        public static MainForm mainFormWnd;
        public static Lobby lobbyWnd;
        public static Game gameWnd;
        public static string localPlayerName;

        public static Color canColor;
        public static Color cantColor;

        public static int playerIdTurn;
        public static int mapSize;
        public static List<Player> players;
        public static GameData gameData;

        public static int spyCellShowedTurns;

        public static int energy;
        public static int cash;
        public static int people;

        public static Socket socket;
        public static bool socketClosed;
        public static byte[] buffer = new byte[32768];

        public static void Initialize(MainForm mf)
        {
            mainFormWnd = mf;
            socketClosed = false;
            canColor = Color.LimeGreen;
            cantColor = Color.OrangeRed;
            gameData = new GameData();
            players = new List<Player>();
            rand = new Random();
            Commander.Initialize();
        }

        public static Player GetLocalPlayer()
        {
            foreach(Player p in players)
            {
                if (p.isLocal) return p;
            }
            return null;
        }

        public static Player GetNotLocalPlayer()
        {
            foreach (Player p in players)
            {
                if (!p.isLocal) return p;
            }
            return null;
        }

        public static void ChangeWnd(Form from, Form to)
        {
            from.Hide();
            to.Show();
        }

        public static void FormClosed(Form cl)
        {
            if(cl.Name == "Lobby")
            {
                mainFormWnd.Show();
                lobbyWnd.Dispose();
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            Socket soc = (Socket)ar.AsyncState;
            socket = soc.EndAccept(ar);
            socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReadCallback), socket);
        }

        public static void Receive()
        {
            socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReadCallback), socket);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            Socket s = (Socket)ar.AsyncState;
            int bytesRead = 0;
            try
            {
                bytesRead = s.EndReceive(ar);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Receive : " + ex.Message);
            }
            if (bytesRead > 0)
            {
                byte[] data = new byte[bytesRead];
                Array.Copy(buffer, data, bytesRead);
                Commander.ExecuteCommand(ParserReader.Decompress(data));
                if(!socketClosed)
                    s.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), s); 
            }
        }

        public static void Send(byte[] data)
        {
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
        }

        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket s = (Socket)ar.AsyncState;
                int bytes = s.EndSend(ar);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CloseConnections()
        {
            try
            {
                Environment.Exit(0);
            }
            catch { }
        }
    }
}
