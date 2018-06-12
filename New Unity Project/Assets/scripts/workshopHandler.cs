using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workshopHandler : MonoBehaviour {

	int charge;
	public GameObject prefab;
	public character_behavior.interaction interaction;

	// Use this for initialization
	void Start () {
		charge = 0;
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

		if (other.GetComponent< character_behavior > () != null && other.GetComponent< character_behavior > ().isPlayer) {
			if (other.GetComponent<character_behavior> ().buildingMaterials !=null) {
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.build;
				return;
			}
			other.GetComponent< character_behavior > ().aviableInteraction = interaction;

			if (other.GetComponent< character_behavior > ().charInteract == true) {	//teleport character

				charge += 2;
				if (charge > 300) {
					other.GetComponent<character_behavior>().buildingMaterials=prefab;
					charge = 0;
				}



			}
		}

	}
}
