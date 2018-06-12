using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAcrossLayers : MonoBehaviour {

	public int quantity;

	public float xmin, xmax, ymin, ymax;
	public int zmin, zmax;
	public GameObject[] entity;
	public GameObject newCharacter;
	Vector3 location;
	public bool growing;
	// Use this for initialization
	void Start () 
	{

		//loop through spawned prefabs
		for(int i=0; i<entity.Length;i++)
		{
			for (int z = zmin; z < zmax; z += 20) 
			{
				for(int j=0; j<quantity;j++)
				{
					location = new Vector3 (xmin + (Random.value * (xmax - xmin)), ymin + (Random.value * (ymax - ymin)), z + 1f);
					newCharacter = Instantiate (entity [i], location, Quaternion.identity);
//			newCharacter.transform.parent = gameObject.transform.parent.transform.parent.transform.GetChild (0);
//			newCharacter.GetComponent< character_behavior > ().mapPlane = location.z;
				}
			}

		}
	}

	// Update is called once per frame
	void Update () {




	}
}
