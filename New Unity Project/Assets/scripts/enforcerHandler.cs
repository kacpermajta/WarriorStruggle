using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enforcerHandler : MonoBehaviour {
	public Vector3 target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) {
//when character enters it is sent back
		if (other.GetComponent< character_behavior > () != null) 
		{
			other.transform.Translate (target);

			other.GetComponent< character_behavior > ().mapPlane = target.z;
			if (other.GetComponent< character_behavior > ().isPlayer) 
			{
				controller.cameraPlane = target.z;
			}
		}
	}
}
