using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimationScript : MonoBehaviour
{
    private float speed = 0.075f;
	private float timer;
	private float size = 0.01f;
	
	void Update()
	{
		timer += Time.deltaTime;
		size += speed * Time.deltaTime;
		transform.localScale = new Vector3(size, size, size);
		if(size > 0.15f)
		{
			Destroy(gameObject);
		}
	}
}
