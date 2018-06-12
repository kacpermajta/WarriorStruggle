using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour {

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
			if (gameObject.transform.parent.parent == other.transform) 
			{
				gameObject.transform.parent.parent = other.transform.parent.parent;
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.none;
				other.GetComponent< character_behavior > ().isWorking = false;
			}
		}
	}



	void OnTriggerStay(Collider other) {

		if(other.GetComponent< character_behavior > () != null){ 
			if (other.GetComponent< character_behavior > ().isPlayer&& !other.GetComponent< character_behavior > ().isWorking) {
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.body;

			}

			if(other.GetComponent< character_behavior > ().stamina >0&& other.GetComponent< character_behavior > ().charInteract ==true)
			{	
				transform.parent.parent=other.transform;
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.carrying;
				other.GetComponent< character_behavior > ().isWorking = true;
				other.GetComponent< character_behavior > ().stamina = -60f;

			}
		}
	}

}
