using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player

{
	public int connectionId;
	public GameObject avatar;
	public string playerName;
}
public class clientScript : MonoBehaviour {

	private const int maxPlayers=10;
	private int port= 5701;
	private int hostID;
	private int ourClientID;
	private int reliableChannel;
	private int unreliableChannel;
	private bool storedUp, storedRight, storedLeft, storedStrike, storedSkill;
	private float storedX, storedY;


	float updateInterval=0.1f;


	private int connectionID;

	private float connectionTime;
	private bool isConnected=false;
	private bool isStarted=false;
	private byte error;

	public GameObject playerPrefab;
	public Dictionary<int, Player> players=new Dictionary<int, Player>();
	public List<GameObject> characters = new List<GameObject>();
	public List<GameObject> missiles = new List<GameObject>();

	public void Connect()
	{
		if (playerSettings.playerName == "")
		{
			playerSettings.playerName = "Warrior";
		}
		NetworkTransport.Init();
		ConnectionConfig myConfig = new ConnectionConfig ();
		reliableChannel = myConfig.AddChannel (QosType.Reliable);
		unreliableChannel = myConfig.AddChannel (QosType.Unreliable);
		HostTopology myTopology = new HostTopology (myConfig, maxPlayers);
		hostID = NetworkTransport.AddHost (myTopology, 0);
		if(playerSettings.serverIP=="")
			playerSettings.serverIP="127.0.0.1";
		connectionID = NetworkTransport.Connect(hostID,playerSettings.serverIP,port, 0, out error);//127.0.0.1

		isConnected = true;
		connectionTime = Time.time;
		Debug.Log (error + "; " + (NetworkError)error);
		if ((NetworkError)error == NetworkError.WrongChannel) 
		{
			SceneManager.LoadScene("multiplayer");
		}

	}


	// Use this for initialization
	void Start () {
		int [] aviable=new int[]{2,3, 4,5,6,7,8,9, 10, 11,12, 13,14, 15,17,18,19, 20,22,23};
		bool nope = true;
		foreach (int number in aviable)
			if (playerSettings.heroNum == number)
				nope = false;
		if (nope)
			playerSettings.heroNum = 0;
		Connect ();

	}
	
	// Update is called once per frame
	void Update()
	{
		if (!isConnected)
			return;
		int recHostId; 
		int connectionId; 
		int channelId; 
		byte[] recBuffer = new byte[1024]; 
		int bufferSize = 1024;
		int dataSize;
		byte error;

		if (controller.alive&&(Camera.main != null&&(Mathf.Abs (storedX - Camera.main.ScreenToWorldPoint (Input.mousePosition).x) > updateInterval || Mathf.Abs (storedY - Camera.main.ScreenToWorldPoint (Input.mousePosition).y) > updateInterval) ))
		{
			
			SendAim ();
		}


		if (controller.alive&&(  storedUp != controller.moveUp || storedRight != controller.moveRight || storedLeft != controller.moveLeft ||
			storedStrike != controller.Strike || storedSkill != controller.Skill))
		{
			SendControls ();
			storedUp = controller.moveUp;
			storedRight = controller.moveRight;
			storedLeft = controller.moveLeft ;

			storedStrike = controller.Strike ;
			storedSkill = controller.Skill;
		}
		NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
		switch (recData)
		{
		case NetworkEventType.Nothing:         //1
			break;
		case NetworkEventType.DisconnectEvent: //4
			//Debug.Log ("player " + connectionId + " disconnected");

			SceneManager.LoadScene("multiplayer");
			break;
		
		case NetworkEventType.DataEvent:       //3
			string msg = Encoding.Unicode.GetString (recBuffer, 0, dataSize);
			Debug.Log ("Receiving: " + msg);
			string[] splitData=msg.Split('|');
			switch(splitData[0])
			{
			case "ASKNAME":
				OnAskName(splitData);
				break;
			case "CNN":
				SpawnPlayer(splitData[1],int.Parse(splitData[2]),int.Parse(splitData[3]),int.Parse(splitData[4])
					,int.Parse(splitData[5]));
				break;
			case "DC":
				PlayerDisconnected (int.Parse (splitData [1]));
				break;
			case "LOC":
				//Debug.Log (int.Parse (splitData [1]));
				UpdateAgentLocation(splitData);
				break;
			case "HP":
				//Debug.Log (int.Parse (splitData [1]));
				SetHealth(int.Parse(splitData[1]),float.Parse(splitData[2]));
				break;
			case "MI":
				//Debug.Log (int.Parse (splitData [1]));
				SpawnMissile(int.Parse(splitData[1]),float.Parse(splitData[2]),float.Parse(splitData[3]),float.Parse(splitData[4]),float.Parse(splitData[5]),float.Parse(splitData[6]));
				break;
			case "ATT":
				//Debug.Log (int.Parse (splitData [1]));
				SetAttack(int.Parse (splitData [1]),int.Parse (splitData [2]));
				break;
//			case "AIC":
//				//Debug.Log (int.Parse (splitData [1]));
//				UpdateAgentAim(int.Parse (splitData [1]),splitData [2]);
//				break;
			case "KILL":
				//Debug.Log (int.Parse (splitData [1]));
				if (int.Parse (splitData [1]) == ourClientID) 
				{
					controller.alive = false;
					StartCoroutine (DelDisconnect ());
				}
				players[int.Parse (splitData [1])].avatar.GetComponent<character_behavior> ().clientKill();
				break;

			default:
				//Debug.Log("invalid: " +msg);
				break;

			}

			break;

		}
	}

	private void OnAskName(string[] data)
	{
		ourClientID = int.Parse(data [1]);

		//sendname
		Send("NAMEIS|" + playerSettings.playerName+"|"+playerSettings.heroNum+"|"+playerSettings.headNum+
			"|"+playerSettings.bodyNum, reliableChannel);
		//spawn other players
		for (int i = 2; i < data.Length ; i++)
		{
			string[] d = data [i].Split ('%');
			if (int.Parse (d [1]) != ourClientID) 
			{
				SpawnPlayer (d [0], int.Parse (d [1]), int.Parse (d [2]), int.Parse (d [3]), int.Parse (d [4]));
			}
		}

	}
	private void SpawnPlayer(string playername, int cnnId, int heroNum, int headNum, int bodyNum)
	{
		Debug.Log ("i tried"+heroNum);
		//playerPrefab = playerSettings.character [heroNum];
		playerPrefab = playerSettings.character[heroNum];

		GameObject go = Instantiate (playerPrefab)  as GameObject;
		if (cnnId == ourClientID) {
		//	GameObject.Find ("Canvas").SetActive (false);
			isStarted = true;
			Debug.Log ("look it me "+cnnId);
		} else {
			
			Destroy (go.transform.Find ("camera").gameObject);
			go.GetComponent<character_behavior> ().isPlayer = false;
		}
		go.transform.Find ("head").GetComponent<MeshFilter> ().mesh = playerSettings.headSkins [headNum];
		go.transform.Find ("body").GetComponent<MeshFilter> ().mesh = playerSettings.bodySkins [bodyNum];

		Player p = new Player ();

		p.avatar = go;
		p.playerName = playername;
		p.connectionId = cnnId;
		players.Add (cnnId,p);

		p.avatar.GetComponent<character_behavior> ().nameTag = playername;
	}
	private void PlayerDisconnected(int cnnID)
	{
		Destroy (players [cnnID].avatar);
		players.Remove (cnnID);
	}
	private void UpdateAgentLocation(string[] data)
	{
		for (int i = 1; i < data.Length ; i++)
		{
			string[] locVec = data[i].Split ('%');
			if (players.ContainsKey (int.Parse (locVec [0]))) 
			{
				
				players [int.Parse (locVec [0])].avatar.transform.position = new Vector3 (float.Parse (locVec [1]), float.Parse (locVec [2]), players [int.Parse (locVec [0])].avatar.GetComponent<character_behavior> ().mapPlane);
				players [int.Parse (locVec [0])].avatar.GetComponent<character_behavior> ().aim = new Vector3(float.Parse(locVec[3]), float.Parse(locVec[4]), players [int.Parse (locVec [0])].avatar.GetComponent<character_behavior> ().mapPlane);

			}
//			string[] d = data [i].Split ('%');
//			if (int.Parse (d [1]) != ourClientID) 
//			{
//				SpawnPlayer (d [0], int.Parse (d [1]), int.Parse (d [2]), int.Parse (d [3]), int.Parse (d [4]));
//			}
		}



//		if (players.ContainsKey(cnnID))
//		{
//		string[] locVec = location.Split ('%');
//		players [cnnID].avatar.transform.position = new Vector3(float.Parse(locVec[0]), float.Parse(locVec[1]), players [cnnID].avatar.GetComponent<character_behavior> ().mapPlane);
//	
//
//		}
	}

	private void UpdateAgentAim(int cnnID,string aim)
	{
		if (players.ContainsKey(cnnID))
		{
			string[] locVec = aim.Split ('%');
			players [cnnID].avatar.GetComponent<character_behavior> ().aim = new Vector3(float.Parse(locVec[0]), float.Parse(locVec[1]), players [cnnID].avatar.GetComponent<character_behavior> ().mapPlane);


		}
	}

	private void Send (string message, int channelID)
	{
		Debug.Log ("sending: " + message);
		byte[] msg = Encoding.Unicode.GetBytes (message);

		NetworkTransport.Send(hostID,connectionID,channelID,msg,message.Length*sizeof(char), out error);



	}
	private void SendControls()
	{
		int numOut=0;
		if (controller.moveUp) 
		{
			numOut += 1;
		}
		if (controller.moveRight) 
		{
			numOut += 2;
		}
		if (controller.moveLeft) 
		{
			numOut += 4;
		}
		if (controller.Strike) 
		{
			numOut += 8;
		}
		if (controller.Skill) 
		{
			numOut += 16;
		}
		Send("CONT|"+ numOut, unreliableChannel);

	}
	private void SendAim()
	{
		storedX = Camera.main.ScreenToWorldPoint (Input.mousePosition).x;
		storedY = Camera.main.ScreenToWorldPoint (Input.mousePosition).y;

		Send("AIM|"+ storedX.ToString("F2")+ "%"+storedY.ToString("F2"),unreliableChannel );

	}

	IEnumerator DelDisconnect()
	{

		yield return new WaitForSeconds(2f);
		Network.Disconnect ();
		//NetworkTransport.Disconnect (hostID,0, out error);
		SceneManager.LoadScene("multiClient");
		//	controller.message = 2;
		//controller.changeMessage = true;

	}
	private void SetHealth(int cnnID, float hp)
	{
		players [cnnID].avatar.GetComponent<character_behavior> ().health = hp;

	}
	private void SetAttack(int cnnID, int numInp)
	{
		if (numInp >= 2) 
		{
			players [cnnID].avatar.GetComponent<character_behavior> ().charSkill = true;
			numInp -= 2;
		}
		else
		{
			players [cnnID].avatar.GetComponent<character_behavior> ().charSkill = false;
		}
		if (numInp >= 1) 
		{
			players [cnnID].avatar.GetComponent<character_behavior> ().charStrike = true;

		}
		else
		{
			players [cnnID].avatar.GetComponent<character_behavior> ().charStrike = false;
		}

	}
	public void SpawnMissile(int numPref, float x, float y,float z, float rotZ,float rotY)
	{

		GameObject missile = GameObject.Instantiate (playerSettings.missiles[numPref], new Vector3 (x, y, z ), Quaternion.Euler(0f, rotY,rotZ));
		if(missile.GetComponent<shot> ()!=null)
			missile.GetComponent<shot> ().airborne = true;
	}


}
