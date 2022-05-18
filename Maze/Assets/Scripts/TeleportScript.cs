using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public float rotSpeed = 1;
	public float moveSpeed = 1;
	public GameObject bottom;
	public GameObject upper;
	public float timeToTeleport;
	private float timerTele = 0.0f;
	private Vector3 zero;
	private bool animate;
	private MainScript mainS;
	private bool animateReversed;
	
	private float bottomPos;
	private float upperPos;
	
	private Vector3 boPos;
	private Vector3 upPos;
	
	private bool oneTimeAnim = false;
	
	private float timer = 0.0f;
	
	void Start()
	{
		oneTimeAnim = false;
		animateReversed = false;
		bottomPos = transform.position.y - bottom.transform.position.y;
		upperPos = upper.transform.position.y - transform.position.y;
		mainS = GameObject.FindWithTag("MainObject").GetComponent<MainScript>();
		zero = this.transform.position;
		animate = false;
	}
	
	void Update()
	{
		bottom.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
		upper.transform.Rotate(Vector3.down * Time.deltaTime * rotSpeed);
		timerTele += Time.deltaTime;
		timer += Time.deltaTime;
		
		if(!animate && timerTele > timeToTeleport && !oneTimeAnim)
		{
			timerTele = 0.0f;
			animate = true;
			oneTimeAnim = true;
			mainS.player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Renderer>().enabled = false;
		}
		
		if(animate)
		{
			bottom.transform.position = Vector3.MoveTowards(bottom.transform.position, zero, moveSpeed * Time.deltaTime);
			upper.transform.position = Vector3.MoveTowards(upper.transform.position, zero, moveSpeed * Time.deltaTime);
			
			if(SoClose(1))
			{
				animate = false;
				mainS.TeleportPlayer(gameObject);
			}
		}
		
		if(animateReversed)
		{
			bottom.transform.position = Vector3.MoveTowards(bottom.transform.position, boPos, moveSpeed * Time.deltaTime);
			upper.transform.position = Vector3.MoveTowards(upper.transform.position, upPos, moveSpeed * Time.deltaTime);
		}
		
		if(timer > 0.92f)
		{
			Destroy(gameObject);
		}
	}
	
	bool SoClose(int mode)
	{
		if(mode == 1)
		{
			if(Vector3.Distance(bottom.transform.position, zero) < 0.1f && Vector3.Distance(upper.transform.position, zero) < 0.1f)
			{
				return true;
			}
		}
		else if(mode == 2)
		{
			if(Vector3.Distance(bottom.transform.position, boPos) < 0.1f && Vector3.Distance(upper.transform.position, upPos) < 0.1f)
			{
				return true;
			}
		}
		return false;
	}
	
	public void ReverseTeleport()
	{
		boPos = new Vector3(bottom.transform.position.x, bottom.transform.position.y - bottomPos, bottom.transform.position.z);
		upPos = new Vector3(upper.transform.position.x, upper.transform.position.y + upperPos, upper.transform.position.z);
		animateReversed = true;
	}
}
















