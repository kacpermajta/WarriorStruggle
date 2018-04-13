using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinModel : MonoBehaviour {
	public bool head;
	// Use this for initialization
	void Start () {
		//GameObject.Find ("head").GetComponent<MeshFilter> ().mesh;
		if(head)
			gameObject.GetComponent<MeshFilter> ().mesh=playerSettings.headSkins[playerSettings.headNum];
		else
			gameObject.GetComponent<MeshFilter> ().mesh=playerSettings.bodySkins[playerSettings.bodyNum];

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void nextHead()
	{
		if (playerSettings.headNum == playerSettings.headcount-1)
			playerSettings.headNum = 0;
		else
			playerSettings.headNum++;
		gameObject.GetComponent<MeshFilter> ().mesh=playerSettings.headSkins[playerSettings.headNum];
		Debug.Log (playerSettings.headNum + "; ");
	}
	public void previousHead()
	{
		if (playerSettings.headNum == 0)
			playerSettings.headNum = playerSettings.headcount-1;
		else
			playerSettings.headNum--;
		gameObject.GetComponent<MeshFilter> ().mesh=playerSettings.headSkins[playerSettings.headNum];

	}
	public void nextBody()
	{
		if (playerSettings.bodyNum == playerSettings.bodycount-1)
			playerSettings.bodyNum = 0;
		else
			playerSettings.bodyNum++;
		gameObject.GetComponent<MeshFilter> ().mesh=playerSettings.bodySkins[playerSettings.bodyNum];
		Debug.Log (playerSettings.bodyNum + "; ");
	}
	public void previousBody()
	{
		if (playerSettings.bodyNum == 0)
			playerSettings.bodyNum = playerSettings.bodycount-1;
		else
			playerSettings.bodyNum--;
		gameObject.GetComponent<MeshFilter> ().mesh=playerSettings.bodySkins[playerSettings.bodyNum];

	}
}
