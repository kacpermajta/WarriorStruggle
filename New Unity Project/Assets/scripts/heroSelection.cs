using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heroSelection : MonoBehaviour {
	public GameObject[] character;


	public void mainMenu () {
		SceneManager.LoadScene("menu");

	}

	public void hero1 () {
		playerSettings.heroNum = 1;
		playerSettings.playerHero=character[1];
		SceneManager.LoadScene("menu");

	}
	public void hero2 () {
		playerSettings.heroNum = 2;
		playerSettings.playerHero=character[2];
		SceneManager.LoadScene("menu");

	}

	public void hero3 () {
		playerSettings.heroNum = 3;
		playerSettings.playerHero=character[3];
		SceneManager.LoadScene("menu");

	}

	public void hero4 () {
		playerSettings.heroNum = 4;
		playerSettings.playerHero=character[4];
		SceneManager.LoadScene("menu");

	}

	public void hero5 () {
		playerSettings.heroNum = 5;
		playerSettings.playerHero=character[5];
		SceneManager.LoadScene("menu");

	}

	public void hero6 () {
		playerSettings.heroNum = 6;
		playerSettings.playerHero=character[6];
		SceneManager.LoadScene("menu");

	}

	public void hero7 () {
		playerSettings.heroNum = 7;
		playerSettings.playerHero=character[7];
		SceneManager.LoadScene("menu");

	}

	public void hero8 () {
		playerSettings.heroNum = 8;
		playerSettings.playerHero=character[8];
		SceneManager.LoadScene("menu");

	}

	public void hero9 () {
		playerSettings.heroNum = 9;
		playerSettings.playerHero=character[9];
		SceneManager.LoadScene("menu");

	}

	public void hero10 () {
		playerSettings.heroNum = 10;
		playerSettings.playerHero=character[10];
		SceneManager.LoadScene("menu");

	}

	public void hero11 () {
		playerSettings.heroNum = 11;
		playerSettings.playerHero=character[11];
		SceneManager.LoadScene("menu");

	}
	public void hero12 () {
		playerSettings.heroNum = 12;
		playerSettings.playerHero=character[12];
		SceneManager.LoadScene("menu");

	}
	public void hero13 () {
		playerSettings.heroNum = 13;
		playerSettings.playerHero=character[13];
		SceneManager.LoadScene("menu");

	}

	public void hero14 () {
		playerSettings.heroNum = 14;
		playerSettings.playerHero=character[14];
		SceneManager.LoadScene("menu");

	}
	public void hero15 () {
		playerSettings.heroNum = 15;
		playerSettings.playerHero=character[15];
		SceneManager.LoadScene("menu");

	}


	public void hero16 () {
		playerSettings.heroNum = 16;
		playerSettings.playerHero=character[16];
		SceneManager.LoadScene("menu");

	}

	public void hero17 () {
		playerSettings.heroNum = 17;
		playerSettings.playerHero=character[17];
		SceneManager.LoadScene("menu");

	}

	public void hero18 () {
		playerSettings.heroNum = 18;
		playerSettings.playerHero=character[18];
		SceneManager.LoadScene("menu");

	}

	public void hero19 () {
		playerSettings.heroNum = 19;
		playerSettings.playerHero=character[19];
		SceneManager.LoadScene("menu");

	}

	public void hero20 () {
		playerSettings.heroNum = 20;
		playerSettings.playerHero=character[20];
		SceneManager.LoadScene("menu");

	}

	public void hero21 () {
		playerSettings.heroNum = 21;
		playerSettings.playerHero=character[21];
		SceneManager.LoadScene("menu");

	}
	public void hero22 () {
		playerSettings.heroNum = 22;
		playerSettings.playerHero=character[22];
		SceneManager.LoadScene("menu");

	}

	public void hero23 () {
		playerSettings.heroNum = 23;
		playerSettings.playerHero=character[23];
		SceneManager.LoadScene("menu");

	}



	// Use this for initialization
	void Start () {
		playerSettings.character = character;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
