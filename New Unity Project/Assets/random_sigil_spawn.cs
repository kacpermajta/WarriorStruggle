using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_sigil_spawn : MonoBehaviour {
	public int counter,delay;

	public float xmin, xmax, ymin, ymax, z;
	public GameObject entity;
	GameObject newSigil;
	Vector3 location;
	// Use this for initialization
	void Start () 
	{
		//initilize when first agent spawn

			counter  = delay+(int)(Random.value * 2000);

	}

	// Update is called once per frame
	void Update () {

		//loop through spawned prefabs

			counter--;

			//time to spawn a bot
			if (counter < 0) 
			{
				location=new Vector3(xmin + (Random.value * (xmax-xmin)), ymin + (Random.value * (ymax-ymin)), z);
				Instantiate(entity, location, Quaternion.identity);
				Destroy (gameObject);

			}

	}
}