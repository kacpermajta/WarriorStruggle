using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_spawn : MonoBehaviour {
	public int[] counter;
	public int cooldown;
	public float xmin, xmax, ymin, ymax,z;
	public GameObject[] entity;
	public GameObject newCharacter;
	Vector3 location;
	public bool growing;
	// Use this for initialization
	void Start () 
	{
		if (cooldown == 0) 
		{
			cooldown = 600;
		}
//initilize when first agent spawn
		for (int i = 0; i < counter.Length; i++) 
		{
			counter [i] = (int)(Random.value * (3*cooldown));
		}
	}
	
	// Update is called once per frame
	void Update () {

//loop through spawned prefabs
		for(int i=0; i<entity.Length;i++){
		counter[i]--;

//time to spawn a bot
		if (counter[i] < 0) 
			{
				location=new Vector3(xmin + (Random.value * (xmax-xmin)), ymin + (Random.value * (ymax-ymin)), z);
				newCharacter= Instantiate(entity[i], location, Quaternion.identity);
				newCharacter.transform.parent = gameObject.transform.parent.transform.parent.transform.GetChild (0);
				newCharacter.GetComponent< character_behavior > ().mapPlane = location.z;
				if (growing)
					cooldown--;
//set new spawn time
				counter[i] = cooldown+(int)(Random.value * (2*cooldown));
			}
		}


	}
}
