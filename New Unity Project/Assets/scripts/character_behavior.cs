using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character_behavior : MonoBehaviour {
	public enum equipment {none, spell, bow, axe,sword, xbow, spear, shield, dagger, sideWeapon, conjure};
	public enum interaction {none, passage, sigil, body, door};
	public interaction aviableInteraction;

	public bool isPlayer,  isGood, isInvader, isGuardian;
	public float exhaust,offexhaust, speed, damage, secDamage;
	public equipment weapon;

	public equipment offEquipment;
	public GameObject arrow;
	public GameObject bowModel,shieldModel;
	public  float health;

	public Canvas charCanvas;
	public Canvas  textDisplayerPrefab;
	public Text textContent;
	public int SiegeCounter;
	public bool isHostile, isSiege;

	public string displayedMessage;
	public CharacterController charController;
	float x, y, z, jump, orientation;
	public int leap;
	public float stamina, mana, slash, thrust,offslash;
	public float  mapPlane,penShift;
	Vector3 calcMov;
	public Vector3 location,guard, leftGuard,lokacja;
	public GameObject weaponModel, offhandModel;
	public Vector3 aim;
	public  bool charUp, charLeft, charRight, charStrike, charSkill, charInteract;
	public Vector3 aimError;
	public Vector3 siegeTarget;
	public float siegeDistance;

	// Use this for initialization
	void Start () {
		charController = gameObject.GetComponent<CharacterController>();
		leap = 15;
		location= gameObject.GetComponent<Transform>().position;
		location.y += 0.5f;
//eq initialisation
		weaponModel= GameObject.Instantiate(bowModel, location, Quaternion.identity);
		weaponModel.transform.parent = gameObject.transform;
		displayedMessage = "";
		charCanvas=GameObject.Instantiate(textDisplayerPrefab, new Vector3 (0f, 0f, 0f), Quaternion.identity);
//		textDisplayer=new Text

		Transform child =  charCanvas.transform.Find("Text");
		textContent = child.GetComponent<Text>();

		//textContent= charCanvas.GetComponent<Text>();

		charCanvas.transform.SetParent (gameObject.transform,false);

		if (weapon == equipment.axe||weapon == equipment.sword||weapon == equipment.spear) {
			weaponModel.GetComponent< meleeStrike> ().owner = gameObject;
		}


		if (shieldModel != null&&offEquipment!=equipment.conjure) {
			offhandModel = GameObject.Instantiate (shieldModel, location, Quaternion.identity);
			offhandModel.transform.parent = gameObject.transform;
			if(offEquipment==equipment.sideWeapon)
			{
				offhandModel.GetComponent< meleeStrike> ().owner = gameObject;
			}
		}


		if (health == 0f) 
		{
			health = 15f;
		}
		aimError = new Vector3(0f,0f, 0f);
		slash = 0f;
		offslash = 0f;
		speed = 0.015f;
		charLeft = false;
		charRight = false;
		charUp = false;
		charStrike = false;
		if (weaponModel.GetComponent< meleeStrike > () != null) 
		{
			weaponModel.GetComponent< meleeStrike > ().damage += damage;
		}
		if(isPlayer==false)
		{
			
			if (weapon == equipment.sword && playerSettings.difficulty < 5) 
			{
				speed += (playerSettings.difficulty - 5) * 0.002f;
			} else 
			{
				speed += (playerSettings.difficulty - 5) * 0.001f;
			}
			if (weaponModel.GetComponent< meleeStrike > () != null) 
			{
			weaponModel.GetComponent< meleeStrike > ().damage*=playerSettings.difficulty/5f;
			}
			damage*=playerSettings.difficulty/5f;
			health+= 3*(playerSettings.difficulty - 5);
		}
	}

	void Update () {



		x *= 0.86f;//drag
		y = -0.13f;//gravity
		jump = 0;

		lokacja=gameObject.GetComponent<Transform>().position;

		if ((charController.collisionFlags & CollisionFlags.Below) != 0) 
		{	
//character on ground and can jump again
			leap = 15;
		}
		if (isPlayer) 
		{	
//apply controller values
			aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			charUp=controller.moveUp;
			charRight = controller.moveRight;
			charLeft= controller.moveLeft;
			charStrike = controller.Strike;
			charSkill = controller.Skill;
			charInteract = controller.Interact;
		}
//movement commends
		if (charUp) {				
				jump = 0.2f;
				leap --;							
			} 

		if (charRight) {
			x += speed;
				jump += 0.03f;

			}
		if (charLeft){
			x += -speed;
				jump += 0.03f;
			}
			if (leap < 0) {
				jump = 0.0f;
			}

		location= gameObject.GetComponent<Transform>().position;
		location.y += 0.7f;

//weapon transforms
		guard = location;
		if (aim.x - location.x < 0) {

			guard.x -= 0.5f;
		} else {

			guard.x += 0.5f;

		}

		orientation = Mathf.Atan ((aim.y - guard.y) / (aim.x - guard.x));

//eq shield
		if (offhandModel != null && offEquipment==equipment.shield ) 
		{
			leftGuard = guard;
//move shield
			if (charSkill) {
				if (aim.x - location.x < 0)
				{

					leftGuard.y -= 0.5f * Mathf.Sin (orientation);
				} else {
					leftGuard.y += 0.5f * Mathf.Sin (orientation);
				}
			}
			
			leftGuard.y -= 0.3f;
			offhandModel.transform.position = leftGuard;

//left or right
			if (aim.x - location.x < 0) 
			{
				offhandModel.transform.eulerAngles = (new Vector3 (0f, 180f, 0f));
			} else 
			{
				offhandModel.transform.eulerAngles = (new Vector3 (0f, 0f, 0f));
			}
		}



//eq thrown
		if (arrow != null && offEquipment==equipment.dagger )
		{
			leftGuard = guard;
			leftGuard.y -= 0.3f;
			if (mana > 0) {
				if (charSkill) {

					GameObject missile = GameObject.Instantiate (arrow, guard - new Vector3 (0f, 0.7f, 0f), Quaternion.identity);

					if (aim.x - guard.x < 0) 
					{
						missile.transform.Translate (0.5f * Mathf.Cos (orientation + 3.14f), 0.7f + 0.5f * Mathf.Sin (orientation + 3.14f), 0f);

						missile.transform.Rotate (180f, 0f, -(3.14f + orientation) * 360f / 6.28f);

					} else 
					{
						missile.transform.Translate (0.5f * Mathf.Cos (orientation), 0.7f + 0.5f * Mathf.Sin (orientation), 0f);

						missile.transform.Rotate (0f, 0f, orientation * 360f / 6.28f);

					}
					missile.GetComponent< shot > ().damage += secDamage;
					missile.GetComponent< shot > ().velocity += 0.2f;
					missile.GetComponent< shot > ().airborne = true;

					mana -= 200f;
					if(offhandModel==null)
					{
						stamina -= 200;
					}

				}
			}
			if (offhandModel != null) {
				if (mana > -20f) {
					if (aim.x - location.x < 0) {
						offhandModel.transform.eulerAngles = (new Vector3 (0f, 180f, 40f));
					} else {
						offhandModel.transform.eulerAngles = (new Vector3 (0f, 0f, 40f));
					}


				} else {//no stamina
				
					if (aim.x - location.x < 0) {
						leftGuard += (new Vector3 (0.5f, -0.5f, 40f));
					} else {

						leftGuard += (new Vector3 (-0.5f, -0.5f, 40f));
					}
					offhandModel.transform.eulerAngles = (new Vector3 (0f, 90f, 90f));

				}
				offhandModel.transform.position = leftGuard;
			}  


		}
//eq spells
		if (arrow != null && offEquipment==equipment.spell ) {
			leftGuard = guard;
			leftGuard.y -= 0.3f;
			if (mana > -200) {
				if (charSkill) {
					GameObject missile = GameObject.Instantiate (arrow, guard - new Vector3 (0f, 0.7f, 0f), Quaternion.identity);

					if (aim.x - guard.x < 0) {
						missile.transform.Translate (0.5f * Mathf.Cos (orientation + 3.14f), 0.7f + 0.5f * Mathf.Sin (orientation + 3.14f), 0f);

						missile.transform.Rotate (0f, 0f, (2.64f + orientation+Random.value) * 360f / 6.28f);

					} else {
						missile.transform.Translate (0.5f * Mathf.Cos (orientation), 0.7f + 0.5f * Mathf.Sin (orientation), 0f);

						missile.transform.Rotate (0f, 0f, (orientation +Random.value-0.5f)* 360f / 6.28f);

					}
					missile.GetComponent< shot > ().damage += secDamage;
					missile.GetComponent< shot > ().velocity += 0.2f;
					missile.GetComponent< shot > ().airborne = true;

					mana -= 100f;


				}
			}



			} 
		//eq conjure
		if (shieldModel != null && offEquipment==equipment.conjure ) {
			
			if (mana > 0) {
				if (charSkill) {
					GameObject missile;
					if (shieldModel.GetComponent<shot> () == null) {

						RaycastHit nest;
						aim.z = mapPlane;
						Physics.Raycast (aim, new Vector3 (0f, -1f, 0f), out nest);
//					Vector3 spawnPoint = nest.point;
//					spawnPoint.z = mapPlane;

										
						missile = GameObject.Instantiate (shieldModel, nest.point, Quaternion.identity);
					} else {
						aim.z = mapPlane;

						missile = GameObject.Instantiate (shieldModel, aim, Quaternion.identity);
					}
					if (aim.x - guard.x > 0) {
					//	missile.transform.Translate (0.5f * Mathf.Cos (orientation + 3.14f), 0.7f + 0.5f * Mathf.Sin (orientation + 3.14f), 0f);

						missile.transform.Rotate (0f, 180f, 0f);

					} 
					mana -= offexhaust;


				}
			}



		} 


		//eq offhand blade
		if (offEquipment == equipment.sideWeapon) 
		{
			leftGuard = guard;
			leftGuard.y += 0.4f;
			offhandModel.transform.position = guard;

			aimError.y += 0.1f*(Random.value-0.5f-aimError.y);


			if (mana > 0f && charSkill ) 
			{
				offslash -= (mana) / 40f;
				mana -= 2.5f;
				if (mana < 3f & offslash > 0f) 
				{
					offslash = 0.3f;

				}
			} else {

				offslash = 0.3f;
			}


			if (mana < -3f) 
			{
				offslash += 3f;

			}

			if (aim.x - location.x < 0) 
			{

				offhandModel.transform.eulerAngles = (new Vector3 (0f, 0f, (orientation+offslash) * 360f / 6.28f));
				offhandModel.transform.Rotate(new Vector3 (0f, 180f, 0f));

			} else

			{

				offhandModel.transform.eulerAngles = (new Vector3 (0f, 0f, (orientation-offslash) * 360f / 6.28f));
			}
			if (Mathf.Abs (aim.x - location.x) < 0.5f) {
				offhandModel.transform.Rotate (new Vector3 (0f, 0f, 180f));

			}




		}



//eq 1hand
		if (weapon == equipment.axe) 
		{
			weaponModel.transform.position = guard;

			aimError.y += 0.1f*(Random.value-0.5f-aimError.y);


			if (stamina > 0f && charStrike ) 
			{
				slash += (stamina) / 35f;
				stamina -= 2.5f;
				if (stamina < 3f & slash < 0f) 
				{
					slash =-0.5f;

				}
			} else 
			{

				slash = -0.5f;
			}


			if (stamina < -30f) 
			{
				penShift = 1000f;

			} else {
				penShift = 0f;
			}		
		}

//eq 2hand
		if (weapon == equipment.sword) 
		{
			guard.y += 0.4f;
			weaponModel.transform.position = guard;

			aimError.y += 0.1f*(Random.value-0.5f-aimError.y);


			if (stamina > 0f && charStrike ) 
			{
				slash -= (stamina) / 20f;
				stamina -= 2.5f;
				if (stamina < 3f & slash > 0f) 
				{
					slash = 0.3f;
				
				}
			} else {

				slash = 0.3f;
			}


			if (stamina < -30f) 
			{
				penShift = 1000f;

			} else {
				penShift = 0f;
			}

		}
//eq thrust
		if (weapon == equipment.spear) 
		{
			slash = 0f;
			guard.y -= 0.1f;
			weaponModel.transform.position = guard;

			aimError.y += 0.1f*(Random.value-0.5f-aimError.y);


			if (stamina > 0f && charStrike ) 
			{
				thrust -= (stamina) / 50f;
				slash -= (stamina) /80f;
				stamina -= 2f;
				if (stamina < 3f && thrust < 0f) 
				{
					slash = 0f;
					thrust = -0.3f;

				}
			} else 
			{

				slash = 0f;
				thrust = -0.3f;
			}


			if (stamina < -30f) {
				penShift = 1000f;

			} else {
				penShift = 0f;
			}
		}
			

//eq simple shot
		if (weapon== equipment.xbow) 
		{
			aimError.x += 0.1f*(Random.value-0.5f-aimError.x);
			weaponModel.transform.position = guard;

			if (charStrike){
				if (stamina > 0 && Mathf.Abs(aim.x-location.x)>Mathf.Abs(aim.x-guard.x)) 
				{
					
					GameObject missile = GameObject.Instantiate(arrow, guard-new Vector3(0f,0.7f,0f), Quaternion.identity);
					if (aim.x - guard.x < 0) {
						missile.transform.Translate (0.5f*Mathf.Cos(orientation+3.14f),0.7f+0.5f*Mathf.Sin(orientation+3.14f),0f);

						missile.transform.Rotate(0f,0f,(3.14f+orientation)*360f/6.28f);


					} else 
					{
						missile.transform.Translate (0.5f*Mathf.Cos(orientation),0.7f+0.5f*Mathf.Sin(orientation),0f);

						missile.transform.Rotate(0f,0f,orientation*360f/6.28f);

					}
					missile.GetComponent< shot > ().damage+=damage;
					missile.GetComponent< shot > ().velocity+=0.3f;
					missile.GetComponent< shot > ().airborne = true;

					stamina -= exhaust;

				}
					
			}
		
		}


//eq bow
		if (weapon== equipment.bow) 
		{
			aimError.x += 0.1f*(Random.value-0.5f-aimError.x);
			weaponModel.transform.position = guard;

			if (offhandModel != null) 
			{
				if (offhandModel.GetComponent< shot > () == null) 
				{
					offhandModel = null;

					stamina = -50f;
				}else if (charStrike && stamina > 0f) 
				{
					//draw
					
					offhandModel.GetComponent< shot > ().damage += 0.5f;
					offhandModel.GetComponent< shot > ().velocity += 0.01f;
					stamina = stamina - 3f;
					offhandModel.GetComponent< shot > ().airborne = false;
					offhandModel.transform.Translate (-0.05f, 0f, 0f);

				} else 
				{

					//loose
					offhandModel.GetComponent< shot > ().airborne = true;
					offhandModel.transform.parent = gameObject.transform.parent.transform.parent  ;

					offhandModel = null;
					stamina = -50f;

				}
			}else if(charStrike && stamina >14f&&Mathf.Abs(aim.x-location.x)>Mathf.Abs(aim.x-guard.x))
			{
				//nock
				GameObject missile = GameObject.Instantiate(arrow, guard-new Vector3(0f,0.7f,0f), Quaternion.identity);
				offhandModel = missile;
				offhandModel.GetComponent< shot > ().airborne = false;
				missile.GetComponent< shot > ().damage+=damage;
				missile.GetComponent< shot > ().velocity=0.15f;
				missile.transform.parent = gameObject.GetComponent< character_behavior > ().weaponModel.transform;

				if (aim.x - guard.x < 0) {
					missile.transform.Translate (0.06f*stamina*Mathf.Cos(orientation+3.14f),0.7f+0.06f*stamina*Mathf.Sin(orientation+3.14f),0f);

					missile.transform.Rotate(0f,0f,(3.14f+orientation)*360f/6.28f);

				} else 
				{
					missile.transform.Translate (0.06f*stamina*Mathf.Cos(orientation),0.7f+0.06f*stamina*Mathf.Sin(orientation),0f);
					missile.transform.Rotate(0f,0f,orientation*360f/6.28f);

				}


			}


		}





//refill
		if (stamina<15f)
		{

			stamina++;
		}
		if (mana<15f)
		{
			mana++;
		}

//apply weapon transform
		if (aim.x - location.x < 0) 
		{

			weaponModel.transform.eulerAngles = (new Vector3 (0f, 0f, (orientation+slash) * 360f / 6.28f));
			weaponModel.transform.Rotate(new Vector3 (0f, 180f, 0f));

		} else
			
		{

			weaponModel.transform.eulerAngles = (new Vector3 (0f, 0f, (orientation-slash) * 360f / 6.28f));
		}
		if (Mathf.Abs (aim.x - location.x) < 0.5f) {
			weaponModel.transform.Rotate (new Vector3 (0f, 0f, 180f));
		
		}
		weaponModel.transform.Translate(new Vector3 (-thrust, 0f, penShift));

		calcMov.x=x;
		calcMov.y=jump+y;
		calcMov.z=mapPlane-location.z;
		charController.Move(calcMov);

		if (aviableInteraction == interaction.passage)
			displayedMessage = "Press F to go through passage";
		else if (aviableInteraction == interaction.sigil)
			displayedMessage = "Hold F to activate sigil";
		
		else
			displayedMessage = "";
		
		textContent.text = displayedMessage;

		if (SiegeCounter > 0) {
			isSiege = true;
			SiegeCounter--;
		} else
			isSiege = false;

	}



//function when character is hit
	public void hit(float damage, Vector3 odrzut)
	{
		health -= damage;
		if (health < 0f) 
		{
			if (isPlayer) 
			{
				controller.alive = false;
			//	controller.ShowDelayed ();
			}
			textContent.text = "";
			//Destroy(charCanvas);

			Destroy(gameObject.GetComponent< character_behavior > ());
			Destroy(gameObject.GetComponent< CharacterController > ());
			gameObject.GetComponent< Rigidbody > ().useGravity = true;
			gameObject.GetComponent< Rigidbody > ().isKinematic = false;
			gameObject.GetComponent< Rigidbody > ().AddTorque (odrzut/2);
			SphereCollider ballCollider = gameObject.AddComponent<SphereCollider>();
			ballCollider.radius = 0.25f;
			gameObject.transform.parent = gameObject.transform.parent.transform.parent;


			if (weaponModel.GetComponent< Rigidbody > () == null) 
			{
				//Rigidbody newRigidbody = 
					weaponModel.AddComponent<Rigidbody> ();
			}
			Destroy(weaponModel.GetComponent< meleeStrike > ());

			if (offhandModel!= null&&offhandModel.GetComponent< meleeStrike > () != null) 
			{
				Destroy (offhandModel.GetComponent< meleeStrike > ());
			}
			if (offhandModel != null && offhandModel.GetComponent< Rigidbody > () == null )
			{
				//Rigidbody offRigidbody = 
					offhandModel.AddComponent<Rigidbody>();			
			}
		
		}
	}



}
