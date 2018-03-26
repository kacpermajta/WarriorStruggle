using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script handles control of AI agents
public class botControl : MonoBehaviour {
	public enum equipment {holyBolt, bow, axe,sword, xbow};
	public Transform pathFinder;
	float range, curDist, checkDist;
	Vector3 temporary,botLocation;
	bool temporaryBool;
	Transform mainHero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		foreach(Transform child in transform)//loop through agents to control
		{
			if (!child.GetComponent< character_behavior > ().isPlayer) //loop through potential targets
			{
				curDist = 10000f;

				//reset values
				child.GetComponent< character_behavior > ().charInteract = false;
				child.GetComponent< character_behavior > ().charLeft = false;
				child.GetComponent< character_behavior > ().charRight = false;
				mainHero = null;
				botLocation = child.GetComponent< character_behavior > ().location;

				foreach (Transform agent in transform) //loop through potential target agents
				{
					//check if target agents can be considered victim
					if (child.GetComponent< character_behavior > ().isGood != agent.GetComponent< character_behavior > ().isGood && agent.GetComponent< character_behavior > ().mapPlane == child.GetComponent< character_behavior > ().mapPlane && !Physics.Linecast (agent.GetComponent< character_behavior > ().location, botLocation)) 
					{
						checkDist = Vector3.Distance (agent.GetComponent< character_behavior > ().location, botLocation);
						//check if target is better than previously selected
						if (checkDist < curDist) {
							curDist = checkDist;
							mainHero = agent;
						}
					}
				}

				//if no victim selected
				if (mainHero == null) {
					child.GetComponent< character_behavior > ().charStrike = false;

					//checked is controlled agent should invade 
					if (child.GetComponent< character_behavior > ().isInvader) {
						foreach (Transform agent in pathFinder) {
							temporary = agent.GetComponent< Transform > ().localPosition;
							if (child.GetComponent< character_behavior > ().mapPlane == temporary.z) {
								if (agent.GetComponent< passageHandler > ().entrance) {
									if (child.GetComponent< character_behavior > ().location.x  > temporary.x+0.6f) {
										child.GetComponent< character_behavior > ().charLeft = true;
										child.GetComponent< character_behavior > ().charRight = false;
									} else if (child.GetComponent< character_behavior > ().location.x  < temporary.x-0.6f) {
										child.GetComponent< character_behavior > ().charRight = true;
										child.GetComponent< character_behavior > ().charLeft = false;
									} else {
										child.GetComponent< character_behavior > ().charInteract = true;
										child.GetComponent< character_behavior > ().charRight = false;
										child.GetComponent< character_behavior > ().charLeft = false;
									}

							
								} 
							}
						}
					}
				} else 
				{
					
					temporary = mainHero.GetComponent< character_behavior > ().location;

					//check if range troop should account for distance
					if (child.GetComponent< character_behavior > ().weapon == character_behavior.equipment.bow) 
					{
						temporary.y += Mathf.Exp (Mathf.Abs (temporary.x - botLocation.x) * 0.12f) * 0.32f;
						temporary += 3 * child.GetComponent< character_behavior > ().aimError;
					}


					child.GetComponent< character_behavior > ().aim = temporary;

					//enemy in sight

					if ((child.GetComponent< character_behavior > ().weapon == character_behavior.equipment.bow ||
						child.GetComponent< character_behavior > ().weapon == character_behavior.equipment.xbow)
						&& Mathf.Abs(temporary.x-botLocation.x)>Mathf.Abs(temporary.x-child.GetComponent< character_behavior > ().guard.x)) 
					{
						
						range = 30;
						child.GetComponent< character_behavior > ().charStrike = true;
					} 
					else if (child.GetComponent< character_behavior > ().weapon == character_behavior.equipment.axe ||
					            child.GetComponent< character_behavior > ().weapon == character_behavior.equipment.sword ||
					            child.GetComponent< character_behavior > ().weapon == character_behavior.equipment.spear) 
					{
						
						if (child.GetComponent< character_behavior > ().weaponModel != null) 
						{
							range = child.GetComponent< character_behavior > ().weaponModel.GetComponent< meleeStrike > ().range;
						} else 
						{
							range = 0f;
						}

						if (Vector3.Distance (child.GetComponent< character_behavior > ().aim, child.GetComponent< character_behavior > ().guard) > range+0.1f) 
						{	//victim too far to strike
							
							child.GetComponent< character_behavior > ().charStrike = false;

						} else 
						{
							if (child.GetComponent< character_behavior > ().charStrike == true &&
							       child.GetComponent< character_behavior > ().stamina > 0) 
							{	//attact continued
								child.GetComponent< character_behavior > ().charStrike = true;
							} else if (child.GetComponent< character_behavior > ().stamina > 14) 
							{	//attack start
								child.GetComponent< character_behavior > ().charStrike = true;

							} else 
							{	//no attack for you, you get nothing, you loose, good day sir!
								child.GetComponent< character_behavior > ().charStrike = false;


							}
						}
					}
								
					//remember if victim is right
					temporaryBool = (child.GetComponent< character_behavior > ().aim.x - botLocation.x > 0f);
					//check if we are on victim
					if (Mathf.Abs (child.GetComponent< character_behavior > ().aim.x - botLocation.x) < 0.7f) {
						temporaryBool = !temporaryBool;
					}
					//victim too close
					if (Vector3.Distance (child.GetComponent< character_behavior > ().aim, child.GetComponent< character_behavior > ().guard) > range)
					{

						child.GetComponent< character_behavior > ().charRight = temporaryBool;
						child.GetComponent< character_behavior > ().charLeft = !temporaryBool;

					} else 
						//victim too far
					{			
						if (!child.GetComponent< character_behavior > ().isGuardian) 
						{
							child.GetComponent< character_behavior > ().charRight = !temporaryBool;
							child.GetComponent< character_behavior > ().charLeft = temporaryBool;
						}

					} 

				}//end of victim script
				if ((child.GetComponent< character_behavior > ().charRight || child.GetComponent< character_behavior > ().charLeft) && !child.GetComponent< character_behavior > ().charStrike && (child.GetComponent<CharacterController> ().collisionFlags & CollisionFlags.Sides) != 0) {
					child.GetComponent< character_behavior > ().charUp = true;
				} else {
					child.GetComponent< character_behavior > ().charUp = false;
				}
			}///end of single bot script


		}//end of single agent iteration
		
	}
}
