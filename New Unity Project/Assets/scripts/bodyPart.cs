using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script handles behaviour of single beast body part
public class bodyPart : MonoBehaviour {
	public enum bodyPartType {none, follow, uppercut, crush, propel, wave};
	public bodyPartType partType;
	public Vector3 originPoint,location;
	public float partTilt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		location= gameObject.GetComponent<Transform>().position;

		//change position of body when beast is going through slopes
		if (partType == bodyPartType.follow) {
			partTilt = gameObject.transform.parent.GetComponent<BeastController> ().tilt;
			transform.RotateAround (location+originPoint, new Vector3(0f,0f,1f),-partTilt);

		}
	}
}
