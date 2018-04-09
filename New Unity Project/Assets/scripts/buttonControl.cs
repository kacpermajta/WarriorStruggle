using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Net;

public class buttonControl : MonoBehaviour {

	void Start () {
		if(GameObject.Find("jumpButt")!=null)
			GameObject.Find("jumpButt").GetComponentInChildren<Text>().text = "Use classic jump";
		playerSettings.isClient = false;
		playerSettings.isServer = false;
		if (GameObject.Find ("IPBOX") != null) {
			Debug.Log ("paczan");
			CheckIP ();

		}
//		if(GameObject.Find("IPBOX")!=null)
//
//			GameObject.Find("IPBOX").GetComponentInChildren<Text>().text = Network.player.externalIP;
	}


	// Use this for initialization
	public void Tutorial () {
		SceneManager.LoadScene("tutorial");

	}
	public void StartGame () {
		SceneManager.LoadScene("levels");

	}
	public void mainMenu () {
		SceneManager.LoadScene("menu");

	}
	public void heroes () {
		SceneManager.LoadScene("heroes");

	}
	public void controls () {
		SceneManager.LoadScene("controls");

	}
	public void zyczenia () {
		SceneManager.LoadScene("zyczenia");

	}

	public void levelOne () {
		SceneManager.LoadScene("sceneTwo");

	}
	public void levelTwo () {
		SceneManager.LoadScene("sceneThree");

	}
	public void levelThree () {
		SceneManager.LoadScene("sceneOne");

	}
	public void levelFour() {
		SceneManager.LoadScene("sceneFour");

	}
	public void levelFive() {
		SceneManager.LoadScene("sceneFive");

	}
	public void levelSix() {
		SceneManager.LoadScene("sceneSix");

	}
	public void cheatmap() {
		SceneManager.LoadScene("cheatmap");

	}
	public void levelSeven() {
		SceneManager.LoadScene("sceneSeven");

	}
	public void levelEight() {
		SceneManager.LoadScene("sceneEight");

	}
	public void levelNine() {
		SceneManager.LoadScene("sceneNein");

	}

	public void levelTwov2 () {
		SceneManager.LoadScene("sceneTwov2");

	}
	public void levelThreev2 () {
		SceneManager.LoadScene("sceneThreev2");

	}
	public void levelDeathroom () {
		SceneManager.LoadScene("deathroom");

	}
	public void menuMultiplayer () {
		SceneManager.LoadScene("multiplayer");

	}
	public void multiServer () {
		playerSettings.playerName = GameObject.Find ("NameInput").GetComponent<InputField> ().text;
//		Debug.Log (playerSettings.playerName);
		playerSettings.isServer = true;
		SceneManager.LoadScene("multiServer");

	}	
	public void multiClient () {
		
		playerSettings.playerName = GameObject.Find ("NameInput").GetComponent<InputField> ().text;
		playerSettings.serverIP = GameObject.Find ("IPInput").GetComponent<InputField> ().text;
//		Debug.Log (playerSettings.playerName);

		playerSettings.isClient = true;

		SceneManager.LoadScene("multiClient");

	}
	public void setStandCrl () {
		playerSettings.classicCrl = false;
		SceneManager.LoadScene("menu");

	}

	public void setClassCrl () {
		playerSettings.classicCrl = true;
		SceneManager.LoadScene("menu");

	}

	public void setJumpMode () {
		if (playerSettings.classicJump) {
			GameObject.Find("jumpButt").GetComponentInChildren<Text>().text = "Use classic jump";
			playerSettings.classicJump = false;
		}
		else
		{
			GameObject.Find("jumpButt").GetComponentInChildren<Text>().text = "Use standard jump";
			playerSettings.classicJump = true;
		}


	}
	void CheckIP()
	{

//		Debug.Log ("pyklo?");
//
//			using(WWW myExtIPWWW = new WWW ("http://checkip.dyndns.org"))
//			{
////			if (myExtIPWWW == null) 
////			{
////				Debug.Log ("niepyklo");
////				yield break;
////			}
		//			yield return myExtIPWWW;	//https://api.ipify.org
//			string myExtIP = myExtIPWWW.data;
////			myExtIP = myExtIP.Substring (myExtIP.IndexOf (":") + 1);
////			myExtIP = myExtIP.Substring (0, myExtIP.IndexOf ("<"));
//			Debug.Log ("ejno");
			WebClient webClient = new WebClient();
			string publicIp = webClient.DownloadString("http://checkip.dyndns.org");
			publicIp = publicIp.Substring (publicIp.IndexOf (":") + 1);
			publicIp = publicIp.Substring (0, publicIp.IndexOf ("<"));
			GameObject.Find ("IPBOX").GetComponentInChildren<Text> ().text = publicIp;
//			}

		// print(myExtIP);
	}




	// Update is called once per frame
	public void ExitGame () {
		Application.Quit();
	}
}
