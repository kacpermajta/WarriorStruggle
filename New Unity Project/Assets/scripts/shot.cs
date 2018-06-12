using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour {
	public float smooth = 2.0F;
	public bool airborne,physical;
	public float velocity;
	Quaternion target;
	public float damage;
	public AudioClip bodyHit, Weaponhit, mapHit;

	// Use this for initialization
	void Start () {
		if (playerSettings.isServer && airborne) 
		{
			Debug.Log ("wyslij");
			serverScript.SendMissile (gameObject, gameObject.transform.position, gameObject.transform.rotation);
		}
	}
	
	void Update () {

//flying missile movement
		if (airborne) {
			if (transform.eulerAngles.z < 90f|| transform.eulerAngles.z > 270f ) {
				transform.Rotate (0f, 0f, -0.8f+(2*velocity));
			} else {
				transform.Rotate (0f, 0f, 0.8f-(2*velocity));
			}

			transform.Translate (velocity,0f,0f);

		}
	}




	void OnTriggerStay(Collider other) 
	//flying missile hit
	{
		
		if(airborne && other.tag != "nonexist"&& other.tag != "cosmetics")//real hit
		{
			GameObject Hitsound = Instantiate (playerSettings.staticSingleSound, gameObject.transform.position, Quaternion.identity);
			if (other.GetComponent< character_behavior > () != null)//person hit 
			{
				if (other is CapsuleCollider) {//on the head
					other.GetComponent< character_behavior > ().hit (1.5f * damage, transform.eulerAngles);


				} else {//bodyhit
					other.GetComponent< character_behavior > ().hit (damage, transform.eulerAngles);
				}
				Hitsound.GetComponent<AudioSource> ().clip = bodyHit;

			}
			if (other.GetComponent< enviromentDamage > () != null) {//destructible object hit
				other.GetComponent< enviromentDamage > ().hit (damage, transform.eulerAngles);

			}
			if (playerSettings.isClient||playerSettings.isServer) 
			{
				Destroy (gameObject);
			}
			if (physical) 
			{
				Destroy (gameObject.GetComponent<Collider> ());
				Destroy (gameObject.GetComponent<shot> ());
				gameObject.tag = "nonexist";
				if (other.tag == "weapon") 
				{
					//Rigidbody newRigidbody = 
						gameObject.AddComponent<Rigidbody> ();
					Hitsound.GetComponent<AudioSource> ().clip = Weaponhit;
				} else 
				{
					if(	Hitsound.GetComponent<AudioSource> ().clip == null)
						Hitsound.GetComponent<AudioSource> ().clip = mapHit;
					
						
					gameObject.transform.parent = other.transform;
				}
				airborne = false;
			} else if (other.tag != "weapon")
			{
				if(	Hitsound.GetComponent<AudioSource> ().clip == null)
					Hitsound.GetComponent<AudioSource> ().clip = mapHit;
				Destroy (gameObject,0.01f);

				/*
				Destroy(gameObject.GetComponent<Collider>());
				Destroy(gameObject.GetComponent<Rigidbody>());

				foreach(Transform child in transform)
				{
					Destroy (child.gameObject);
				}


				try {Destroy(gameObject.GetComponent<MeshFilter>());}
				finally{
				};
				airborne = false;
			*/

			}
			Hitsound.GetComponent<AudioSource> ().Play();
			Destroy (Hitsound, 5f);
		//	gameObject.GetComponent<AudioSource> ().Play();
		}
	}
}
