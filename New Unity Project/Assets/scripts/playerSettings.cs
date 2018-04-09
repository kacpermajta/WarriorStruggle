using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerSettings : MonoBehaviour {
	
	public static GameObject playerHero;
	public  GameObject defaultHero;
	public static GameObject[] headSkins;
	public static GameObject[] bodySkins;
	public static GameObject[] character;
//	public GameObject[] characterList;

	public static int difficulty, heroNum, headNum, bodyNum;
	public static bool classicCrl, classicJump;
	public static bool isClient, isServer;
	public static string playerName;

	public static string serverIP;

	void Awake() {

	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
		classicCrl = false;
		classicJump = false;
		//choose defauld class
		playerHero = defaultHero;
		difficulty = 5;
		playerName = "";
		SceneManager.LoadScene("menu");
		heroNum = 1;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
