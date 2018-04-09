using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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

	}


	// Use this for initialization
	void Start () {
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

		if (Camera.main != null&&(Mathf.Abs (storedX - Camera.main.ScreenToWorldPoint (Input.mousePosition).x) > 0.01f || Mathf.Abs (storedY - Camera.main.ScreenToWorldPoint (Input.mousePosition).y) > 0.01f) )
		{
			
			SendAim ();
		}


		if (storedUp != controller.moveUp || storedRight != controller.moveRight || storedLeft != controller.moveLeft ||
			storedStrike != controller.Strike || storedSkill != controller.Skill)
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
				UpdateAgentLocation(int.Parse (splitData [1]),splitData [2]);
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
		for (int i = 2; i < data.Length - 1; i++)
		{
			string[] d = data [i].Split ('%');
			SpawnPlayer (d [0], int.Parse (d [1]), int.Parse (d [2]), int.Parse (d [3]), int.Parse (d [1]));

		}

	}
	private void SpawnPlayer(string playername, int cnnId, int heroNum, int headNum, int bodyNum)
	{
		//playerPrefab = playerSettings.character [heroNum];
		playerPrefab=playerSettings.character [heroNum];

		GameObject go = Instantiate (playerPrefab)  as GameObject;
		if (cnnId == ourClientID) {
			GameObject.Find ("Canvas").SetActive (false);
			isStarted = true;

		} else {
			
			Destroy (go.transform.Find ("camera").gameObject);
			go.GetComponent<character_behavior> ().isPlayer = false;
		}
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
	private void UpdateAgentLocation(int cnnID,string location)
	{
		if (players.ContainsKey(cnnID))
		{
		string[] locVec = location.Split ('%');
		players [cnnID].avatar.transform.position = new Vector3(float.Parse(locVec[0]), float.Parse(locVec[1]), players [cnnID].avatar.GetComponent<character_behavior> ().mapPlane);
	

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


}
