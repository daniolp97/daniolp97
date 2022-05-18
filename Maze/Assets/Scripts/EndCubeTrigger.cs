using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCubeTrigger : MonoBehaviour
{
	private GameObject mainObj;
	
	void Start()
	{
		mainObj = GameObject.FindWithTag("MainObject");
	}
	
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
		{
			mainObj.GetComponent<MainScript>().EndLevel();
			Destroy(this.gameObject);
		}
    }
}
