using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBackAndForth : MonoBehaviour {

	public Vector3 movement;
	public bool backwards;
	Vector3 tempMovement;
	public Vector3 minBoundry;
	public Vector3 maxBoundry;
	// Use this for initialization
	void Start () {
		backwards=(Random.value > 0.5f);
		//napisz tu normalizacje min i maxboundry;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x > maxBoundry.x || gameObject.transform.position.y > maxBoundry.y || gameObject.transform.position.z > maxBoundry.z) 
		{
			backwards = true;
		}
		else if(gameObject.transform.position.x < minBoundry.x || gameObject.transform.position.y < minBoundry.y || gameObject.transform.position.z < minBoundry.z)
		{
			backwards = false;
		}
		if (backwards)
			tempMovement = -movement;
		else
			tempMovement = movement;
		gameObject.transform.position += tempMovement / 100;

	}
}
