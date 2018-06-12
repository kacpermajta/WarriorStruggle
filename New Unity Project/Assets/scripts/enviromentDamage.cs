using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enviromentDamage : MonoBehaviour {

	public float damage;
	public float health;
	public AudioClip knock, crash;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) 
	{
		//You died
		if(other.GetComponent< character_behavior > () != null)
		{
			other.GetComponent< character_behavior > ().hit (damage,new Vector3(0f,0f,1f));


		}
	}
	public void hit(float damage, Vector3 odrzut)
	{
		health -= damage;
		if (damage > 4)
			AudioSource.PlayClipAtPoint (crash, gameObject.transform.position);
		else
			AudioSource.PlayClipAtPoint (knock, gameObject.transform.position);


		if (health < 0f) {

			Destroy (gameObject.transform.parent.gameObject);
		}
	}
			

}
