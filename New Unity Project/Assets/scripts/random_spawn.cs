using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_spawn : MonoBehaviour {
	public int[] counter;
	public float xmin, xmax, ymin, ymax;
	public GameObject[] entity;
	public GameObject newCharacter;
	Vector3 location;
	// Use this for initialization
	void Start () 
	{
//initilize when first agent spawn
		for (int i = 0; i < 7; i++) 
		{
			counter [i] = (int)(Random.value * 2000);
		}
	}
	
	// Update is called once per frame
	void Update () {

//loop through spawned prefabs
		for(int i=0; i<7;i++){
		counter[i]--;

//time to spawn a bot
		if (counter[i] < 0) 
			{
				location=new Vector3(xmin + (Random.value * (xmax-xmin)), ymin + (Random.value * (ymax-ymin)), 0f);
				newCharacter= Instantiate(entity[i], location, Quaternion.identity);
				newCharacter.transform.parent = gameObject.transform.parent.transform.parent.transform.GetChild (0);
				newCharacter.GetComponent< character_behavior > ().mapPlane = location.z;

//set new spawn time
				counter[i] = 600+(int)(Random.value * 1400);
			}
		}


	}
}
