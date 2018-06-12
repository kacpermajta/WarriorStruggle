using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEntitiesManager : MonoBehaviour {
	GameObject playerCamera;
	Vector3 location;
	Vector3 entLocation;
	public int quantity;

	public float  xmax, ymax;
	public int zmin, zmax;
	GameObject[] entity;
	GameObject newCharacter;


	// Use this for initialization
	void Start () {
		//Debug.Log ("duze ilosci naraz dymu");
		//location = playerCamera.transform.position;
		entity = playerSettings.staticBackscreen;

		if (quantity == 0) 
		{
			xmax = 60f;
			ymax = 40f;
			quantity = 5;
			zmin = -40;
			zmax = 100;

		}
		for(int i=0; i<entity.Length;i++)
		{
			for (int z = zmin; z < zmax; z += 20) 
			{
				for(int j=0; j<quantity;j++)
				{
					entLocation = new Vector3 (gameObject.transform.position.x + ((Random.value-0.5f) * (xmax)), 
						gameObject.transform.position.y + ((Random.value-0.5f)* (ymax)), z + 1f);
					newCharacter = Instantiate (entity [i], entLocation, Quaternion.identity);
					newCharacter.GetComponent<cameraDusting> ().pivot = gameObject.transform.parent.gameObject;
					newCharacter.GetComponent<cameraDusting> ().radius = xmax / 2;
					newCharacter.GetComponent<cameraDusting> ().yradius = ymax / 2;

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
