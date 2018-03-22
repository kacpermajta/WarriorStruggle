using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_start : MonoBehaviour {
	public GameObject character;
	public GameObject newCharacter;
	// Use this for initialization
	void Start () {

//use chosen player hero unless set otherwise
		if (character == null) 
		{
			character = playerSettings.playerHero;
		}
//spawn character
		Vector3 location= gameObject.GetComponent<Transform>().position;
		newCharacter= Instantiate(character, location, Quaternion.identity);
		newCharacter.transform.parent = gameObject.transform.parent.transform.parent.transform.GetChild (0);
		newCharacter.GetComponent< character_behavior > ().mapPlane = location.z;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
