using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
	public float maxPosX = 0.0f;
	private float startPosX = 0.0f;
	public float speed = 0.0f;
	public Transform plane;
	public MainScript mainS;
	private Styler styler;
	
	public GameObject thisCamera;
	public GameObject mainCamera;
	public GeneratorScript generator;
	private bool shouldLoad;
	private bool loaded;
	
	private bool testMode = true;
	
	void Start()
	{
		loaded = false;
		shouldLoad = false;
		startPosX = plane.position.x;
		generator = GameObject.FindWithTag("MainObject").GetComponent<GeneratorScript>();
		styler = GameObject.FindWithTag("MainObject").transform.GetChild(0).gameObject.GetComponent<Styler>();
	}
	
    void Update()
    {
		if(!testMode)
		{
			if(Input.GetKeyDown("p"))
			{
				mainS.AfterIntro();
				mainCamera.SetActive(true);
				thisCamera.SetActive(false);
			}
			if(plane.position.x < startPosX - maxPosX && !loaded)
			{
				shouldLoad = true;
				if(generator.generatorThread.IsAlive) return;
				loaded = true;
				mainS.AfterIntro();
				mainCamera.SetActive(true);
				thisCamera.SetActive(false);
			}
			else
			{
				plane.Translate(Vector3.left * Time.deltaTime * speed);
			}
		}
		else
		{
			if(!generator.generatorThread.IsAlive)
			{
				mainS.AfterIntro();
				mainCamera.SetActive(true);
				thisCamera.SetActive(false);
			}
		}
    }
	
	void OnGUI()
	{
		if(!testMode)
		{
			if(shouldLoad && !loaded)
			{
				GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Loading...", styler.loadingStyle);
			}
		}
		else GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.8f, Screen.width * 0.2f, Screen.height * 0.1f), "Loading...", styler.loadingStyle);
	}
}
