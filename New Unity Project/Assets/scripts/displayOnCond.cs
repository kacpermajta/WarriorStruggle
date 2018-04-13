using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayOnCond : MonoBehaviour {
	Color tmpCol;
	public bool cond;
	public Sprite[] tmpSprite;


	// Use this for initialization
	void Start () {
		
	}



	// Update is called once per frame
	void Update () {

		if (controller.changeMessage) {
		
			gameObject.GetComponent< Image > ().sprite=tmpSprite[controller.message];


			controller.changeMessage = false;
		}


		//display when player died
		if (!controller.alive) {

			tmpCol = gameObject.GetComponent< Image > ().color;
			tmpCol.a += 0.01f;
			gameObject.GetComponent< Image > ().color = tmpCol;
			if (controller.message == 0) {
				StartCoroutine (DelayedInfo ());
				controller.message = 2;
			}


		} 
		else 
		{
			tmpCol = gameObject.GetComponent< Image > ().color;
			tmpCol.a = 0.0f;
			gameObject.GetComponent< Image > ().color = tmpCol;
		}
	}

	public void changePic(int number)
	{
		cond = !cond;

	}
		IEnumerator DelayedInfo()
		{

			yield return new WaitForSeconds(3f);
		//	controller.message = 2;
			controller.changeMessage = true;

		}

}
