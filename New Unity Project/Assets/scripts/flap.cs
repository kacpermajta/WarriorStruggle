using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flap : MonoBehaviour {
	bool offhand;
	int endurance;
	int angle;
	GameObject owner;
	Vector3 tempVec;
	// Use this for initialization
	void Start () {
		angle = 0;
		endurance = 10;
		owner = gameObject.transform.parent.gameObject;
		if (owner.GetComponent<character_behavior>().offEquipment == character_behavior.equipment.flight)
		{
			offhand = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (owner.GetComponent<character_behavior> () == null)
		{
			Destroy (gameObject.GetComponent<flap> ());
		}
		else if (offhand && owner.GetComponent<character_behavior> ().charSkill) 
		{
			if (endurance > 0)
				angle+=2;
			else
				angle -= 3;


			endurance--;
			if (endurance < -10) {
				endurance = 10;
				angle = 0;
			}
				
			tempVec = transform.eulerAngles;
			tempVec.z += angle;
			transform.eulerAngles = tempVec;
		
		
		} else 
		{
		
			angle = 0;
		}

		
	}
}
