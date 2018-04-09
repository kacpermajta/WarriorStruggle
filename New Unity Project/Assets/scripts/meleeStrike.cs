using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeStrike : MonoBehaviour {
	public float damage;
	public GameObject owner;
	public GameObject victim;
	public float range;
	public bool offhand;
	public Vector3 positioning;

	// Use this for initialization
	void Start () {
//		damage = 10;
	}

	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerStay(Collider other) 
	{
		


		if(other!=null && 
			(//owner==null||owner.GetComponent< character_behavior > ()!=null &&
				(!offhand && (owner.GetComponent< character_behavior > ().charStrike|| 
				owner.GetComponent< character_behavior > ().weapon==character_behavior.equipment.conjure))||
				(offhand &&owner.GetComponent< character_behavior > ().charSkill)))
		{	
			if( other.transform.gameObject != owner &&other.tag != "nonexist")
			{	//weapon hit
				if (gameObject.tag=="nonexit" || owner.GetComponent< character_behavior > ().stamina > 0) 
				{
					
					if (other.GetComponent< character_behavior > () != null) 
					{
						

						if (other is CapsuleCollider) {
							other.GetComponent< character_behavior > ().hit (1.5f * damage, transform.eulerAngles);

						} else {
							other.GetComponent< character_behavior > ().hit (damage, transform.eulerAngles);
						}
					}
					if (other.GetComponent< enviromentDamage > () != null) {
						other.GetComponent< enviromentDamage > ().hit (damage, transform.eulerAngles);
					
					
					}

				}
				if (gameObject.tag != "nonexist") {
					owner.GetComponent< character_behavior > ().stamina = -15;
				}
			}
		
		}
	}
}
