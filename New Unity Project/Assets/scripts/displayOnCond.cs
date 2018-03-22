using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayOnCond : MonoBehaviour {
	Color tmpCol;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//display when player died
		if (!controller.alive) 
		{

			tmpCol = gameObject.GetComponent< Image > ().color;
			tmpCol.a += 0.01f;
			gameObject.GetComponent< Image > ().color = tmpCol;

		}
	}
}
