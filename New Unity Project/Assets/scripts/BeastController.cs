using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script handles behaviour of beast-like agents
//WIP
public class BeastController : MonoBehaviour {

	public bool isPlayer, isHostile, isGood, isInvader;
	public CharacterController charController;
	float x, y, z, jump, orientation;
	public int leap;
	public float stamina, mana, tilt,mainDist, rearDist;
	public float mapPlane;
	public enum bodyPart {none, follow, paw, uppercut, crush, propel, wave};
	Vector3 calcMov;
	public Vector3 location, mainCast, rearCast;
	//Vector3 aim;
	public Vector3 aim;
	public  bool charUp;
	public  bool charLeft;
	public  bool charRight;
	public  bool charStrike;
	public  bool charSkill;
	public  bool charInteract;
	public  float health;
	public Vector3 aimError;
	RaycastHit hit;


	// Use this for initialization
	void Start () {
		leap = 15;
		health = 15f;
		aimError = new Vector3(0f,0f, 0f);

	}
	
	// Update is called once per frame
	void Update () {
		location= gameObject.GetComponent<Transform>().position;

		//check slope of the terrain agent is over
		Physics.Raycast (location+mainCast, new Vector3 (0f, -1f, 0f), out hit);
		mainDist = hit.distance;
		Physics.Raycast (location+mainCast + rearCast, new Vector3 (0f, -1f, 0f), out hit);
		rearDist = hit.distance;
		if (mainDist - 0.06 > rearDist) {		
			tilt = 1f;
		} else if (mainDist < rearDist) {
			tilt = -1f;
		} else {
			tilt = 0;
		}
		
	}
}
