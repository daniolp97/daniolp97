using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public float rotSpeed;
	private float actualRotSpeed;
	private float speedDecrease;
	public int directory;
	
	public int prize;
	private float timer;
	
	public GameObject pickUpChestParticle;
	
	public GameObject chestPreviewGameObject;
	
	void Start()
	{
		directory = 0;
		speedDecrease = rotSpeed * 0.15f;
		actualRotSpeed = rotSpeed;
		RandomRot();
		RandomPrize();
	}
	
	void Update()
	{
		transform.Rotate(Vector3.forward * Time.deltaTime * actualRotSpeed);
		timer += Time.deltaTime;
		if(timer > 1f)
		{
			if(directory == 0) 
			{
				actualRotSpeed = actualRotSpeed - speedDecrease;
				if(actualRotSpeed <= -rotSpeed) directory = 1;
			}
			else if(directory == 1)
			{
				actualRotSpeed = actualRotSpeed + speedDecrease;
				if(actualRotSpeed >= rotSpeed) directory = 0;
			}
			timer = 0.0f;
		}
	}
	
	bool InThe(int liczba, int od, int doo)
	{
		if(liczba >= od && liczba <= doo) return true;
		return false;
	}
	
	void RandomPrize()
	{
		int rand = Random.Range(0,100);
		int secRand = 0;
		if(rand > 50) prize = 1;
		else
		{
			secRand = Random.Range(0,100);
			if(InThe(secRand, 0, 50))
			{
				prize = 4;
			}
			else if(InThe(secRand, 50, 80))
			{
				prize = 2;
			}
			else
			{
				prize = 3;
			}
		}
	}
	
	void RandomRot()
	{
		transform.rotation = Quaternion.Euler(new Vector3(-90,0,Random.Range(0, 180)));
	}
	
	public void CollectChest()
	{
		GameObject.FindWithTag("MainObject").GetComponent<MainScript>().CollectedChest(prize, transform.position);
		GameObject g = Instantiate(pickUpChestParticle, transform.position, Quaternion.identity);
		Destroy(chestPreviewGameObject);
		Destroy(transform.parent.gameObject);
	}
}
