using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ladder : MonoBehaviour {

//	public float damage;
//	public float health;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) 
	{
		//ladder up up up
		if(other.GetComponent< character_behavior > () != null && other is CharacterController)
		{
			other.GetComponent< character_behavior > ().isClimbing=true;


		}
	}
	void OnTriggerExit(Collider other) 
	{
		//ladder up up up
		if(other.GetComponent< character_behavior > () != null && other is CharacterController)
		{
			other.GetComponent< character_behavior > ().isClimbing=false;


		}
	}

			

}
