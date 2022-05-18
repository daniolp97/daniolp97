using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class MainScript : MonoBehaviour
{
	[HideInInspector] public bool menuShowed = false; // sklep między poziomami
	[HideInInspector] public bool gameMenu = false; // w czasie grania poziomu
	[HideInInspector] public GeneratorScript generator;
	
	public GameObject playerPrefab;
	public GameObject mainCamera;
	public GameObject cameraToPlayerRot;
	public GameObject cameraToMapRot;
	public GameObject cameraToEndRot;
	public GameObject cameraToEndRotCam;
	
	public GameObject arrowStartPrefab;
	public GameObject arrowEndPrefab;
	
	[HideInInspector] public int level;
	private int levelChanger;
	[HideInInspector] public int stageInLevel;
	[HideInInspector] public int gold;
	[HideInInspector] public int showPath;
	[HideInInspector] public int levitation;
	[HideInInspector] public int teleport;
	[HideInInspector] public int showPathLv;
	[HideInInspector] public int levitationLv;
	[HideInInspector] public int teleportLv;
	[HideInInspector] public int coinsLv;
	[HideInInspector] public string savedMaze;
	
	[HideInInspector] public int[,] maze;
	[HideInInspector] public GameObject player;
	[HideInInspector] public Vector3 startChangeCamPos;
	[HideInInspector] public Vector3 startCamPos;
	[HideInInspector] public Vector3 endCamPos;
	[HideInInspector] public Vector3 mapCamPos;
	[HideInInspector] public Vector3 playerCamPos;
	[HideInInspector] public MoveScript moveS;
	
	[HideInInspector] public bool cameraFlyToPlayer;
	[HideInInspector] public bool cameraFlyToMap;
	[HideInInspector] public bool floorShow;
	[HideInInspector] public bool cameraFlyToEnd;
	[HideInInspector] public bool cameraFlyToMiddleLevel;
	[HideInInspector] public bool cameraFlyToStartLevel;
	
	private int quitCounter = 0;
	
	private GameObject arrowEndObject;
	private bool skipped;
	private GameObject arrowStartObject;
	private Vector3 endCamPlayerPos;
	private bool readyToChangeLevel;
	private GameObject endTrigger;
	public GameObject endTriggerObject;
	public GameObject FloorParent;
	public GameObject WallsParent;
	public GameObject EdgesParent;
	
	[HideInInspector] public int[] startPos;
	[HideInInspector] public int[] endPos;
	
	private float maxCamTimer = 0.0f;
	public GameObject podgladObject;
	public GameObject podgladObjectBack;
	public GameObject pathFinderPrefab;
	private float timer = 0.0f;
	private bool startSetted = false;
	private float deltaTime = 0.0f;
	private int askingForAd = 0;
	private string askingForAdText = "";
	
	private bool levelReady = false;
	private float secsToSeeMaze = 2;
	private bool camerFlySkip = false;
	private Vector3 cameraSkipStartPos;
	private GameObject cameraSkipStartRot;
	
	private List<Vector2> listToTeleport;
	[HideInInspector] public bool isLevitating;
	private float levitationTimer = 0.0f;
	
	[HideInInspector] public Styler styler;
	public GUIStyle fpsStyle;
	private GameObject pathFinder;
	private bool teleporting;
	public bool showRestMenu;
	private bool askingForDelete;
	private float skipSpeed;
	
	private int showPathGold = 1000;
	private int teleportGold = 1200;
	private int levitationGold = 800;
	private int showPathGoldMulti = 650; // to razy level, tyle golda
	private int levitationGoldMulti = 550; // to razy level, tyle golda
	private int teleportGoldMulti = 800; // to razy level, tyle golda
	
	private System.Random rand;
	public GameObject coinsTextPrefab;
	
	private List<GameObject> UnstableFloorsList;
	private int description = 0;
	
	private int showPathGoldForLevel;
	private int levitationGoldForLevel;
	private int teleportGoldForLevel;
	
	public GameObject teleportAnimPrefab;
	
	private float sW = 0.0f;
	private float sH = 0.0f;
	private bool hideGuiForAd = false;
	
	private string notifDesc = "";
	public bool showGoldNotif = false;
	public float notifTime = 0.0f;
	private float nTimer = 0.0f;
	
	private List<GameObject> oldEdges;
	private string errorAd = "";
	private bool videoAdLoaded = false;
	
	private BannerView bannerView;
	private string bannerId = "ca-app-pub-9950335474064913/7939922391";
	private string coinAmountAdId = "ca-app-pub-9950335474064913/8517989970";
	private string coinLevelAdId = "ca-app-pub-9950335474064913/8406309335";
	private RewardedAd coinAmountAd;
	private RewardedAd coinLevelAd;
	
	private string testVideoAD = "ca-app-pub-3940256099942544/5224354917";
	private string testBannerAD = "ca-app-pub-3940256099942544/6300978111";
	
	private bool isConnection = false;
	private float timerCheckConn = 0.0f;
	
	[HideInInspector] public List<Vector2> chestCollected;
	
	[HideInInspector] public int[] collectables;
	/* 0 - gold
	   1 - path
	   2 - tele
	   3 - lev
	*/
	
	void Awake()
	{
		sW = Screen.width;
		sH = Screen.height;
	}
	
	void Start()
	{
		collectables = new int[4];
		timerCheckConn = 0.0f;
		isConnection = false;
		quitCounter = 0;
		chestCollected = new List<Vector2>();
		oldEdges = new List<GameObject>();
		askingForDelete = false;
		showGoldNotif = false;
		showRestMenu = false;
		teleporting = false;
		styler = transform.GetChild(0).gameObject.GetComponent<Styler>();
		UnstableFloorsList = new List<GameObject>();
		isLevitating = false;
		listToTeleport = new List<Vector2>();
		rand = new System.Random();
		readyToChangeLevel = false;
		menuShowed = false;
		gameMenu = false;
		floorShow = false;
		deltaTime = 0.0f;
		startSetted = false;
		timer = 0.0f;
		cameraFlyToPlayer = false;
		LoadPlayerData();
        generator = this.GetComponent<GeneratorScript>();
		generator.mainS = this.GetComponent<MainScript>();
		NewMaze();
		MobileAds.Initialize(initStatus => { });
		LoadBanner();
		LoadVideoAd();
		hideGuiForAd = false;
		errorAd = "";
		askingForAdText = "";
		askingForAd = 0;
	}
	
	void LoadBanner()
	{
		AdSize adSize = new AdSize(320, 50);
		bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.BottomRight);
		this.bannerView.OnAdClosed += this.HandleOnAdClosedBanner;
		this.bannerView.OnAdLoaded += this.HandleOnAdLoadedBanner;
		//bannerView = new BannerView(testBannerAD, AdSize.Banner, AdPosition.BottomRight);
		AdRequest requestBanner = new AdRequest.Builder().Build();
		bannerView.LoadAd(requestBanner);
	}
	
	public void HandleOnAdLoadedBanner(object sender, EventArgs args)
    {
        bannerView.Show();
    }

	public void HandleOnAdClosedBanner(object sender, EventArgs args)
    {
        bannerView.Hide();
    }
	
	void LoadVideoAd()
	{
		coinAmountAd = new RewardedAd(coinAmountAdId);
		//coinAmountAd = new RewardedAd(testVideoAD);
		coinAmountAd.OnAdLoaded += HandleRewardedAdLoaded;
		coinAmountAd.OnUserEarnedReward += HandleUserEarnedReward;
		coinAmountAd.OnAdClosed += HandleRewardedAdClosed;
		coinAmountAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		coinAmountAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		AdRequest requestAmount = new AdRequest.Builder().Build();
		coinAmountAd.LoadAd(requestAmount);
		
		coinLevelAd = new RewardedAd(coinLevelAdId);
		//coinLevelAd = new RewardedAd(testVideoAD);
		coinLevelAd.OnAdLoaded += HandleRewardedAdLoaded;
		coinLevelAd.OnUserEarnedReward += HandleUserEarnedReward;
		coinLevelAd.OnAdClosed += HandleRewardedAdClosed;
		coinLevelAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		coinLevelAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		AdRequest requestLevel = new AdRequest.Builder().Build();
		coinLevelAd.LoadAd(requestLevel);
	}
	
	public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        askingForAd = 0;
		hideGuiForAd = false;
		LoadVideoAd();
	}
	
	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
		if(askingForAd != 0)
		{
			errorAd = "Ad cannot be showed. Error code : 53331";
			askingForAd = 0;
			hideGuiForAd = false;
			LoadVideoAd();
		}
    }
	
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        videoAdLoaded = true;
    }
	
	public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
		if(askingForAd != 0)
		{
			errorAd = "Ad cannot be showed. Error code : 53332";
			askingForAd = 0;
			hideGuiForAd = false;
			LoadVideoAd();
		}
    }

	
	public void HandleUserEarnedReward(object sender, Reward args)
    {
        if(askingForAd == 1)
		{
			gold += 500;
			PlayerPrefs.SetInt("Gold", gold);
		}
		if(askingForAd == 2)
		{
			coinsLv += 2;
			PlayerPrefs.SetInt("CoinsLevel", coinsLv);
		}
		if(askingForAd == 1) errorAd = "Added 500 coins!";
		else if(askingForAd == 2) errorAd = "You will earn 2% more for every stage!";
		askingForAd = 0;
		hideGuiForAd = false;
		LoadVideoAd();
    }

	
	void SaveGame(int mode = 1)
	{
		if(mode == 1)
		{
			savedMaze = "-";
		}
		PlayerPrefs.SetString("SavedMaze", savedMaze);
		PlayerPrefs.SetInt("Stage", stageInLevel);
		PlayerPrefs.SetInt("Level", level);
		PlayerPrefs.SetInt("Gold", gold);
		PlayerPrefs.SetInt("ShowPath", showPath);
		PlayerPrefs.SetInt("Levitation", levitation);
		PlayerPrefs.SetInt("Teleport", teleport);
		PlayerPrefs.SetInt("ShowPathLevel", showPathLv);
		PlayerPrefs.SetInt("LevitationLevel", levitationLv);
		PlayerPrefs.SetInt("CoinsLevel", coinsLv);
		PlayerPrefs.SetInt("TeleportLevel", teleportLv);
		PlayerPrefs.Save();
	}
	
	void ChangeLevel()
	{
		for(int i = 0; i < WallsParent.transform.childCount; i++)
		{
			Destroy(WallsParent.transform.GetChild(i).gameObject);
		}
		for(int i = 0; i < FloorParent.transform.childCount; i++)
		{
			Destroy(FloorParent.transform.GetChild(i).gameObject);
		}
		for(int i = 0; i < EdgesParent.transform.childCount; i++)
		{
			Destroy(EdgesParent.transform.GetChild(i).gameObject);
		}
		Destroy(player);
		generator.InitializeMaze(level, savedMaze.Length > 10);
		readyToChangeLevel = true;
		isLevitating = false;
	}
	
	void EraseOldEdges()
	{
		for(int i = 0; i < oldEdges.Count; i++)
		{
			if(oldEdges[i] == null) continue;
			oldEdges.Add(oldEdges[i]);
		}
	}
	
	void PosLerper(Vector3 startPos, GameObject objectToMove, Vector3 destinationPoint, float speed)
	{
		float maxDist = Vector3.Distance(startPos, destinationPoint);
		float actDist = Vector3.Distance(objectToMove.transform.position, destinationPoint);
		if(maxDist == actDist) actDist = maxDist - 0.1f;
		float s = (((actDist / maxDist) - 1) * -1) * speed;
		objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, destinationPoint, s);
	}
	
	void RotSlerper(GameObject startRot, GameObject objectToRotate, GameObject destinationRotate, float speed)
	{
		float maxDist = Vector3.Distance(startRot.transform.rotation.eulerAngles, destinationRotate.transform.rotation.eulerAngles);
		float actDist = Vector3.Distance(objectToRotate.transform.rotation.eulerAngles, destinationRotate.transform.rotation.eulerAngles);
		if(maxDist == actDist) actDist = maxDist - 0.1f;
		float s = (((actDist / maxDist) - 1) * -1) * speed;
		objectToRotate.transform.rotation = Quaternion.Slerp(objectToRotate.transform.rotation, destinationRotate.transform.rotation, s);
	}
	
    void NewMaze()
    {
		generator.InitializeMaze(level, savedMaze.Length > 10);
    }
	
	void LoadPlayerData()
	{
		stageInLevel = PlayerPrefs.GetInt("Stage");
		if(stageInLevel >= 1)
		{
			stageInLevel = PlayerPrefs.GetInt("Stage");
			level = PlayerPrefs.GetInt("Level");
			gold = PlayerPrefs.GetInt("Gold");
			showPath = PlayerPrefs.GetInt("ShowPath");
			levitation = PlayerPrefs.GetInt("Levitation");
			teleport = PlayerPrefs.GetInt("Teleport");
			showPathLv = PlayerPrefs.GetInt("ShowPathLevel");
			levitationLv = PlayerPrefs.GetInt("LevitationLevel");
			teleportLv = PlayerPrefs.GetInt("TeleportLevel");
			coinsLv = PlayerPrefs.GetInt("CoinsLevel");
			savedMaze = PlayerPrefs.GetString("SavedMaze");
		}
		else
		{
			stageInLevel = 1;
			level = 0;
			gold = 0;
			showPath = 0; // pokaż ścieżkę do końca (zależne od ulepszenia)
			levitation = 0; // przechodzenie po usuniętach podłożach (zależne od ulepszenia)
			teleport = 0; // teleportacja do losowego punktu (z którego da się dojść do końca), zależne od ulepszenia
			showPathLv = 1;
			levitationLv = 1;
			teleportLv = 1;
			coinsLv = 0;
			savedMaze = "-";
			PlayerPrefs.SetInt("Stage", 1);
		}
		levelChanger = level + 15;
		teleportGoldForLevel = teleportGoldMulti * teleportLv;
		levitationGoldForLevel = levitationGoldMulti * levitationLv;
		showPathGoldForLevel = showPathGoldMulti * showPathLv;
	}
	
	public void SetMaze(int[,] m)
	{
		maze = m;
		generator.SpawnMaze();
		UnstableFloorsList.Clear();
		string mazeToSave = "";
		for(int i = 0; i < FloorParent.transform.childCount; i++)
		{
			if(FloorParent.transform.GetChild(i).gameObject.tag != "UnstableFloor") continue;
			UnstableFloorsList.Add(FloorParent.transform.GetChild(i).gameObject);
		}
		WallsParent.transform.position = new Vector3(0,-3,0);
		startChangeCamPos = new Vector3(startPos[0], -0.1f, startPos[1] + 5);
		levelReady = true;
		GC.Collect();
		if(cameraFlyToMiddleLevel || cameraFlyToEnd)
		{
			mainCamera.transform.position = new Vector3(endCamPos.x, endCamPos.y + 25, endCamPos.z - 1.5f);
			mainCamera.transform.rotation = Quaternion.identity;
			Fly("STARTLEVEL");
		}
	}
	
	void CheckForNet()
	{
		timerCheckConn += Time.deltaTime;
		if(timerCheckConn > 5)
		{
			if(Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || 
			   Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork) 
			{
			   isConnection = true;
			}
			else
			{
				isConnection = false;
			}
		}
	}
	
	void Update()
	{
		sW = Screen.width;
		sH = Screen.height;
		if(quitCounter > 0)
		{
			quitCounter++;
			if(quitCounter > 180) Application.Quit();
		}
		CheckForNet();
		if(camerFlySkip)
		{
			maxCamTimer += Time.deltaTime;
			if(!startSetted) 
			{
				SetStart();
				skipSpeed = mainCamera.transform.position.y / 10f;
				if(skipSpeed < 1) skipSpeed = 1;
			}
			PosLerper(cameraSkipStartPos, mainCamera, playerCamPos, skipSpeed);
			RotSlerper(cameraSkipStartRot, mainCamera, cameraToPlayerRot, skipSpeed);
			if(Vector3.Distance(mainCamera.transform.position, playerCamPos) < 5f)
			{
				floorShow = true;
			}
			if(Vector3.Distance(mainCamera.transform.position, playerCamPos) < 0.001f)
			{
				camerFlySkip = false;
				if(cameraSkipStartRot != null) Destroy(cameraSkipStartRot);
				mainCamera.transform.position = playerCamPos;
				mainCamera.transform.rotation = Quaternion.identity;
				mainCamera.transform.rotation = Quaternion.Euler(60,180,0);
				mainCamera.transform.parent = player.transform;
				moveS.canMove = true;
				timer = 0.0f;
			}
		}
		else if(cameraFlyToPlayer)
		{
			maxCamTimer += Time.deltaTime;
			timer += Time.deltaTime;
			if(timer > secsToSeeMaze)
			{
				if(!startSetted) SetStart();
				PosLerper(mapCamPos, mainCamera, playerCamPos, 0.1f);
				RotSlerper(cameraToMapRot, mainCamera, cameraToPlayerRot, 0.1f);
				if(Vector3.Distance(mainCamera.transform.position, playerCamPos) < 10f)
				{
					floorShow = true;
				}
				if(Vector3.Distance(mainCamera.transform.position, playerCamPos) < 0.001f)
				{
					cameraFlyToPlayer = false;
					mainCamera.transform.position = playerCamPos;
					mainCamera.transform.rotation = Quaternion.identity;
					mainCamera.transform.rotation = Quaternion.Euler(60,180,0);
					mainCamera.transform.parent = player.transform;
					moveS.canMove = true;
					showRestMenu = true;
					timer = 0.0f;
				}
			}
		}
		else if(cameraFlyToMap)
		{
			maxCamTimer += Time.deltaTime;
			PosLerper(startCamPos, mainCamera, mapCamPos, 0.1f);
			RotSlerper(gameObject, mainCamera, cameraToMapRot, 0.325f);
			if(Vector3.Distance(mainCamera.transform.position, mapCamPos) < 0.001f)
			{
				cameraFlyToMap = false;
				mainCamera.transform.position = mapCamPos;
				Fly("PLAYER");
			}
		}
		else if(cameraFlyToEnd)
		{
			maxCamTimer += Time.deltaTime;
			PosLerper(endCamPlayerPos, mainCamera, endCamPos, 0.075f);
			RotSlerper(cameraToEndRotCam, mainCamera, cameraToEndRot, 0.1f);
			if(Vector3.Distance(mainCamera.transform.position, endCamPos) < 0.001f)
			{
				ChangeLevel();
				cameraFlyToEnd = false;
				mainCamera.transform.position = new Vector3(endCamPos.x, endCamPos.y - 10000, endCamPos.z + 2);
				mainCamera.transform.rotation = Quaternion.identity;
				Fly("MIDDLE");
			}
		}
		else if(cameraFlyToMiddleLevel)
		{
			maxCamTimer += Time.deltaTime;
			PosLerper(endCamPlayerPos, mainCamera, new Vector3(endCamPos.x, endCamPos.y, endCamPos.z - 2), 0.05f);
			if(Vector3.Distance(mainCamera.transform.position, new Vector3(endCamPos.x, endCamPos.y, endCamPos.z - 2)) < 0.001f)
			{
				cameraFlyToMiddleLevel = false;
				mainCamera.transform.position = new Vector3(endCamPos.x, endCamPos.y + 25, endCamPos.z - 1.5f);
				mainCamera.transform.rotation = Quaternion.identity;
				EraseOldEdges();
			}  
		}
		else if(cameraFlyToStartLevel)
		{
			maxCamTimer += Time.deltaTime;
			PosLerper(startChangeCamPos, mainCamera, startCamPos, 0.075f);
			if(Vector3.Distance(mainCamera.transform.position, startCamPos) < 0.001f)
			{
				mainCamera.transform.position = startCamPos;
				cameraFlyToStartLevel = false;
				menuShowed = true;
			}
		}
		if(floorShow)
		{
			if(!WallsParent.activeSelf) WallsParent.SetActive(true);
			PosLerper(new Vector3(0,-3,0),  WallsParent, new Vector3(0,0,0), 0.2f);
			if(podgladObject.activeSelf)
			{
				if(Vector3.Distance(WallsParent.transform.position, new Vector3(0,0,0)) < 1f)
				{
					podgladObject.SetActive(false);
					podgladObjectBack.SetActive(false);
					FloorParent.SetActive(true);
					for(int i = 0; i < generator.chestPreviewList.Count; i++)
					{
						if(generator.chestPreviewList[i] != null) Destroy(generator.chestPreviewList[i]);
					}
					generator.chestPreviewList.Clear();
				}
			}
			if(Vector3.Distance(WallsParent.transform.position, new Vector3(0,0,0)) < 0.01f)
			{
				WallsParent.transform.position = new Vector3(0,0,0);
				floorShow = false;
				showRestMenu = true;
			}
		}
		if(readyToChangeLevel && !generator.generating && !generator.generatorThread.IsAlive)
		{
			readyToChangeLevel = false;
			AfterIntro();
		}
	}
	
	public void SetStart()
	{
		startSetted = true;
		if(arrowStartObject != null) Destroy(arrowStartObject);
		if(arrowEndObject != null) Destroy(arrowEndObject);
		player = (GameObject) Instantiate(playerPrefab, new Vector3(startPos[0], 0f, startPos[1] - 1), Quaternion.identity);
		playerCamPos = new Vector3(player.transform.position.x, player.transform.position.y + 4.5f, player.transform.position.z + 2);
		moveS = player.GetComponent<MoveScript>();
	}
	
	public void Fly(string flyTo)
	{
		cameraFlyToMap = false;
		cameraFlyToPlayer = false;
		cameraFlyToEnd = false;
		cameraFlyToMiddleLevel = false;
		cameraFlyToStartLevel = false;
		camerFlySkip = false;
		timer = 0;
		maxCamTimer = 0;
		if(flyTo == "MAP") cameraFlyToMap = true;
		else if(flyTo == "PLAYER") cameraFlyToPlayer = true;
		else if(flyTo == "END") cameraFlyToEnd = true;
		else if(flyTo == "MIDDLE") cameraFlyToMiddleLevel = true;
		else if(flyTo == "STARTLEVEL") cameraFlyToStartLevel = true;
		else if(flyTo == "SKIP") camerFlySkip = true;
	}
	
	public void EndLevel()
	{
		if(endTrigger != null) Destroy(endTrigger);
		chestCollected.Clear();
		for(int i = 0; i < generator.chestList.Count; i++)
		{
			if(generator.chestList[i] != null) Destroy(generator.chestList[i]);
		}
		int addCoins = (int)Mathf.Round((levelChanger * levelChanger - (moveS.cellsMoved * 5f)) * ((coinsLv / 100f) + 1));
		int baseCoins = (int)Mathf.Round(levelChanger * levelChanger / 5f);
		if(addCoins < baseCoins) addCoins = baseCoins;
		showGoldNotif = true;
		if(pathFinder != null) Destroy(pathFinder);
		for(int i = 0; i < generator.chestPreviewList.Count; i++)
		{
			if(generator.chestPreviewList[i] != null) Destroy(generator.chestPreviewList[i]);
		}
		generator.chestPreviewList.Clear();
		nTimer = 5;
		notifDesc = "Stage " + level + "-" + stageInLevel + " clear!" + "\n You earned " + addCoins + " coins";
		gold += addCoins;
		PlayerPrefs.SetInt("Gold", gold);
		startSetted = false;
		mainCamera.transform.parent = null;
		cameraToEndRotCam.transform.rotation = mainCamera.transform.rotation;
		player.GetComponent<MoveScript>().canMove = false;
		endCamPlayerPos = mainCamera.transform.position;
		showRestMenu = false;
		levitationTimer = 0.0f;
		if(collectables.Length == 4)
		{
			collectables[0] = 0;
			collectables[1] = 0;
			collectables[2] = 0;
			collectables[3] = 0;
		}
		if(stageInLevel >= 2)
		{
			level++;
			levelChanger = level + 15;
			stageInLevel = 1;
		}
		else stageInLevel++;
		SaveGame(1);
		Fly("END");
		ClearFloor();
	}
	
	public void SpawnPlayer(int[] startPos)
	{
		startChangeCamPos = new Vector3(startPos[0], -0.1f, startPos[1] + 2);
		startCamPos = new Vector3(startPos[0] - 0.1f, 0.15f, startPos[1] - 0.5f);
		mapCamPos = new Vector3((float)levelChanger / 2f, levelChanger + 4, (float)levelChanger / 2f);
		endCamPos = new Vector3(endPos[0], -0.1f, endPos[1] + 1);
	}
	
	public void AfterIntro()
	{
		mainCamera.transform.position = startChangeCamPos;
		gameMenu = false;
		podgladObject.SetActive(true);
		podgladObjectBack.SetActive(true);
		Fly("STARTLEVEL");
	}
	
	public void SpawnArrows()
	{
		arrowStartObject = (GameObject) Instantiate(arrowStartPrefab, new Vector3(startPos[0], -0.4f, startPos[1] - 1), Quaternion.identity);
		arrowEndObject = (GameObject) Instantiate(arrowEndPrefab, new Vector3(endPos[0], -0.4f, endPos[1] + 1), Quaternion.identity);
		endTrigger = (GameObject) Instantiate(endTriggerObject, new Vector3(endPos[0], 0, endPos[1]), Quaternion.identity);
	}
	
	public void ClearFloor()
	{
		GameObject g;
		for(int i = 0; i < UnstableFloorsList.Count; i++)
		{
			if(UnstableFloorsList[i].gameObject == null) continue;
			UnstableFloorsList[i].GetComponent<FloorScript>().RestartFloor();
		}
	}
	
	public void PerformExit()
	{
		ClearFloor();
		if(collectables.Length == 4)
		{
			if(collectables[0] > 0) gold -= collectables[0];
			if(collectables[1] > 0) showPath -= collectables[1];
			if(collectables[2] > 0) teleport -= collectables[2];
			if(collectables[3] > 0) levitation -= collectables[3];
			collectables[0] = 0;
			collectables[1] = 0;
			collectables[2] = 0;
			collectables[3] = 0;
		}
		chestCollected.Clear();
		generator.ChangeChestPreview(2);
		timer = 0.0f;
		startSetted = false;
		WallsParent.transform.position = new Vector3(0,-3,0);
		WallsParent.SetActive(false);
		podgladObject.SetActive(true);
		FloorParent.SetActive(false);
		mainCamera.transform.parent = null;
		showRestMenu = false;
		Destroy(player);
		mainCamera.transform.position = startCamPos;
		mainCamera.transform.rotation = Quaternion.identity;
		isLevitating = false;
		if(pathFinder != null) Destroy(pathFinder);
		teleporting = false;
		if(endTrigger != null) Destroy(endTrigger);
		gameMenu = false;
		menuShowed = true;
	}
	
	public void PerformRestart()
	{
		ClearFloor();
		timer = 0.0f;
		isLevitating = false;
		generator.ChangeChestPreview();
		if(pathFinder != null) Destroy(pathFinder);
		teleporting = false;
		startSetted = false;
		showRestMenu = false;
		WallsParent.transform.position = new Vector3(0,-3,0);
		WallsParent.SetActive(false);
		podgladObject.SetActive(true);
		podgladObjectBack.SetActive(true);
		FloorParent.SetActive(false);
		mainCamera.transform.parent = null;
		if(endTrigger != null) Destroy(endTrigger);
		Destroy(player);
		SpawnArrows();
		skipped = false;
		Fly("MAP");
	}
	
	public void FloorErased(int x, int y)
	{
		generator.map[x,y] = 2;
		maze[x,y] = 2;
	}
	
	public void LevitationSetFloor(int mode)
	{
		if(mode == 1)
		{
			for(int i = 0; i < UnstableFloorsList.Count; i++)
			{
				if(UnstableFloorsList[i] == null) continue;
				UnstableFloorsList[i].GetComponent<FloorScript>().PlayerLevitation(1);
			}
		}
		else
		{
			for(int i = 0; i < UnstableFloorsList.Count; i++)
			{
				if(UnstableFloorsList[i] == null) continue;
				UnstableFloorsList[i].GetComponent<FloorScript>().PlayerLevitation(2);
			}
		}
	}
	
	public void CreatePathFinder()
	{
		if(pathFinder != null) pathFinder = null;
		Vector3 pl = player.transform.position;
		Vector3 pfPos = new Vector3(pl.x, 0.5f, pl.z);
		pathFinder = (GameObject) Instantiate(pathFinderPrefab, pfPos, Quaternion.identity);
		List<Vector2> finder = new List<Vector2>();
		Vector3 pPos = player.transform.position;
		Vector2 playerCell = new Vector2((int)Mathf.Round(pPos.x), (int)Mathf.Round(pPos.z));
		finder = generator.FindPathFromPoint(playerCell);
		pathFinder.GetComponent<PathFinder>().InitializePathFinder(finder, showPathLv);
	}
	
	public void CollectedChest(int prize, Vector3 pos)
	{
		if(prize == 4) 
		{
			notifDesc = "+1 Leviation";
			levitation++;
			collectables[3]++;
		}
		else if(prize == 3) 
		{
			notifDesc = "+1 Teleport";
			teleport++;
			collectables[2]++;
		}
		else if(prize == 2) 
		{
			notifDesc = "+1 Patcher";
			showPath++;
			collectables[1]++;
		}
		else if(prize == 1)
		{
			int amount = rand.Next(500,1200);
			notifDesc = "+" + amount + " Coins";
			gold += amount;
			collectables[0] += amount;
		}
		Vector3 plPos = player.transform.position;
		GameObject g = Instantiate(coinsTextPrefab, new Vector3(plPos.x, plPos.y + 1, plPos.z), Quaternion.identity);
		g.GetComponent<CoinsTextScript>().SetText(notifDesc);
		Vector2 newPos = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.z));
		chestCollected.Add(newPos);
	}
	
	public void TeleportPlayer(GameObject anim)
	{
		int cell = rand.Next(0, listToTeleport.Count);
		Vector3 v = new Vector3(listToTeleport[cell].x, player.transform.position.y, listToTeleport[cell].y);
		if(Vector3.Distance(player.transform.position, v) > 0.75f)
		{
			Vector3 tel = new Vector3(listToTeleport[cell].x, 0, listToTeleport[cell].y);
			if(collectables[2] > 0) collectables[2]--;
			teleport--;
			PlayerPrefs.SetInt("Teleport", teleport);
			moveS.TeleportToPoint(tel);
			anim.transform.position = player.transform.position;
			anim.GetComponent<TeleportScript>().ReverseTeleport();
		}
		player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Renderer>().enabled = true;
		moveS.canMove = true;
		listToTeleport.Clear();
		teleporting = false;
	}

    void OnGUI()
    {
		if(hideGuiForAd) return;
		if(quitCounter > 0)
		{
			GUI.Label(new Rect(sW * 0.35f, sH * 0.3f, sW * 0.3f, sH * 0.1f), "Saving and exiting game...", styler.buyStyle);
			return;
		}
		if(errorAd != "")
		{
			GUI.Label(new Rect(sW * 0.35f, sH * 0.3f, sW * 0.3f, sH * 0.1f), errorAd, styler.buyStyle);
			if(GUI.Button(new Rect(sW * 0.375f, sH * 0.46f, sW * 0.08f, sW * 0.08f), "", styler.yesStyle))
			{
				errorAd = "";
			}
			return;
		}
		if(askingForAd != 0)
		{
			GUI.Label(new Rect(sW * 0.35f, sH * 0.3f, sW * 0.3f, sH * 0.1f), askingForAdText, styler.buyStyle);
			if(GUI.Button(new Rect(sW * 0.35f, sH * 0.41f, sW * 0.08f, sW * 0.08f), "", styler.yesStyle))
			{
				bool ready = false;
				if(!isConnection)
				{
					errorAd = "Ad is not ready. No internet connection established.";
					askingForAd = 0;
				}
				if(askingForAd == 1)
				{
					ready = coinAmountAd.IsLoaded();
					if (ready) 
					{
						hideGuiForAd = true;
						coinAmountAd.Show();
					}
				}
				else if(askingForAd == 2)
				{
					ready = coinLevelAd.IsLoaded();
					if (ready) 
					{
						hideGuiForAd = true;
						coinLevelAd.Show();
					}
				}
				if(!ready)
				{
					errorAd = "Ad is not ready yet. Try again later.";
					askingForAd = 0;
				}
			}
			if(GUI.Button(new Rect(sW * 0.55f, sH * 0.41f, sW * 0.08f, sW * 0.08f), "", styler.noStyle))
			{
				askingForAd = 0;
			}
			return;
		}
		if(showGoldNotif)
		{
			nTimer -= Time.deltaTime;
			GUI.Label(new Rect(sW * 0.3f, sH * 0.475f, sW * 0.4f, sH * 0.1f), notifDesc, styler.notifStyle);
			if(nTimer <= 0)
			{
				nTimer = 0.0f;
				showGoldNotif = false;
			}
		}
        if(menuShowed && description == 0)
		{
			if(askingForDelete)
			{
				GUI.Label(new Rect(sW * 0.35f, sH * 0.3f, sW * 0.3f, sH * 0.1f), "Delete all saved progress?" + "\n" + "Game will exit!", styler.buyStyle);
				if(GUI.Button(new Rect(sW * 0.35f, sH * 0.41f, sW * 0.08f, sW * 0.08f), "", styler.yesStyle))
				{
					PlayerPrefs.SetInt("Stage", 0);
					PlayerPrefs.SetInt("Level", 0);
					PlayerPrefs.SetInt("Gold", 0);
					PlayerPrefs.SetInt("ShowPath", 0);
					PlayerPrefs.SetInt("Levitation", 0);
					PlayerPrefs.SetInt("Teleport", 0);
					PlayerPrefs.SetInt("ShowPathLevel", 1);
					PlayerPrefs.SetInt("LevitationLevel", 1);
					PlayerPrefs.SetInt("TeleportLevel", 1);
					PlayerPrefs.SetInt("CoinsLevel", 0);
					PlayerPrefs.SetString("SavedMaze", "-");
					LoadPlayerData();
					Application.Quit();
				}
				if(GUI.Button(new Rect(sW * 0.55f, sH * 0.41f, sW * 0.08f, sW * 0.08f), "", styler.noStyle))
				{
					askingForDelete = false;
				}
				return;
			}
			if(levelReady)
			{
				GUI.Label(new Rect(sW * 0.3f, sH * 0.075f + sW * 0.275f, sW * 0.2f, sW * 0.05f), "Stage : " + level + "-" + stageInLevel, styler.buyStyle);
				if(GUI.Button(new Rect(sW * 0.12f,sH * 0.075f + sW * 0.2225f,sW * 0.15f, sW * 0.15f), "", styler.playLevel))
				{
					podgladObject.SetActive(true);
					podgladObjectBack.SetActive(true);
					FloorParent.SetActive(false);
					menuShowed = false;
					SpawnArrows();
					skipped = false;
					Fly("MAP");
					gameMenu = true;
				}
			}
			#region GOLD
			GUI.Label(new Rect(sW * 0.775f, sH * 0.025f, sW * 0.2f, sW * 0.05f), "Coins : " + gold, styler.goldAmount);
			if(GUI.Button(new Rect(sW * 0.775f,sH * 0.035f + sW * 0.05f,sW * 0.2f, sW * 0.05f), "Add 500 coins", styler.goldAdd))
			{
				askingForAdText = "Do you want to watch ad?\nYou will earn 500 coins.";
				askingForAd = 1;
			}
			if(GUI.Button(new Rect(sW * 0.775f,sH * 0.045f + sW * 0.1f,sW * 0.2f, sW * 0.05f), "      Get " + (coinsLv + 2) + "% more coins \n for every stage!", styler.goldAmount))
			{
				askingForAdText = "Do you want to watch ad?\nYou will earn 2% more coins for every cleared stage.";
				askingForAd = 2;
			}
			GUI.enabled = true;
			#endregion
			
			#region PATCHER
			if(GUI.Button(new Rect(sW * 0.12f,sH * 0.025f,sW * 0.15f, sW * 0.15f), "", styler.showPathStyle))
			{
				description = 1;
			}
			GUI.enabled = gold >= showPathGold;
			if(GUI.Button(new Rect(sW * 0.095f,sH * 0.05f + sW * 0.1425f,sW * 0.2f, sW * 0.0375f), "        Coins of Patcher : " + showPathGold, styler.buyStyle))
			{
				gold -= showPathGold;
				showPath++;
				PlayerPrefs.SetInt("ShowPath", showPath);
			}
			GUI.enabled = gold >= showPathGoldForLevel && showPathLv < 50;
			if(GUI.Button(new Rect(sW * 0.095f,sH * 0.075f + sW * 0.135f + sW * 0.0375f,sW * 0.2f, sW * 0.0375f), "        Coins for upgrade : " + showPathGoldForLevel, styler.buyStyle))
			{
				gold -= showPathGoldForLevel;
				showPathLv++;
				showPathGoldForLevel = showPathGoldMulti * showPathLv;
				PlayerPrefs.SetInt("ShowPathLevel", showPathLv);
			}
			GUI.enabled = true;
			GUI.Label(new Rect(sW * 0.095f,sH * 0.05f + sW * 0.1425f,sW * 0.0375f, sW * 0.0375f), "" + showPath, styler.hintsAmount);
			GUI.Label(new Rect(sW * 0.095f,sH * 0.075f + sW * 0.135f + sW * 0.0375f,sW * 0.0375f, sW * 0.0375f), "" + showPathLv, styler.hintsLevel);
			#endregion
			
			#region TELEPORT
			if(GUI.Button(new Rect(sW * 0.335f,sH * 0.025f,sW * 0.15f, sW * 0.15f), "", styler.teleportStyle))
			{
				description = 2;
			}
			GUI.enabled = gold >= teleportGold;
			if(GUI.Button(new Rect(sW * 0.31f,sH * 0.05f + sW * 0.1425f,sW * 0.2f, sW * 0.0375f), "        Coins for Teleport : " + teleportGold, styler.buyStyle))
			{
				gold -= teleportGold;
				teleport++;
				PlayerPrefs.SetInt("Teleport", teleport);
			}
			GUI.enabled = gold >= teleportGoldForLevel && teleportLv < 50;
			if(GUI.Button(new Rect(sW * 0.31f,sH * 0.075f + sW * 0.135f + sW * 0.0375f,sW * 0.2f, sW * 0.0375f), "        Coins for upgrade : " + teleportGoldForLevel, styler.buyStyle))
			{
				gold -= teleportGoldForLevel;
				teleportLv++;
				teleportGoldForLevel = teleportGoldMulti * teleportLv;
				PlayerPrefs.SetInt("TeleportLevel", teleportLv);
			}
			GUI.enabled = true;
			GUI.Label(new Rect(sW * 0.31f,sH * 0.05f + sW * 0.1425f,sW * 0.0375f, sW * 0.0375f), "" + teleport, styler.hintsAmount);
			GUI.Label(new Rect(sW * 0.31f,sH * 0.075f + sW * 0.135f + sW * 0.0375f,sW * 0.0375f, sW * 0.0375f), "" + teleportLv, styler.hintsLevel);
			#endregion
			
			#region LEVITATION
			if(GUI.Button(new Rect(sW * 0.55f,sH * 0.025f,sW * 0.15f, sW * 0.15f), "", styler.levitationStyle))
			{
				description = 3;
			}
			GUI.enabled = gold >= levitationGold;
			if(GUI.Button(new Rect(sW * 0.525f,sH * 0.05f + sW * 0.1425f,sW * 0.2f, sW * 0.0375f), "        Coins for Levitation : " + levitationGold, styler.buyStyle))
			{
				gold -= levitationGold;
				levitation++;
				PlayerPrefs.SetInt("Levitation", levitation);
			}
			GUI.enabled = gold >= levitationGoldForLevel && levitationLv < 50;
			if(GUI.Button(new Rect(sW * 0.525f,sH * 0.075f + sW * 0.135f + sW * 0.0375f,sW * 0.2f, sW * 0.0375f), "        Coins for upgrade : " + levitationGoldForLevel, styler.buyStyle))
			{
				gold -= levitationGoldForLevel;
				levitationLv++;
				levitationGoldForLevel = levitationGoldMulti * levitationLv;
				PlayerPrefs.SetInt("LevitationLevel", levitationLv);
			}
			GUI.enabled = true;
			GUI.Label(new Rect(sW * 0.525f,sH * 0.05f + sW * 0.1425f,sW * 0.0375f, sW * 0.0375f), "" + levitation, styler.hintsAmount);
			GUI.Label(new Rect(sW * 0.525f,sH * 0.075f + sW * 0.135f + sW * 0.0375f,sW * 0.0375f, sW * 0.0375f), "" + levitationLv, styler.hintsLevel);
			#endregion
			
			if(GUI.Button(new Rect(sW * 0.875f,sH * 0.055f + sW * 0.15f,sW * 0.1f, sW * 0.1f), "", styler.exitGame))
			{
				SaveGame(2);
				quitCounter = 1;
			}
			if(GUI.Button(new Rect(sW * 0.7625f,sH * 0.055f + sW * 0.15f,sW * 0.1f, sW * 0.1f), "", styler.deleteStyle))
			{
				askingForDelete = true;
			}
		}
		if(menuShowed && description != 0)
		{
			Rect r = new Rect(sW * 0.2f, sH * 0.3f, sW * 0.6f, sW * 0.2f);
			if(GUI.Button(new Rect(sW * 0.85f, sH * 0.1f, sW * 0.1f, sW * 0.1f), "", styler.backStyle))
			{
				description = 0;
			}
			if(description == 1)
			{
				GUI.Label(r, styler.pathDescription, styler.descriptionStyle);
			}
			if(description == 2)
			{
				GUI.Label(r, styler.teleportDescription, styler.descriptionStyle);
			}
			if(description == 3)
			{
				GUI.Label(r, styler.levitationDescription, styler.descriptionStyle);
			}
		}
		if(gameMenu)
		{
			if((cameraFlyToMap || (cameraFlyToPlayer && timer < secsToSeeMaze)) && !skipped)
			{
				if(GUI.Button(new Rect(sW * 0.8f, sH * 0.1f, sW * 0.1f, sW * 0.1f), "", styler.skipStyle))
				{
					cameraSkipStartRot = new GameObject("CameraRotSkip");
					cameraSkipStartRot.transform.rotation = mainCamera.transform.rotation;
					cameraSkipStartPos = mainCamera.transform.position;
					skipped = true;
					Fly("SKIP");
				}
			}
			if(showRestMenu)
			{
				if(GUI.Button(new Rect(sW * 0.025f, sH * 0.05f, sW * 0.1f, sW * 0.1f), "", styler.exitGame))
				{
					PerformExit();
				}
				GUI.Label(new Rect(sW * 0.15f, sH * 0.05f, sW * 0.2f, sW * 0.05f), "Coins : " + gold, styler.goldAmount);
				if(GUI.Button(new Rect(sW * 0.375f, sH * 0.05f, sW * 0.1f, sW * 0.1f), "", styler.restartLevel))
				{
					PerformRestart();
				}
				GUI.enabled = showPath > 0;
				if((pathFinder != null&& !pathFinder.GetComponent<PathFinder>().ended) || teleporting) GUI.enabled = false;
				if(GUI.Button(new Rect(sW * 0.85f, sH * 0.05f, sW * 0.1f, sW * 0.1f), "", styler.showPathStyle))
				{
					CreatePathFinder();
					if(collectables[1] > 0) collectables[1]--;
					showPath--;
					PlayerPrefs.SetInt("ShowPath", showPath);
				}
				GUI.Label(new Rect(sW * 0.93125f, sH * 0.05f - sW * 0.01875f, sW * 0.0375f, sW * 0.0375f), showPath + "", styler.hintsAmount);
				GUI.enabled = teleport > 0;
				if(teleporting) GUI.enabled = false;
				if(GUI.Button(new Rect(sW * 0.725f, sH * 0.05f, sW * 0.1f, sW * 0.1f), "", styler.teleportStyle))
				{
					if(teleporting) return;
					teleporting = true;
					moveS.canMove = false;
					listToTeleport = generator.FindPathTeleport(teleportLv);
					if(listToTeleport.Count == 0) 
					{
						Debug.Log("NIE ZNALEEZIONO PUNKTU TELEPORTU");
						teleporting = false;
						moveS.canMove = true;
					}
					else
					{
						GameObject g = Instantiate(teleportAnimPrefab, player.transform.position, Quaternion.identity);
					}
				}
				GUI.Label(new Rect(sW * 0.80625f, sH * 0.05f - sW * 0.01625f, sW * 0.0375f, sW * 0.0375f), teleport + "", styler.hintsAmount);
				GUI.enabled = levitation > 0;
				if(isLevitating || teleporting) GUI.enabled = false;
				if(GUI.Button(new Rect(sW * 0.6f, sH * 0.05f, sW * 0.1f, sW * 0.1f), "", styler.levitationStyle))
				{
					isLevitating = true;
					if(collectables[3] > 0) collectables[3]--;
					levitation--;
					PlayerPrefs.SetInt("Levitation", levitation);
					LevitationSetFloor(1); // można lewitować
				}
				GUI.Label(new Rect(sW * 0.68125f, sH * 0.05f - sW * 0.01625f, sW * 0.0375f, sW * 0.0375f), levitation + "", styler.hintsAmount);
				GUI.enabled = true;
				if(isLevitating)
				{
					levitationTimer += Time.deltaTime;
					if(levitationTimer > 8 + (levitationLv * 2f))
					{
						levitationTimer = 0.0f;
						isLevitating = false;
						LevitationSetFloor(2); // usuń lewitację
					}
				}
				if(isLevitating)
				{
					float percent = (levitationTimer / (float)(8 + (levitationLv * 2f)));
					float diff = (percent * -1) + 1;
					float posH = sH * 0.05f + sW * 0.1f - (sW * 0.1f * diff);
					float sizeW = sW * 0.1f;
					
					Rect r = new Rect(sW * 0.6f + 1, posH + 1, sW * 0.1f - 2, sizeW * diff - 3);
					
					GUI.enabled = true;
					GUI.Label(r, "", styler.greyAlphaStyle);
				}
			}
		}
    }
}
