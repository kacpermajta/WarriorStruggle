using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eject : MonoBehaviour {
	bool offhand;
//	int endurance;
//	int angle;
	GameObject owner;
	public Vector3 tempVec, origPos, ejectPos;
	// Use this for initialization
	void Start () {
		owner = gameObject.transform.parent.gameObject;
		if (owner.GetComponent<character_behavior>().offEquipment == character_behavior.equipment.flight)
		{
			offhand = true;
		}
	}

	// Update is called once per frame
	void Update () {
		if (owner==null || owner.GetComponent<character_behavior> () == null  )
			Destroy (gameObject.GetComponent<flap> ());

		else if (offhand && owner.GetComponent<character_behavior> ().charSkill) {
			transform.localPosition = ejectPos;
		
		
		} else {
		
			transform.localPosition = origPos;

		}

		
	}
}
