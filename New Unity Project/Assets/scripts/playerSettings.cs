using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerSettings : MonoBehaviour {
	public static int headcount=15;
	public static int bodycount=8;
		
	public static GameObject playerHero;
	public GameObject defaultHero;
	public static GameObject[] staticBackscreen;
	public GameObject[] backscreen;

	public static GameObject staticSingleSound;
	public GameObject singleSound;

	public static GameObject[] missiles=new GameObject[14];
	public static Mesh[] headSkins=new Mesh[headcount];
	public static Mesh[] bodySkins=new Mesh[bodycount];
	public static GameObject[] character=new GameObject[24];
//	public GameObject[] characterList;

	public static int difficulty, heroNum, headNum, bodyNum;
	public static bool classicCrl, classicJump;
	public static bool isClient, isServer;
	public static string playerName;

	public static string serverIP, serverName;

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
		heroNum = 1;
		headNum = 4;
		bodyNum = 3;
		staticBackscreen = backscreen;
		staticSingleSound = singleSound;

		SceneManager.LoadScene("menu");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
