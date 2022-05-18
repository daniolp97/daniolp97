using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
	public Material destroyedMat;
	public Material defaultMat;
	
	private BoxCollider col;
	
	private MainScript mainS;
	private int x;
	private int y;
	
	private float colSize = 0.0f;
	
	void Start()
	{
		colSize = 0.05f;
		col = gameObject.transform.GetChild(0).GetComponent<BoxCollider>();
		string[] name = gameObject.name.Replace("Floor", "").Split('x');
		x = System.Int32.Parse(name[0]);
		y = System.Int32.Parse(name[1]);
		mainS = GameObject.FindWithTag("MainObject").GetComponent<MainScript>();
	}
	
    public void ExecuteFloor()
	{
		if(mainS.isLevitating) return;
		Transform child = gameObject.transform.GetChild(0);
		child.rotation = Quaternion.identity;
		child.rotation = Quaternion.Euler(90,0,0);
		col.size = new Vector3(2,2,5);
		colSize = 5;
		mainS.FloorErased(x,y);
	}
	
	public void RestartFloor()
	{
		Transform child = gameObject.transform.GetChild(0);
		child.rotation = Quaternion.identity;
		child.rotation = Quaternion.Euler(-90,0,0);
		col.size = new Vector3(2,2,0.05f);
		colSize = 0.05f;
	}
	
	public void PlayerLevitation(int mode)
	{
		if(mode == 1) col.size = new Vector3(2,2,0.05f);
		else col.size = new Vector3(2,2,colSize);;
	}
}
