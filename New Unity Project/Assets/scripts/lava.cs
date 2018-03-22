using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) 
	{
//You died
		if(other.GetComponent< character_behavior > () != null)
		{
			other.GetComponent< character_behavior > ().hit (500f,new Vector3(0f,0f,1f));
		}
	}
}
