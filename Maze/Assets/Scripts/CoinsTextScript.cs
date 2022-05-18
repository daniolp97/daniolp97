using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsTextScript : MonoBehaviour
{
    private float timeToDestroy = 3f;
	private float timer;

	private GameObject camera;
	private float speedPos = 0.5f;
	
	void Start()
	{
		camera = GameObject.FindWithTag("MainCamera");
		timer = 0.0f;
		transform.LookAt(camera.transform);
	}
	
	void Update()
	{
		transform.Translate(Vector3.up * Time.deltaTime * speedPos);
		timer += Time.deltaTime;
		if(timer > timeToDestroy)
		{
			Destroy(this.gameObject);
		}
	}
	
	public void SetText(string txt)
	{
		this.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = txt;
	}
}
