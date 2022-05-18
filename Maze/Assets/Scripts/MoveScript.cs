using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveScript : MonoBehaviour
{
	public float speed;
	private CharacterController control;
	private MainScript mainS;
	public bool canMove = false;
	private Vector3 lastPos;

	private List<GameObject> lastFloor;
	private int maxFloorsBehind = 8;
	
	public GUIStyle joystickStyle;
	public GUIStyle joystickTmp;
	public GUIStyle joystickCenter;
	private int touchIndexMove;
	
	private Rect joystickWorkingArea;
	private Vector2 joyDefaultPos;
	private Vector2 joyActualPos;
	private Vector2 centerJoyPos;
	
	private float speedMultiplierX;
	private float speedMultiplierY;
	
	public Vector2 lastCell;
	public int cellsMoved = 0;
	
	[HideInInspector] public GameObject characterModel;
	[HideInInspector] public Animation anim;

	private GameObject objectToLook;
	private Vector2 joysticPosToSpace;

	void Start()
	{
		objectToLook = new GameObject("ObjectToPlayerLook");
		characterModel = gameObject.transform.GetChild(0).gameObject;
		anim = characterModel.GetComponent<Animation>();
		anim.Play("WaitAnim");
		anim["RunEnd"].speed = 3;
		anim["RunLoop"].speed = 1.5f;
		anim["RunBegin"].speed = 3f;
		lastCell = new Vector2(-1000,-1000);
		cellsMoved = 0;
		touchIndexMove = -1;
		joystickWorkingArea = new Rect(Screen.width * 0.05f, Screen.height - Screen.width * 0.2f, Screen.width * 0.125f, Screen.width * 0.125f);
		centerJoyPos = new Vector2(joystickWorkingArea.x + (joystickWorkingArea.width / 2f), joystickWorkingArea.y + (joystickWorkingArea.height / 2f));
		joyDefaultPos = new Vector2(centerJoyPos.x, centerJoyPos.y);
		joyActualPos = joyDefaultPos;
		objectToLook.transform.position = new Vector3(characterModel.transform.position.x + 1, characterModel.transform.position.y, characterModel.transform.position.z);
		characterModel.transform.LookAt(objectToLook.transform);
		if(maxFloorsBehind == 0) maxFloorsBehind = 1;
		mainS = GameObject.FindWithTag("MainObject").GetComponent<MainScript>();
		lastFloor = new List<GameObject>();
		control = gameObject.GetComponent<CharacterController>();
	}
	
	public void TeleportToPoint(Vector3 point)
	{
		canMove = false;
		control.enabled = false;
		transform.position = point;
		control.enabled = true;
		canMove = true;
	}
	
	public void CountCells()
	{
		Vector3 pl = mainS.player.transform.position;
		Vector2 changedPos = new Vector3((int)Mathf.Round(pl.x), (int)Mathf.Round(pl.y));
		if(lastCell != changedPos)
		{
			cellsMoved++;
			lastCell = changedPos;
		}
	}
	
	void OnGUI()
	{
		if(canMove && !mainS.menuShowed && mainS.gameMenu)
		{
			GUI.Label(new Rect(joyActualPos.x - Screen.width * 0.05f, joyActualPos.y - Screen.width * 0.05f, Screen.width * 0.1f, Screen.width * 0.1f), "", joystickStyle);
			if(touchIndexMove == -1)
			{
				if(Input.touchCount == 0) return;
				for(int i = 0; i < Input.touchCount; i++)
				{
					Touch t = Input.GetTouch(i);
					if(t.phase != TouchPhase.Began) continue;
					Vector2 inputPos = new Vector2(t.position.x, t.position.y * -1 + Screen.height);
					if(joystickWorkingArea.Contains(inputPos))
					{
						anim.Play("RunBegin");
						touchIndexMove = i;
						break;
					}
				}
			}
			else
			{
				if(Input.touchCount <= touchIndexMove) return;
				Touch t = Input.GetTouch(touchIndexMove);
				if(t.phase == TouchPhase.Ended)
				{
					speedMultiplierX = 0.5f;
					speedMultiplierY = 0.5f;
					touchIndexMove = -1;
					joyActualPos = joyDefaultPos;
					anim.Play("RunEnd");
					return;
				}
				Vector2 joyPos = new Vector2(t.position.x, t.position.y * -1 + Screen.height);
				if(joystickWorkingArea.Contains(joyPos))
				{
					joyActualPos = joyPos;
				}
				else
				{
					if(joyPos.x < joystickWorkingArea.x || joyPos.x > joystickWorkingArea.x + joystickWorkingArea.width)
					{
						if(joyPos.x < joystickWorkingArea.x) 
							joyPos.x = joystickWorkingArea.x;
						else if(joyPos.x > joystickWorkingArea.x + joystickWorkingArea.width)
							joyPos.x = joystickWorkingArea.x + joystickWorkingArea.width;
					}
					if(joyPos.y < joystickWorkingArea.y || joyPos.y > joystickWorkingArea.y + joystickWorkingArea.height)
					{
						if(joyPos.y < joystickWorkingArea.y) 
							joyPos.y = joystickWorkingArea.y;
						else if(joyPos.y > joystickWorkingArea.y + joystickWorkingArea.height)
							joyPos.y = joystickWorkingArea.y + joystickWorkingArea.height;
					}
					joyActualPos = joyPos;
				}
				joysticPosToSpace = new Vector2(joyActualPos.x - centerJoyPos.x, joyActualPos.y - centerJoyPos.y);
				Vector3 objPos = new Vector3(characterModel.transform.position.x - joysticPosToSpace.y, characterModel.transform.position.y, characterModel.transform.position.z - joysticPosToSpace.x);
				objectToLook.transform.position = objPos;
				speedMultiplierX = (joyPos.x - joystickWorkingArea.x) / (joystickWorkingArea.width);
				if(speedMultiplierX > 1) speedMultiplierX = 1;
				speedMultiplierY = (joyPos.y - joystickWorkingArea.y) / (joystickWorkingArea.height);
				if(speedMultiplierY > 1) speedMultiplierY = 1;
				float newSpeedX = (speedMultiplierX - 0.5f) * 2;
				float newSpeedY = (speedMultiplierY - 0.5f) * 2;
				Vector3 forward = transform.TransformDirection(Vector3.forward);
				Vector3 left = transform.TransformDirection(Vector3.left);
				float biggerSpeed = 0.0f;
				if(Math.Abs(newSpeedX) >= Math.Abs(newSpeedY)) biggerSpeed = Math.Abs(newSpeedX);
				else biggerSpeed = Math.Abs(newSpeedY);
				anim["RunLoop"].speed = biggerSpeed * 1.5f;
				if(!anim.isPlaying && newSpeedX != 0 && newSpeedY != 0) anim.Play("RunLoop");
				control.SimpleMove(forward * (newSpeedY * speed));
				control.SimpleMove(left * (newSpeedX * speed));
				CountCells();
			}
		}
		else
		{
			if(!anim.isPlaying) anim.Play("WaitAnim");
		}
	}
	
    void Update()
    {
		if(transform.position.y < -1f)
		{
			mainS.PerformRestart();
		}
		characterModel.transform.LookAt(objectToLook.transform);
    }
	
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.tag == "Chest")
		{
			c.gameObject.GetComponent<ChestScript>().CollectChest();
		}
		if(c.gameObject.tag == "UnstableFloor" && !lastFloor.Contains(c.gameObject))
		{
			lastFloor.Add(c.gameObject);
		}
		if(lastFloor.Count > maxFloorsBehind)
		{
			int diff = lastFloor.Count - maxFloorsBehind;
			Vector3 floorVec;
			Vector3 playerVec = new Vector3(transform.position.x, 0, transform.position.z);
			for(int i = 0; i < diff; i++)
			{
				floorVec = new Vector3(lastFloor[i].transform.position.x, 0, lastFloor[i].transform.position.z);
				if(Vector3.Distance(floorVec, playerVec) > 0.6f)
				{
					if(lastFloor[i] != null) lastFloor[i].GetComponent<FloorScript>().ExecuteFloor();
					lastFloor.RemoveAt(i);
				}
			}
		}
	}
}
