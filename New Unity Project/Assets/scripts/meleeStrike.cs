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
	public AudioClip bodyHit, weaponHit, dryHit, mapHit;

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
				if (gameObject.tag == "nonexit" || (!offhand && owner.GetComponent< character_behavior > ().stamina > 0)||
					(offhand && owner.GetComponent< character_behavior > ().mana > 0)
				) {
					GameObject Hitsound = Instantiate (playerSettings.staticSingleSound, gameObject.transform.position, Quaternion.identity);

					
					if (other.GetComponent< character_behavior > () != null) {
						Hitsound.GetComponent<AudioSource> ().clip = bodyHit;

						if (other is CapsuleCollider) {
							other.GetComponent< character_behavior > ().hit (1.5f * damage, transform.eulerAngles);

						} else {
							other.GetComponent< character_behavior > ().hit (damage, transform.eulerAngles);
						}
					} else if (other.tag == "weapon") 
					{
						Hitsound.GetComponent<AudioSource> ().clip = weaponHit;
					} else 
					{
						Hitsound.GetComponent<AudioSource> ().clip = mapHit;
					}
				
					if (other.GetComponent< enviromentDamage > () != null) {
						other.GetComponent< enviromentDamage > ().hit (damage, transform.eulerAngles);
					
					
					}
					Hitsound.GetComponent<AudioSource> ().Play();
					Destroy (Hitsound, 5f);
				} else 
				{
					if (!offhand && owner.GetComponent< character_behavior > ().stamina > -40) 
					{
						//gameObject.GetComponent<AudioSource> ().clip = dryHit;
						owner.GetComponent< character_behavior > ().stamina -= 45;
					}					
					if (offhand && owner.GetComponent< character_behavior > ().mana > -40) 
					{
						//gameObject.GetComponent<AudioSource> ().clip = dryHit;
						owner.GetComponent< character_behavior > ().mana -= 45;
					}
				}
				if (gameObject.tag != "nonexist") {
					owner.GetComponent< character_behavior > ().stamina = -15;
				}
				//gameObject.GetComponent<AudioSource> ().Play();
			}

		}
	}
}
