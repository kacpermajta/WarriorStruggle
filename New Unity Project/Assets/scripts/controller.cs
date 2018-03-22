using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class controller : MonoBehaviour {
	public static bool moveUp;
	public static bool moveLeft;
	public static bool moveRight;
	public static bool Strike;
	public static bool Skill;
	public static bool Interact;
	public static bool alive;

	public static float cameraPlane;




	// Use this for initialization
	void Start () {
		alive = true;
		
	}
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SceneManager.LoadScene("menu");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.W))
			moveUp=true;
		else
			moveUp=false;
		
		if (Input.GetKey(KeyCode.A))
			moveLeft=true;
		else
			moveLeft=false;
		
		if (Input.GetKey(KeyCode.D))
			moveRight=true;
		else
			moveRight=false;
		
		if (Input.GetKey(KeyCode.F))
			Interact=true;
		else
			Interact=false;
		
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




		
	}




}
