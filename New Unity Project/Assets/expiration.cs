using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expiration : MonoBehaviour {
	public int countdown;
	float shrink;
	public Vector3 targetScale;
	Vector3 storedScale;

	// Use this for initialization
	void Start () {
		shrink = -0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		countdown--;
		if (countdown < 0) {
			storedScale = gameObject.transform.localScale;
			if (storedScale.x > targetScale.x)
				storedScale.x += shrink;
			if (storedScale.y > targetScale.y)
				storedScale.y += shrink;
			if (storedScale.z > targetScale.z)
				storedScale.z += shrink;
			if (storedScale == gameObject.transform.localScale)
				Destroy (gameObject);
			else
				gameObject.transform.localScale = storedScale;
			

		}

	}
}
