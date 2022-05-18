using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
	private bool go = false;
	private int index = 0;
	private List<Vector3> path;
	private int showLv;
	private ParticleSystem ps;
	private LineRenderer line;
	private Vector3 startPos;
	
	private float speed = 4.5f;
	private Vector3[] linePos;
	private List<Vector3> linePosList;
	
	private List<Transform> pathObjects;
	[HideInInspector] public bool ended = false;
	
	private GameObject parentObj;
	
    public void InitializePathFinder(List<Vector2> list, int lv)
	{
		ended = false;
		pathObjects = new List<Transform>();
		line = this.GetComponent<LineRenderer>();
		startPos = new Vector3(-10000, 0,0);
		path = new List<Vector3>();
		for(int i = 0; i < list.Count; i++)
		{
			path.Add(new Vector3(list[i].x, 0.35f, list[i].y));
		}
		showLv = (lv * 2) + 8;
		index = 0;
		line.positionCount = 1;
		line.SetPosition(0, transform.position);
		ps = this.GetComponent<ParticleSystem>();
		PrepareObjects();
		go = true;
	}
	
	void PrepareObjects()
	{
		parentObj = new GameObject("ParentPath");
		parentObj.transform.position = new Vector3(0,0,0);
		for(int i = 0; i < path.Count; i++)
		{
			GameObject g = new GameObject("PathObject" + i);
			g.transform.position = path[i];
			g.transform.parent = parentObj.transform;
			pathObjects.Add(g.transform);
		}
	}
	
	void FixedUpdate()
	{
		if(go)
		{
			if(!ended)
			{
				line.SetPosition(index, transform.position);
				transform.LookAt(pathObjects[index]);
				transform.position = Vector3.MoveTowards(transform.position, path[index], Time.deltaTime * speed);
				float dist = Vector3.Distance(transform.position, path[index]);
				if(dist < 0.05f && !ended)
				{
					transform.position = path[index];
					line.SetPosition(index, transform.position);
					index++;
					if(index == path.Count)
					{
						ended = true;
					}
					else 
					{
						line.positionCount++;
						line.SetPosition(index, transform.position);
					}
				}
			}
			if(index > showLv || ended)
			{
				ended = true;
				var psm = ps.main;
				psm.loop = false;
				if(!ps.IsAlive()) 
				{	
					for(int i = 0; i < pathObjects.Count; i++)
					{
						if(pathObjects[i].gameObject == null) continue;
						Destroy(pathObjects[i].gameObject);
					}
					Destroy(gameObject);
				}
			}
		}
	}
}
