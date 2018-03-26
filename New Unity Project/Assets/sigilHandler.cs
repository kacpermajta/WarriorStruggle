using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sigilHandler : MonoBehaviour {

	public enum sigNature	{death, divine, demonic, spiritual	}

	public sigNature kind;
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

		if (other.GetComponent< character_behavior > () != null && other.GetComponent< character_behavior > ().isPlayer && controller.alive) {

			other.GetComponent< character_behavior > ().aviableInteraction = character_behavior.interaction.sigil;

			if (other.GetComponent< character_behavior > ().charInteract == true) {	//teleport character


				charge += 3;
				if (charge > 300) {
					controller.message = 1;
					controller.changeMessage = true;
					controller.alive = false;
					StartCoroutine (DelayedJump ());
				}


		
			}
		}

	}
	IEnumerator DelayedJump(){

		yield return new WaitForSeconds(5f);
		SceneManager.LoadScene("levels");


	}
}
