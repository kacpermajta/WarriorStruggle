using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaAttack : MonoBehaviour {

	public float damage;
	public GameObject owner;
	public GameObject victim;
	public Vector3 positioning;
	public bool oriented;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) 
	{



		if(other!=null && 
			 other.transform.gameObject != owner &&other.tag != "nonexist")
			{	//weapon hit


					if (other.GetComponent< character_behavior > () != null) 
					{


				if (other is CharacterController) {
							other.GetComponent< character_behavior > ().hit (1f * damage, transform.eulerAngles);

						} 
					}
					if (other.GetComponent< enviromentDamage > () != null) {
						other.GetComponent< enviromentDamage > ().hit (damage, transform.eulerAngles);


					}

			}
			


	}
}
