using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heroSelection : MonoBehaviour {
	public GameObject character1;
	public GameObject character2;
	public GameObject character3;
	public GameObject character4;
	public GameObject character5;
	public GameObject character6;
	public GameObject character7;
	public GameObject character8;
	public GameObject character9;
	public GameObject character10;
	public GameObject character11;

	public void mainMenu () {
		SceneManager.LoadScene("menu");

	}

	public void hero1 () {
		playerSettings.playerHero=character1;
		SceneManager.LoadScene("menu");

	}
	public void hero2 () {
		playerSettings.playerHero=character2;
		SceneManager.LoadScene("menu");

	}

	public void hero3 () {
		playerSettings.playerHero=character3;
		SceneManager.LoadScene("menu");

	}

	public void hero4 () {
		playerSettings.playerHero=character4;
		SceneManager.LoadScene("menu");

	}

	public void hero5 () {
		playerSettings.playerHero=character5;
		SceneManager.LoadScene("menu");

	}

	public void hero6 () {
		playerSettings.playerHero=character6;
		SceneManager.LoadScene("menu");

	}

	public void hero7 () {
		playerSettings.playerHero=character7;
		SceneManager.LoadScene("menu");

	}

	public void hero8 () {
		playerSettings.playerHero=character8;
		SceneManager.LoadScene("menu");

	}

	public void hero9 () {
		playerSettings.playerHero=character9;
		SceneManager.LoadScene("menu");

	}

	public void hero10 () {
		playerSettings.playerHero=character10;
		SceneManager.LoadScene("menu");

	}

	public void hero11 () {
		playerSettings.playerHero=character11;
		SceneManager.LoadScene("menu");

	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
