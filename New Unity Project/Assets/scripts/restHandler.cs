using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restHandler : MonoBehaviour {

	int charge;

	// Use this for initialization
	void Start () {
		charge = 0;
	}

	// Update is called once per frame
	void Update () {
		if (charge > 0) 
		{
			charge--;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent< character_behavior > () != null && other.GetComponent< character_behavior > ().isPlayer) {
			other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.none;
		}
	}


	void OnTriggerStay(Collider other) {

		if (other.GetComponent< character_behavior > () != null ) {
			if (other.GetComponent<character_behavior> ().health >= other.GetComponent<character_behavior> ().maxhealth) {
				return;
			}
			other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.rest;

			if (other.GetComponent< character_behavior > ().charInteract == true) {	//teleport character
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.none;

				charge += 3;
				if (charge > 300) {
					other.GetComponent<character_behavior>().health++;
					charge = 0;
				}



			}
		}

	}

}
