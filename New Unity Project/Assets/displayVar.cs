using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class displayVar : MonoBehaviour {
	GameObject  textDisplayer;
	Text textContent;
	// Use this for initialization
	void Start () {
	//	textDisplayer = GameObject.Find("MyText"); 
		textContent = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		textContent.text = playerSettings.difficulty.ToString();
	}

	public void increaseDiff(){
		if (playerSettings.difficulty < 8){
			playerSettings.difficulty++;
		}
	}
	public void decreaseDiff(){
		if (playerSettings.difficulty > 1){
			playerSettings.difficulty--;
		}
	}
}
