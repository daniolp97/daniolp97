using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;
using System.Threading;
using System.Text;

public class GeneratorScript : MonoBehaviour
{
	#region Variables
	
	[HideInInspector] public int level; 						// actual level to generate
	private int levelChanger = 0; 								// size of actual level (1 cell is 1 in unity scale)
	[HideInInspector] public bool isSaved; 						// was level generated previously before exit game? 
	[HideInInspector] public bool generating; 					// variable to control generating thread
	[HideInInspector] public MainScript mainS; 					// main script is a "bridge" between scripts to control everything
	[HideInInspector] public Thread generatorThread; 			// thread for greneration
	
	[HideInInspector] public int[,] map; 						// map in int[,] array (0 is Wall, -1 is pass, 1 is chest)
	[HideInInspector] public int[] startPos;					// start position (int, int) by cells
	[HideInInspector] public int[] endPos;						// end position (int, int) by cells
	[HideInInspector] public int mapSize;						// as it says, map size int*int
	[HideInInspector] public System.Random rand;				// no need to comment
	
	// Objects to generate maze on scene
	// Each objech has different rotation and size
	public GameObject wallHorizontalDown;
	public GameObject wallHorizontalUp;
	public GameObject wallHorizontalEdge;
	public GameObject wallVerticalUp;
	public GameObject wallVerticalDown;
	public GameObject wallVerticalEdge;
	public GameObject wallAngleLeft;
	public GameObject wallAngleRight;
	public GameObject wallFull;
	
	public GameObject floorStable; 								// Floor under walls which cannot be destroyed
	public GameObject floorUnstable;							// Floor under passage which is destroying after player will step off it
	public GameObject floorEdgeH;								// Edge to create closed maze in horizontal
	public GameObject floorEdgeV;								// Edge to create closed maze in vertical
	public GameObject start;									// Start object which is transparent, player is spawning on it
	public GameObject end;										// Start object which is transparent, and has OnTriggerEnter event
	public GameObject chestPrefab;								// Prefab of chest with scirpts and animation
	
	public Transform wallsParent;								// Parent of all walls
	public GameObject previewObject;							// Object (plane) with generated testure of whole maze
	public GameObject previewObjectBack;						// Background of previewObject
	public GameObject FloorParent;								// Parent of all floors
	public GameObject EdgesParent;								// Parent of all edges
	
	public Texture2D tex;										// Preview texture
	public GameObject chestPreviewPrefab;						// Chest prefab which is spawning only on maze preview
	
	[HideInInspector] public List<Vector2> erasedList;			// List of randomly erased wall to create more paths
	[HideInInspector] public List<GameObject> chestPreviewList;	// List of chest objects preview, for hiding and showing
	[HideInInspector] public List<GameObject> chestList;		// List of all chests
	[HideInInspector] public List<GameObject> wallsList;		// List of walls
	
	#endregion
	
	#region MainMethods (Start's here)
	
	// First method after end level OR at game launch
    public void InitializeMaze(int lvl, bool saved)
	{
		generating = true;
		erasedList = new List<Vector2>();
		wallsList = new List<GameObject>();
		chestPreviewList = new List<GameObject>();
		level = lvl;
		levelChanger = level + 15; // always level + 15, level 5 has 20x20 size
		isSaved = saved;
		generatorThread = new Thread(Generate);
		generatorThread.Start(); // start generating level
	}
	
	void Update()
	{
		if(generating)
		{
			if(!generatorThread.IsAlive)
			{
				generating = false;
				mainS.SetMaze(map); // if generated, spawn maze on scene (SpawnMaze() method) 
			}
		}
	}
	
	#endregion
	
	#region MapGenerator (all is native C# (no Unity dependencies), just copy this and variables to new C# project and will work)
	void Generate()
	{
		if(isSaved) // if there is saved maze, load it instead of generating
		{
			string str = mainS.savedMaze;
			string[] strTable = str.Split('|');
			string lastStr = strTable[strTable.Length - 1];
			mapSize = Int32.Parse(lastStr.Substring(0, lastStr.IndexOf("x"))) + 1;
			if(mapSize == levelChanger)
			{
				map = new int[mapSize,mapSize];
				startPos = new int[2];
				endPos = new int[2];
				for(int i = 0; i < strTable.Length; i++)
				{
					string[] cell = strTable[i].Split('x');
					if(i == 0)
					{
						startPos[0] = Int32.Parse(cell[0]);
						startPos[1] = Int32.Parse(cell[1]);
						endPos[0] = Int32.Parse(cell[2]);
						endPos[1] = Int32.Parse(cell[3]);
						continue;
					}
					int x = Int32.Parse(cell[0]);
					int y = Int32.Parse(cell[1]);
					int field = Int32.Parse(cell[2]);
					map[x,y] = field;
				}
				return;
			}
		}
		// start generating
		startPos = new int[2];
		endPos = new int[2];
		int lastX = -1;
		int lastY = -1;
		rand = new System.Random();
		mapSize = levelChanger;
		int index = 0;
		map = new int[mapSize,mapSize];
		for(int x = 0; x < mapSize; x++)
		{
			for(int y = 0; y < mapSize; y++)
			{
				map[x, y] = -1;
			}
		}
		int rX = rand.Next(1, mapSize - 2);
		int rY = rand.Next(1, mapSize - 2);
		map[rX, rY] = index;

		while (index > -1)
		{
			int x = 0; 
			int y = 0;
			bool found = false;
			for(x = 0; x < mapSize; x++)
			{
				for(y = 0; y < mapSize; y++)
				{
					if (map[x, y] == index)
					{
						found = true;
						break;
					}
				}
				if (found) break;
			}
			if(!found)
			{
				index--;
				continue;
			}
			
			List<Point> goodWalls = GetGoodWalls(x, y);
			if (goodWalls.Count > 0)
			{
				int r = rand.Next(0, goodWalls.Count);
				index++;
				map[goodWalls[r].X, goodWalls[r].Y] = index;
			}
			else
			{
				map[x, y] = -2;
				index--;
			}
		}
		// Start point Y axis is always max, because it will be at the "bottom" of screen, same end Y but then is 0
		int startX = 0;
		int endX = 0;
		while(true) // find start pos
		{
			startX = rand.Next(0, mapSize);
			if(map[startX, mapSize - 1] != -1) break;
		}
		while(true)
		{
			endX = rand.Next(0, mapSize);
			if(map[endX, 0] != -1) break;
		}
		
		startPos = new int[] {startX, mapSize};
		endPos = new int[] {endX, -1};
		
		EraseRandomWalls();
		
		// Reverse map to -1 and 0 because chests are index 1, so this is for safety
		for(int x = 0; x < mapSize; x++)
		{
			for(int y = 0; y < mapSize; y++)
			{
				if(map[x,y] != -1) map[x,y] = 0;
			}
		}
		
		AddChests();
	}
	
	public List<Point> GetGoodWalls(int x, int y)
	{
		List<Point> list = new List<Point>();
		if (x <= 0 || x >= mapSize - 1 || y <= 0 || y >= mapSize - 1) return list;
		if(x < mapSize - 1 && map[x + 1,y] == -1)
		{
			if (IsWallGood(x + 1, y)) list.Add(new Point(x + 1, y));
		}
		if(x > 0 && map[x - 1, y] == -1)
		{
			if (IsWallGood(x - 1, y)) list.Add(new Point(x - 1, y));
		}
		if (y < mapSize - 1 && map[x, y + 1] == -1)
		{
			if (IsWallGood(x, y + 1)) list.Add(new Point(x, y + 1));
		}
		if (y > 0 && map[x, y - 1] == -1)
		{
			if (IsWallGood(x, y - 1)) list.Add(new Point(x, y - 1));
		}
		return list;
	}
	
	public bool IsWallGood(int x, int y)
	{
		bool result = false;
		int straingt = 0;
		int angle = 0;
		if (x < mapSize - 1 && map[x + 1, y] != -1) straingt++;
		if (x > 0 && map[x - 1, y] != -1) straingt++;
		if (y < mapSize - 1 && map[x, y + 1] != -1) straingt++;
		if (y > 0 && map[x, y - 1] != -1) straingt++;

		if (x < mapSize - 1 && y < mapSize - 1 && map[x + 1, y + 1] != -1) angle++;
		if (x > 0 && y > 0 && map[x - 1, y - 1] != -1) angle++;
		if (x < mapSize - 1 && y > 0 && map[x + 1, y - 1] != -1) angle++;
		if (x > 0 && y < mapSize - 1 && map[x - 1, y + 1] != -1) angle++;

		if (straingt < 2 && angle < 2) result = true;
		return result;
	}
	
	public void EraseRandomWalls()
	{
		int erased = 0;
		erasedList.Clear();
		while(erased < level * 2)
		{
			int x = rand.Next(2,mapSize - 2);
			int y = rand.Next(2,mapSize - 2);
			if(map[x,y] == -1)
			{
				erasedList.Add(new Vector2(x,y));
				map[x,y] = 0;
				erased++;
			}
		}
	}
	
	public void AddChests()
	{
		int amount = rand.Next((int)Math.Floor(level * 0.1f), (int)Math.Ceiling(level * 0.2f));
		if(amount == 0) amount = 1;
		for(int i = 0; i < amount; i++)
		{
			while(true)
			{
				int xC = rand.Next(0, mapSize);
				int yC = rand.Next(0, mapSize - 4);
				if(map[xC, yC] == 0)
				{
					map[xC, yC] = 1;
					break;
				}
			}
		}
	}
	#endregion
	
	#region SpawnMap
	
	public void SpawnMaze()
	{
		// Start saving maze, so it won't be generated next time (loading is always faster than generating)
		StringBuilder mazeToSaveBuilder = new StringBuilder();
		mazeToSaveBuilder.Append(startPos[0] + "x" + startPos[1] + "x" + endPos[0] + "x" + endPos[1] + "|");
		string mazeToSave = "";
		for(int x = 0; x < mapSize; x++)
		{
			for(int y = 0; y < mapSize; y++)
			{
				if(x == mapSize - 1 && y == mapSize - 1)
				{
					mazeToSaveBuilder.Append(x + "x" + y + "x" + map[x,y]);
					continue;
				}
				else mazeToSaveBuilder.Append(x + "x" + y + "x" + map[x,y] + "|");
			}
		}
		mazeToSave = mazeToSaveBuilder.ToString();
		PlayerPrefs.SetString("SavedMaze", mazeToSave);
		// END OF SAVING MAZE
		GameObject g;
		for(int x = -1; x <= mapSize; x++)
		{
			for(int y = -1; y <= mapSize; y++)
			{
				if(x == startPos[0] && y == startPos[1])
				{
					SpawnObject(start, new Vector3(x,0,y), "Start"); // set start on scene
					continue;
				}
				if(x == endPos[0] && y == endPos[1])
				{
					SpawnObject(end, new Vector3(x,0,y), "End"); // set end on scene
					continue;
				}
			}
		}
		ConnectWalls(); // there's always some holes between walls so make them disappear
		tex = new Texture2D(levelChanger, levelChanger);
		
		for(int x = 0; x < levelChanger; x++)
		{
			for(int y = 0; y < levelChanger; y++)
			{
				// SET COLORS FOR PREVIEW
				if(map[x,y] == 0) tex.SetPixel(x,y, new UnityEngine.Color(0.95f, 0.95f, 0.95f));
				else if(map[x,y] == -1) tex.SetPixel(x,y, new UnityEngine.Color(0.45f, 0.45f, 0.45f));
				else if(map[x,y] == 1) tex.SetPixel(x,y, new UnityEngine.Color(0.98f, 0.65f, 0.15f));
			}
		}
		tex.filterMode = FilterMode.Point;
		tex.Apply();
		
		//Some transformations of object
		previewObject.transform.localScale = new Vector3((levelChanger) * 0.1f,1,(levelChanger) * 0.1f);
		previewObject.transform.position = new Vector3((levelChanger) * 0.5f - 0.5f,-0.5f,(levelChanger) * 0.5f - 0.5f);
		Vector3 pod = previewObject.transform.position;
		Vector3 podS = previewObject.transform.localScale;
		previewObjectBack.transform.position = new Vector3(pod.x, pod.y - 1, pod.z);
		previewObjectBack.transform.localScale = new Vector3(podS.x + 0.5f, podS.y, podS.z + 0.5f);
		previewObject.GetComponent<Renderer>().materials[0].mainTexture = tex;
	}
	
	// Method to spawn all objects depending on params
	public void SpawnObject(GameObject toSpawn, Vector3 position, string name = "", GameObject chestPrev = null)
	{
		GameObject spawned = (GameObject) Instantiate(toSpawn, position, Quaternion.identity);
		if(name == "Start" || name == "End") 
		{
			if(name == "Start") spawned.transform.parent = EdgesParent.transform;
			else
			{
				spawned.transform.rotation = Quaternion.Euler(0, 180, 0);
				spawned.transform.parent = EdgesParent.transform;
			}
		}
		else if(name == "Chest")
		{
			spawned.transform.parent = wallsParent;
			// if its chest, it has spawned preview before so add it now
			spawned.transform.GetChild(0).gameObject.GetComponent<ChestScript>().chestPreviewGameObject = chestPrev;
			chestList.Add(spawned);
		}
		else spawned.transform.parent = wallsParent;
		if(name != "") spawned.name = name;
		wallsList.Add(spawned);
	}
	
	public void ConnectWalls()
	{
		GameObject wallG;
		int[,] tmpSpawner = new int[mapSize, mapSize]; // 1 - sprawdzone
		SetEdges();
		for(int y = -1; y <= mapSize; y++)
		{
			for(int x = -1; x <= mapSize; x++)
			{
				if(x == -1 || y == -1 || x == mapSize || y == mapSize)
				{
					if(x == -1 && y == -1 && map[x + 1, y + 1] == -1)
					{
						SpawnObject(wallFull, new Vector3(x + 0.5f,0,y + 0.5f));
					}
					else if(y != -1 && x == -1 && y < mapSize && map[x + 1, y] == -1)
					{
						SpawnObject(wallVerticalUp, new Vector3(x + 0.5f,0,y));
						if((y < mapSize - 1 && map[x + 1, y + 1] == -1) || y == mapSize - 1)
						{
							SpawnObject(wallFull, new Vector3(x + 0.5f,0,y + 0.5f));
						}
					}
					else if(x != -1 && y == -1 && x < mapSize && map[x, y + 1] == -1)
					{
						SpawnObject(wallHorizontalUp, new Vector3(x,0,y + 0.5f));
						if(x == mapSize - 1 || map[x + 1, y + 1] == -1)
						{
							SpawnObject(wallFull, new Vector3(x + 0.5f,0,y + 0.5f));
						}
					}
					else if(x == mapSize && y > -1 && y < mapSize && map[x - 1, y] == -1)
					{
						SpawnObject(wallVerticalDown, new Vector3(x - 0.5f,0,y));
						if(y == mapSize - 1 || map[x - 1, y + 1] == -1)
						{
							SpawnObject(wallFull, new Vector3(x - 0.5f,0,y + 0.5f));
						}
					}
					else if(y == mapSize && x > -1 && x < mapSize && map[x, y - 1] == -1)
					{
						SpawnObject(wallHorizontalDown, new Vector3(x,0,y - 0.5f));
						if(x == mapSize - 1 || map[x + 1, y - 1] == -1)
						{
							SpawnObject(wallFull, new Vector3(x + 0.5f,0,y - 0.5f));
						}
					}
					continue;
				}
				if(x <= mapSize - 2)
				{
					if(map[x,y] == -1 && map[x + 1,y] == -1)
					{
						SpawnObject(wallVerticalUp, new Vector3(x + 0.5f,0,y));
					}
				}
				if(y <= mapSize - 2)
				{
					if(map[x,y] == -1 && map[x,y + 1] == -1)
					{
						SpawnObject(wallHorizontalUp, new Vector3(x,0,y + 0.5f));
					}
				}
				if(x < mapSize - 1 
					&& y < mapSize - 1 
					&& map[x,y] == -1
					&& map[x,y + 1] == -1 
					&& map[x + 1, y] == -1 
					&& map[x + 1, y + 1] == -1)
				{
					SpawnObject(wallFull, new Vector3(x + 0.5f,0,y + 0.5f));
				}
				if(map[x,y] != -1 || tmpSpawner[x,y] == 1) continue;
				tmpSpawner[x,y] = 1;
				if(x < mapSize - 1 && y < mapSize - 1 && map[x + 1,y] == 0 && map[x, y + 1] == 0 && map[x + 1, y + 1] == -1)
				{
					tmpSpawner[x + 1, y + 1] = 1;
					SpawnObject(wallAngleLeft, new Vector3(x + 0.5f,0,y + 0.5f));
				}
				else if(x > 0 && y > 0 && map[x - 1,y] == 0 && map[x, y - 1] == 0 && map[x - 1, y - 1] == -1)
				{
					tmpSpawner[x - 1, y - 1] = 1;
					SpawnObject(wallAngleLeft, new Vector3(x - 0.5f,0,y - 0.5f));
				}
				else if(x < mapSize - 1 && y > 0 && map[x + 1,y] == 0 && map[x, y - 1] == 0 && map[x + 1, y - 1] == -1)
				{
					tmpSpawner[x + 1, y - 1] = 1;
					SpawnObject(wallAngleRight, new Vector3(x + 0.5f,0,y - 0.5f));
				}
				else if(x > 0 && y < mapSize - 1 && map[x - 1,y] == 0 && map[x, y + 1] == 0 && map[x - 1, y + 1] == -1)
				{
					tmpSpawner[x - 1, y + 1] = 1;
					SpawnObject(wallAngleRight, new Vector3(x - 0.5f,0,y + 0.5f));
				}
			}
		}
		
		// SET CHEST
		
		for(int x = 0; x < mapSize; x++)
		{
			for(int y = 0; y < mapSize; y++)
			{
				if(map[x,y] == 1)
				{
					GameObject g = Instantiate(chestPreviewPrefab, new Vector3(x, -0.5f, y), Quaternion.Euler(0,0,0));
					chestPreviewList.Add(g);
					SpawnObject(chestPrefab, new Vector3(x, 0, y), "Chest", g);
				}
			}
		}
		int[] playerStartPos = new int[2];
		playerStartPos[0] = startPos[0];
		playerStartPos[1] = startPos[1] - 1;
		mainS.startPos = startPos;
		mainS.endPos = endPos;
		mainS.SpawnPlayer(playerStartPos);
	}
	
	public void SetEdges()
	{
		GameObject g;
		
		g = (GameObject) Instantiate(wallHorizontalEdge, new Vector3(-1, 0, (levelChanger - 1f) / 2f), Quaternion.identity);
		g.transform.localScale = new Vector3(1, 1, levelChanger + 1);
		g.transform.parent = EdgesParent.transform;
		g.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2((levelChanger + 1) * 2, 1);
		
		g = (GameObject) Instantiate(wallHorizontalEdge, new Vector3(levelChanger, 0, (levelChanger - 1f) / 2f), Quaternion.identity);
		g.transform.localScale = new Vector3(1, 1, levelChanger + 1);
		g.transform.parent = EdgesParent.transform;
		g.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2((levelChanger + 1) * 2, 1);
		
		g = (GameObject) Instantiate(wallVerticalEdge, new Vector3((levelChanger - 1) / 2f, 0, levelChanger), Quaternion.identity);
		g.transform.localScale = new Vector3(levelChanger + 1, 1, 1);
		g.transform.parent = EdgesParent.transform;
		g.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2((levelChanger + 1) * 2, 1);
		
		g = (GameObject) Instantiate(wallVerticalEdge, new Vector3((levelChanger - 1) / 2f,0,-1), Quaternion.identity);
		g.transform.localScale = new Vector3(levelChanger + 1, 1, 1);
		g.transform.parent = EdgesParent.transform;
		g.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2((levelChanger + 1) * 2, 1);
		
		//SET FLOOR
		
		for(int x = -1; x <= mapSize; x++)
		{
			for(int y = -1; y <= mapSize; y++)
			{
				if(x == -1 || y == -1 || x == mapSize || y == mapSize)
				{
					if((x == -1 && y == -1) || (x == -1 && y == mapSize) || (x == mapSize && y == mapSize) || (x == mapSize && y == -1)) continue;
					float minus = 0f;
					if(x == -1 || x == mapSize)
					{
						if(x == -1) minus = -0.25f;
						else if(x == mapSize) minus = 0.25f;
						g = (GameObject) Instantiate(floorEdgeH, new Vector3(x - minus,-0.5f,y), Quaternion.identity);
						g.transform.parent = FloorParent.transform;
					}
					if(y == -1 || y == mapSize)
					{
						if(y == -1) minus = -0.25f;
						else if(y == mapSize) minus = 0.25f;
						g = (GameObject) Instantiate(floorEdgeV, new Vector3(x,-0.5f,y - minus), Quaternion.identity);
						g.transform.parent = FloorParent.transform;
					}
					continue;
				}
				if(map[x,y] == -1)
				{
					g = (GameObject) Instantiate(floorStable, new Vector3(x,-0.5f,y), Quaternion.identity);
					g.transform.parent = FloorParent.transform;
				}
				else
				{
					g = (GameObject) Instantiate(floorUnstable, new Vector3(x,-0.5f,y), Quaternion.identity);
					g.transform.parent = FloorParent.transform;
					g.name = "Floor" + x + "x" + y;
				}
			}
		}
	}
	
	#endregion
	
	#region ChangesFromMainScript
	
	public void ChangeChestPreview(int mode = 1) // set chest preview depending on mode (1 is for start and restart, 2 is for exiting)
	{
		if(mode == 1)
		{
			for(int i = 0; i < mainS.chestCollected.Count; i++)
			{
				int x = (int)mainS.chestCollected[i].x;
				int y = (int)mainS.chestCollected[i].y;
				tex.SetPixel(x,y, new UnityEngine.Color(0.95f, 0.95f, 0.95f)); // set pixel on preview texture (texture has Point Filter so it will be huge rectangle)
			}
			for(int x = 0; x < mapSize; x++)
			{
				for(int y = 0; y < mapSize; y++)
				{
					if(map[x,y] == 1) 
					{
						Vector2 v = new Vector2(x,y);
						if(!mainS.chestCollected.Contains(v))
						{
							GameObject g = Instantiate(chestPreviewPrefab, new Vector3(x, -0.5f, y), Quaternion.Euler(0,0,0));
							chestPreviewList.Add(g);
						}
					}
				}
			}
			tex.filterMode = FilterMode.Point; // set texture filter to point for no AA
			tex.Apply();
			previewObject.GetComponent<Renderer>().materials[0].mainTexture = tex;
		}
		else
		{
			for(int i = 0; i < chestList.Count; i++)
			{
				if(chestList[i] != null) Destroy(chestList[i]); // destory chests while exit
			}
			for(int x = 0; x < mapSize; x++)
			{
				for(int y = 0; y < mapSize; y++)
				{
					if(map[x,y] == 1) 
					{
						GameObject g = Instantiate(chestPreviewPrefab, new Vector3(x, -0.5f, y), Quaternion.Euler(0,0,0));
						chestPreviewList.Add(g);
						SpawnObject(chestPrefab, new Vector3(x, 0, y), "Chest", g); // spawn new chests
						tex.SetPixel(x,y, new UnityEngine.Color(0.98f, 0.65f, 0.15f));
					}
				}
			}
			tex.filterMode = FilterMode.Point;
			tex.Apply();
			previewObject.GetComponent<Renderer>().materials[0].mainTexture = tex;
		}
	}
	
	#endregion
	
	#region Pathfinding (change Vector2 to other object (or create own Vector2 class) to work outside of Unity)
	
	//Find path, use this if player used Pathfinder tip (obviously list of points form start to end)
	public List<Vector2> FindPathFromPoint(Vector2 point) 
	{
		int[,] mazer = new int[mapSize, mapSize];
		Queue<Vector2> kolejka = new Queue<Vector2>();
		kolejka.Enqueue(point);
		int index = 1;
		mazer[(int)point.x, (int)point.y] = index;
		Vector2 lastCell = new Vector2(0,0);
		while(kolejka.Count > 0)
		{
			Vector2 cell = kolejka.Dequeue();
			index = mazer[(int)cell.x, (int)cell.y];
			if(cell.x == endPos[0] && cell.y == 0)
			{
				mazer[(int)cell.x, (int)cell.y] = index;
				lastCell = new Vector2(cell.x, cell.y);
				kolejka.Clear();
				break;
			}
			if(CheckForPathCell(1, cell, mazer))
			{
				mazer[(int)cell.x + 1, (int)cell.y] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x + 1, cell.y));
			}
			if(CheckForPathCell(2, cell, mazer))
			{
				mazer[(int)cell.x, (int)cell.y + 1] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x, cell.y + 1));
			}
			if(CheckForPathCell(3, cell, mazer))
			{
				mazer[(int)cell.x - 1, (int)cell.y] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x - 1, cell.y));
			}
			if(CheckForPathCell(4, cell, mazer))
			{
				mazer[(int)cell.x, (int)cell.y - 1] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x, cell.y - 1));
			}
		}
		List<Vector2> path = new List<Vector2>();
		path.Add(lastCell);
		while(index > 1)
		{
			index = mazer[(int)lastCell.x, (int)lastCell.y];
			if(lastCell.x < mapSize - 1 && mazer[(int)lastCell.x + 1, (int)lastCell.y] == index - 1)
			{
				index--;
				lastCell = new Vector2(lastCell.x + 1, lastCell.y);
				path.Add(lastCell);
			}
			else if(lastCell.y < mapSize - 1 && mazer[(int)lastCell.x, (int)lastCell.y + 1] == index - 1)
			{
				index--;
				lastCell = new Vector2(lastCell.x, lastCell.y + 1);
				path.Add(lastCell);
			}
			else if(lastCell.x > 0 && mazer[(int)lastCell.x - 1, (int)lastCell.y] == index - 1)
			{
				index--;
				lastCell = new Vector2(lastCell.x - 1, lastCell.y);
				path.Add(lastCell);
			}
			else if(lastCell.y > 0 && mazer[(int)lastCell.x, (int)lastCell.y - 1] == index - 1)
			{
				index--;
				lastCell = new Vector2(lastCell.x, lastCell.y - 1);
				path.Add(lastCell);
			}
		}
		path.Reverse();
		return path;
	}
	
	private bool CheckForPathCell(int mode, Vector2 actCell, int[,] maze) // return true if froor at this cell isn't destroyed and player can reach it
	{
		int x = (int)actCell.x;
		int y = (int)actCell.y;
		
		if(mode == 1)
		{
			if(x >= mapSize - 1) return false;
			if(maze[x + 1, y] != 0) return false;
			if(map[x + 1, y] == -1) return false;
		}
		else if(mode == 2)
		{
			if(y >= mapSize - 1) return false;
			if(maze[x, y + 1] != 0) return false;
			if(map[x, y + 1] == -1) return false;
		}
		else if(mode == 3)
		{
			if(x <= 0) return false;
			if(maze[x - 1, y] != 0) return false;
			if(map[x - 1, y] == -1) return false;
		}
		else if(mode == 4)
		{
			if(y <= 0) return false;
			if(maze[x, y - 1] != 0) return false;
			if(map[x, y - 1] == -1) return false;
		}
		
		return true;
	}
	
	//Find path, use this if player used Teleprt, get random cell from list which is (80% - teleport level) of distance between player and level end 
	public List<Vector2> FindPathTeleport(int telLv)
	{
		float timer = Time.time;
		int[,] maze = new int[mapSize, mapSize];
		
		Queue<Vector2> kolejka = new Queue<Vector2>();
		kolejka.Enqueue(new Vector2(endPos[0], endPos[1] + 1));
		int index = 1;
		maze[endPos[0], endPos[1] + 1] = index;
		while(kolejka.Count > 0)
		{
			Vector2 cell = kolejka.Dequeue();
			index = maze[(int)cell.x, (int)cell.y];
			if(CheckForPathCell(1, cell, maze))
			{
				maze[(int)cell.x + 1, (int)cell.y] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x + 1, cell.y));
			}
			if(CheckForPathCell(2, cell, maze))
			{
				maze[(int)cell.x, (int)cell.y + 1] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x, cell.y + 1));
			}
			if(CheckForPathCell(3, cell, maze))
			{
				maze[(int)cell.x - 1, (int)cell.y] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x - 1, cell.y));
			}
			if(CheckForPathCell(4, cell, maze))
			{
				maze[(int)cell.x, (int)cell.y - 1] = index + 1;
				kolejka.Enqueue(new Vector2(cell.x, cell.y - 1));
			}
			index++;
		}
		
		Vector3 pPos = mainS.player.transform.position;
		float xP = pPos.x;
		float zP = pPos.z;
		if(pPos.x < 0) xP = 0;
		if(pPos.x > mapSize - 1) xP = mapSize - 1;
		if(pPos.z < 0) zP = 0;
		if(pPos.z > mapSize - 1) zP = mapSize - 1;
		Vector2 c = new Vector2((float)Math.Round(xP), (float)Math.Round(zP));
		int cellIndex = maze[(int)c.x, (int)c.y];
		int targetCellIndex = (int)Math.Ceiling(cellIndex * ((80f - (telLv * 2f)) * 0.01f));
		if(targetCellIndex <= 1) targetCellIndex = 2;
		List<Vector2> result = new List<Vector2>();
		
		for(int i = 0; i < result.Count; i++)
		{
			Vector3 v = new Vector3(result[i].x, pPos.y, result[i].y);
			if(Vector3.Distance(pPos, v) < 2f)
			{
				result.RemoveAt(i);
				break;
			}
		}
		
		for(int x = 0; x < mapSize; x++)
		{
			for(int y = 0; y < mapSize; y++)
			{
				if(map[x,y] == 0 && maze[x,y] == targetCellIndex) result.Add(new Vector2(x,y));
			}
		}
		if(result.Count == 0)
		{
			for(int x = 0; x < mapSize; x++)
			{
				for(int y = 0; y < mapSize; y++)
				{
					if(map[x,y] == 0 && maze[x,y] == targetCellIndex - 1) result.Add(new Vector2(x,y));
				}
			}
		}
		return result;
	}
	
	#endregion
}



















