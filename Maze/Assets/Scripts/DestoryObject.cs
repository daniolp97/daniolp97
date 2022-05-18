using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    public float timeToDestroy;
	private float timer;

    void Update()
    {
        timer += Time.deltaTime;
		if(timer > timeToDestroy)
		{
			Destroy(gameObject);
		}
    }
}
