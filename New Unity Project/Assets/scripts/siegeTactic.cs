using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siegeTactic : MonoBehaviour {
	public Vector3 siegeTarget;
	public float siegeDistance;
	// Use this for initialization
	void Start () {
		siegeTarget = gameObject.GetComponent<Transform>().position;
	//	siegeTarget.x -= 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) {
		if (other.GetComponent< character_behavior > () != null) {
			other.GetComponent< character_behavior > ().SiegeCounter = 10;
		//	other.GetComponent< character_behavior > ().isSiegeB = true;
			other.GetComponent< character_behavior > ().siegeTarget = siegeTarget;
			other.GetComponent< character_behavior > ().siegeDistance = siegeDistance;

		
		
		}
	
	}
//	void OnTriggerExit(Collider other) {
//		if (other.GetComponent< character_behavior > () != null) {
//
//
//
//		}
//	}


}
