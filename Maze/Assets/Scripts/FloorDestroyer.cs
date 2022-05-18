using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDestroyer : MonoBehaviour
{
	private List<GameObject> lastFloor;
	public int maxFloorsBehind = 4;
	
	void Start()
	{
		if(maxFloorsBehind == 0) maxFloorsBehind = 1;
		lastFloor = new List<GameObject>();
	}
	
    void OnTriggerEnter(Collider c)
	{
		Debug.Log(c.gameObject.tag);
		if(c.gameObject.tag == "UnstableFloor")
		{
			lastFloor.Add(c.gameObject);
		}
		if(lastFloor.Count > maxFloorsBehind)
		{
			int diff = lastFloor.Count - maxFloorsBehind;
			for(int i = 0; i < diff; i++)
			{
				lastFloor[i].GetComponent<FloorScript>().ExecuteFloor();
				lastFloor.RemoveAt(i);
			}
		}
	}
}
