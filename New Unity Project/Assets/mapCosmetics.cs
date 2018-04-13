using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCosmetics : MonoBehaviour {
	public enum mapType {ground, building};
	public mapType groundType;
	public Mesh[] altMesh;
	public GameObject plant;
	public Vector3 tileLocation;
	GameObject newPlant;
	// Use this for initialization
	void Start () {
		tileLocation = gameObject.transform.position;
		if (groundType == mapType.ground) {
			gameObject.GetComponent<MeshFilter> ().mesh = altMesh [0];
			for (int j=0; j < 5; j++) { 
				Vector3 location = new Vector3 (tileLocation.x - 4f + (Random.value * (8f)), tileLocation.y + 4f, tileLocation.z);

				RaycastHit nest;
				//aim.z = mapPlane;
				Physics.Raycast (location, new Vector3 (0f, -1f, 0f), out nest);
				//					Vector3 spawnPoint = nest.point;
				//					spawnPoint.z = mapPlane;

				if(nest.transform.gameObject.GetComponent<mapCosmetics>()!=null && 
					nest.transform.gameObject.GetComponent<mapCosmetics>().groundType==mapType.ground)
					newPlant = Instantiate (plant, nest.point, Quaternion.identity);

				// = Instantiate (plant, location, Quaternion.identity);
				//newCharacter.transform.parent = gameObject.transform.parent.transform.parent.transform.GetChild (0);
				//newCharacter.GetComponent< character_behavior > ().mapPlane = location.z;
			}
		}
	}
	
	// Update is called once per frame
}
