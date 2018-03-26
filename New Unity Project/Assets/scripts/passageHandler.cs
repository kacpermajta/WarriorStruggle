using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passageHandler : MonoBehaviour {
	public Vector3 target;
	public bool entrance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent< character_behavior > () != null && other.GetComponent< character_behavior > ().isPlayer) {
			other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.none;
		}
	}



	void OnTriggerStay(Collider other) {
		
		if(other.GetComponent< character_behavior > () != null){ 
			if (other.GetComponent< character_behavior > ().isPlayer) {
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.passage;

			}

		if(other.GetComponent< character_behavior > ().stamina >0&& other.GetComponent< character_behavior > ().charInteract ==true)
		{	//teleport character
			other.transform.Translate (target);
			other.GetComponent< character_behavior > ().mapPlane = target.z;
			if (other.GetComponent< character_behavior > ().isPlayer) 
			{
				controller.cameraPlane = target.z;
			}
			other.GetComponent< character_behavior > ().stamina = -60f;

		}
		}
	}
}
