using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerSettings : MonoBehaviour {
	
	public static GameObject playerHero;
	public GameObject defaultHero;

	void Awake() {

	}
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);

		//choose defauld class
		playerHero = defaultHero;

		SceneManager.LoadScene("menu");


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
