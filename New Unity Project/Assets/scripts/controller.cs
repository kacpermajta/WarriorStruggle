using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Networking;

public class controller : MonoBehaviour {
	
	public static bool moveUp;
	public static bool moveLeft;
	public static bool moveRight;
	public static bool Strike;
	public static bool Skill;
	public static bool Interact;
	public static bool alive, changeMessage;
	public static int message;




	public static float cameraPlane;




	// Use this for initialization
	void Start () {
		alive = true;
		message = 0;
	}
	
	// Update is called once per frame
	void Update () {



       
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			SceneManager.LoadScene("menu");
			if (playerSettings.isClient || playerSettings.isServer)
				NetworkTransport.Shutdown ();
			//	Network.Disconnect();
        }

		if (Input.GetKeyDown(KeyCode.Return)&&!playerSettings.isClient)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


		if (playerSettings.classicCrl) {
			if (Input.GetKey(KeyCode.UpArrow))
				moveUp=true;
			else
				moveUp=false;

			if (Input.GetKey (KeyCode.LeftArrow))
				moveLeft = true;
			else
				moveLeft = false;

			if (Input.GetKey (KeyCode.RightArrow))
				moveRight = true;
			else
				moveRight = false;
			




		} else {
			if (Input.GetKey(KeyCode.W))
				moveUp=true;
			else
				moveUp=false;
			
			if (Input.GetKey (KeyCode.A))
				moveLeft = true;
			else
				moveLeft = false;
		
			if (Input.GetKey (KeyCode.D))
				moveRight = true;
			else
				moveRight = false;
			

		}
		if (Input.GetKey (KeyCode.Mouse0)) 
		{
			Strike = true;
			Skill = false;
		} else if (Input.GetKey (KeyCode.Mouse1)) 
		{
			Skill = true;
			Strike = false;
		} else 
		{
			Skill = false;
			Strike = false;
		}
		if (Input.GetKey(KeyCode.F))
			Interact=true;
		else
			Interact=false;
		




		
	}


//	public static void ShowDelayed(){
//
//		StartCoroutine(controller.DelayedInfo ());
//	}
//
//
//	public static IEnumerator DelayedInfo(){
//
//		yield return new WaitForSeconds(3f);
//		controller.message = 2;
//		controller.changeMessage = true;
//	}

}
