using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openerScript : MonoBehaviour {

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
				other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.door;

			}

			if(other.GetComponent< character_behavior > ().stamina >0&& other.GetComponent< character_behavior > ().charInteract ==true)
			{	//teleport character
				transform.parent.Translate (new Vector3(0f,0f,2f));
				StartCoroutine (GoBack ());
				other.GetComponent< character_behavior > ().stamina = -60f;

			}
		}
	}
	IEnumerator GoBack()
	{

		yield return new WaitForSeconds(3f);
		transform.parent.Translate (new Vector3(0f,0f,-2f));


	}
}
