//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonControl : MonoBehaviour {

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
		SceneManager.LoadScene("sceneOne");

	}
	public void levelTwo () {
		SceneManager.LoadScene("sceneTwo");

	}
	public void levelThree () {
		SceneManager.LoadScene("sceneThree");

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

	public void levelTwov2 () {
		SceneManager.LoadScene("sceneTwov2");

	}
	public void levelThreev2 () {
		SceneManager.LoadScene("sceneThreev2");

	}
	public void levelDeathroom () {
		SceneManager.LoadScene("deathroom");

	}


	// Update is called once per frame
	public void ExitGame () {
		Application.Quit();
	}
}
