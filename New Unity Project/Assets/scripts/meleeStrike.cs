﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeStrike : MonoBehaviour {
	public int damage;
	public GameObject owner;
	public GameObject victim;
	public float range;

	// Use this for initialization
	void Start () {
		damage = 10;
	}

	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other) 
	{
		
		if(owner.GetComponent< character_behavior > ().charStrike)
		{	
			if( other.transform.gameObject != owner &&other.tag != "nonexist")
			{	//weapon hit

				if (other.GetComponent< character_behavior > () != null) 
				{
					if (owner.GetComponent< character_behavior > ().stamina > 0) 
					{
						other.GetComponent< character_behavior > ().hit (damage, transform.eulerAngles);

					}

				}

				owner.GetComponent< character_behavior > ().stamina = -15;

			}
		
		}
	}
}