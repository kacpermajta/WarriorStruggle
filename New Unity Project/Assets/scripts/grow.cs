using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour {
	public Vector3 targetScale;
	Vector3 storedScale;
	public float expanse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		storedScale = gameObject.transform.localScale;
		if (storedScale.x < targetScale.x)
			storedScale.x += expanse;
		if (storedScale.y < targetScale.y)
			storedScale.y += expanse;
		if (storedScale.z < targetScale.z)
			storedScale.z += expanse;
		if (storedScale == gameObject.transform.localScale)
			Destroy (gameObject.GetComponent <grow> ());
		else
			gameObject.transform.localScale = storedScale;


	}
}
