using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDusting : MonoBehaviour {


	public Vector3 movement;
	public bool backwards;
	Vector3 tempMovement;
	public float radius, xradius, yradius;
	Vector3  centralLoc, cloudLoc;
//	public Vector3 minBoundry;
//	public Vector3 maxBoundry;
	public GameObject pivot;
	// Use this for initialization
	void Start () {
		backwards=(Random.value > 0.5f);
//		radius = 25f;
//		xradius = 25.3f;
//		yradius = 15f;
		xradius=radius+0.3f;

		//napisz tu normalizacje min i maxboundry;
	}

	// Update is called once per frame
	void Update ()
	{

		centralLoc = pivot.transform.position;

		if (gameObject.transform.position.x > centralLoc.x + radius) {
			backwards = true;
		} else if (gameObject.transform.position.x < centralLoc.x - radius ) {
			backwards = false;
		}






		if (backwards)
			tempMovement = -movement;
		else
			tempMovement = movement;
		gameObject.transform.position += tempMovement / 100;
		cloudLoc = gameObject.transform.position;
		if (gameObject.transform.position.x > centralLoc.x + xradius) 
		{
			cloudLoc.x -= 2*xradius;
		} else if (gameObject.transform.position.x < centralLoc.x - xradius ) 
		{
			cloudLoc.x += 2*xradius;
		}


		if (gameObject.transform.position.y > centralLoc.y + yradius) 
		{
			cloudLoc.y -= (2*yradius);
		} else if (gameObject.transform.position.y < centralLoc.y - yradius ) 
		{
			cloudLoc.y += (2*yradius);
		}
		gameObject.transform.position = cloudLoc;
	}
}
