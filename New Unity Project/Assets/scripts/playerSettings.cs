using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerSettings : MonoBehaviour {
	
	public static GameObject playerHero;
	public GameObject defaultHero;
	public static int difficulty;
	public static bool classicCrl;


	void Awake() {

	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		classicCrl = false;
		//choose defauld class
		playerHero = defaultHero;
		difficulty = 5;

		SceneManager.LoadScene("menu");


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
