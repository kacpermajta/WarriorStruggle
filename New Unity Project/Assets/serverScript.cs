using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using UnityEngine.Networking;
public class ServerClient
{
	public int connectionId;
	public string playerName;

	public ServerAgent agent;
}
public class ServerAgent
{
	public GameObject avatar;
	public float storedX, storedY, sAimX, sAimY;
	public int heroNum, headNum, bodyNum;
}

public class serverScript : MonoBehaviour {
	private const int maxPlayers=10;
	private int port= 5701;//5701
	private int hostID;
	private int reliableChannel;
	private int unreliableChannel;
	private bool isStarted;
	private byte error;
	public GameObject playerPrefab;
	float updateInterval=0.05f;

	public List<GameObject> characters = new List<GameObject>();
	public List<GameObject> missiles = new List<GameObject>();
	public Dictionary<int, ServerClient> clients = new Dictionary<int, ServerClient> ();
	// Use this for initialization
	void Start () {
		NetworkTransport.Init();
		ConnectionConfig myConfig = new ConnectionConfig ();
		reliableChannel = myConfig.AddChannel (QosType.Reliable);
		unreliableChannel = myConfig.AddChannel (QosType.Unreliable);
		HostTopology myTopology = new HostTopology (myConfig, maxPlayers);
		hostID = NetworkTransport.AddHost (myTopology, port, null);
		isStarted = true;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!isStarted)
			return;
		int recHostId; 
		int connectionId; 
		int channelId; 
		byte[] recBuffer = new byte[1024]; 
		int bufferSize = 1024;
		int dataSize;
		byte error;

		foreach (KeyValuePair<int, ServerClient> sc in clients)
			if(sc.Value.agent.avatar!=null)
				distributeState(sc.Value.agent , sc.Key);

		NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
		switch (recData)
		{
		case NetworkEventType.Nothing:         //1
			break;
		case NetworkEventType.ConnectEvent:    //2
			//Debug.Log ("player " + connectionId + " connected");
			OnConnection(connectionId);
			break;
		case NetworkEventType.DataEvent:       //3
		//	string mltMess= Encoding.

			string msg = Encoding.Unicode.GetString (recBuffer, 0, dataSize);
			//Debug.Log ("Receiving from: "+ connectionId+ " : " + msg);
			string[] splitData=msg.Split('|');
			switch(splitData[0])
			{
			case "NAMEIS":
				Debug.Log ("helo word: "+msg);
				OnNameIs(connectionId, splitData[1],splitData[2],splitData[3],splitData[4]);
				break;
			case "CONT":
				ControlPlayer(connectionId, splitData[1]);

				break;
			case "AIM":
				SetAimPlayer(connectionId, splitData[1]);

				break;
//			case "DC":
//				
//				break;

			default:
				//Debug.Log("invalid: " +msg);
				break;

			}


			//Debug.Log("player "+ connectionId + " send"+ msg);

			break;
		case NetworkEventType.DisconnectEvent: //4
			//Debug.Log ("player " + connectionId + " disconnected");
			OnDiconnection (connectionId);
			break;
		}
	}

	private void OnConnection(int cnnId)
	{
		//setup new client
		ServerClient c = new ServerClient ();
		c.agent= new ServerAgent();
		c.connectionId = cnnId;
		c.playerName = "TEMP";

		clients.Add (cnnId, c);
		string msg = "ASKNAME|" + cnnId + "|";


//		foreach(KeyValuePair<string, string> entry in myDictionary)
//		{
//			// do something with entry.Value or entry.Key
//		}
		foreach (KeyValuePair<int, ServerClient> sc in clients)
			msg += sc.Value.playerName + "%" + sc.Value.connectionId+ "%" + sc.Value.agent.heroNum+ "%"
				+ sc.Value.agent.headNum+"%"+ sc.Value.agent.bodyNum  + "|";
		
		msg = msg.Trim ('|');
		Send (msg, reliableChannel, cnnId);

	}



	private void OnDiconnection(int cnnIDD)
	{
		Debug.Log (cnnIDD);
		foreach (KeyValuePair<int, ServerClient> sc in clients) 
		{
			Debug.Log (sc.Value.playerName + ", " + sc.Key);
		}

		Destroy (clients [cnnIDD].agent.avatar);

		clients.Remove (cnnIDD);
		Debug.Log ("one less");
		foreach (KeyValuePair<int, ServerClient> sc in clients) 
		{
			Debug.Log (sc.Value.playerName + ", " + sc.Key);
		}


		Send ("DC|" + cnnIDD, reliableChannel, clients);
	}
	private void OnNameIs(int cnnID, string playerName,  string heroName,  string headName,  string bodyName)
	{
		Debug.Log ("dostajeimie");
		clients [cnnID].playerName = playerName;
		clients [cnnID].agent.heroNum = int.Parse (heroName);
		clients [cnnID].agent.headNum = int.Parse (headName);
		clients [cnnID].agent.bodyNum = int.Parse (bodyName);

		playerPrefab = playerSettings.character[clients [cnnID].agent.heroNum];//clients [cnnID].agent.heroNum];
		GameObject go = Instantiate (playerPrefab)  as GameObject;
		Destroy (go.transform.Find ("camera").gameObject);
		Debug.Log ("powinno");
		clients [cnnID].agent.avatar = go;
		clients [cnnID].agent.avatar.GetComponent<character_behavior> ().nameTag = playerName;


		Send ("CNN|" + playerName + "|" + cnnID+ "|" + clients [cnnID].agent.heroNum+ "|" 
			+ clients [cnnID].agent.headNum+ "|" + clients [cnnID].agent.bodyNum , reliableChannel, clients);

	}
	private void Send (string message, int channelID, int cnnID)
	{
		Dictionary<int, ServerClient> c = new Dictionary<int, ServerClient> ();
		c.Add (cnnID, clients[cnnID]);
//		Debug.Log(
		//c.Add (clients [cnnID]);
		Send (message, channelID, c);

	}
	private void Send (string message, int channelID, Dictionary<int,ServerClient> ce)
	{
		Debug.Log ("sending: " + message);
		byte[] msg = Encoding.Unicode.GetBytes (message);
		foreach (KeyValuePair<int, ServerClient> sc in ce)
			
		//foreach (ServerClient sc in c) 
		{
			NetworkTransport.Send(hostID,sc.Value.connectionId,channelID,msg,message.Length*sizeof(char), out error);


		}
		foreach (KeyValuePair<int, ServerClient> sc in clients) 
		{
			Debug.Log ("serial: "+sc.Value.playerName + ", " + sc.Key);
		}
	}
	private void ControlPlayer(int cnnID, string input)
	{
//		charUp= 1;
//		charRight = 2;
//		charLeft= 4;
//		charStrike = 8;
//		charSkill = 16;
		int numInp= int.Parse(input);
		clients [cnnID].agent.avatar.GetComponent<character_behavior> ().ResetControl ();
		if (numInp >= 16) 
		{
			clients [cnnID].agent.avatar.GetComponent<character_behavior> ().charSkill = true;
			numInp -= 16;
		}
		if (numInp >= 8) 
		{
			clients [cnnID].agent.avatar.GetComponent<character_behavior> ().charStrike = true;
			numInp -= 8;
		}
		if (numInp >= 4) 
		{
			clients [cnnID].agent.avatar.GetComponent<character_behavior> ().charLeft = true;
			numInp -= 4;
		}
		if (numInp >= 2) 
		{
			clients [cnnID].agent.avatar.GetComponent<character_behavior> ().charRight = true;
			numInp -= 2;
		}
		if (numInp >= 1) 
		{
			clients [cnnID].agent.avatar.GetComponent<character_behavior> ().charUp = true;
			numInp -= 1;
		}
	}

	private void SetAimPlayer(int cnnID, string input)
	{
		//		charUp= 1;
		//		charRight = 2;
		//		charLeft= 4;
		//		charStrike = 8;
		//		charSkill = 16;

		string[] newAim=input.Split('%');
		float newX= float.Parse(newAim[0]);
		float newY= float.Parse(newAim[1]);
		clients [cnnID].agent.avatar.GetComponent<character_behavior> ().aim.x = newX;
		clients [cnnID].agent.avatar.GetComponent<character_behavior> ().aim.y = newY;

	}
	private void distributeState(ServerAgent agent, int cnnID)
	{
		if (Mathf.Abs (agent.storedX - agent.avatar.GetComponent<character_behavior> ().lokacja.x) > updateInterval || Mathf.Abs (agent.storedY - agent.avatar.GetComponent<character_behavior> ().lokacja.y) > updateInterval) 
		{
			agent.storedX = agent.avatar.GetComponent<character_behavior> ().lokacja.x;
			agent.storedY = agent.avatar.GetComponent<character_behavior> ().lokacja.y;

			Send("LOC|"+ cnnID+"|"  + agent.storedX.ToString("F2")+ "%"+agent.storedY.ToString("F2"),unreliableChannel,clients );
		}

	}


}
