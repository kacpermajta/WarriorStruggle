using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using UnityEngine.Networking;

public class ServerClient : MonoBehaviour
{
    public int connectionId;
    public string playerName;

    public ServerAgent agent;
}
public class ServerAgent : MonoBehaviour
{
    public GameObject avatar;
    public float storedX, storedY, sAimX, sAimY;
    public int heroNum, headNum, bodyNum;
}