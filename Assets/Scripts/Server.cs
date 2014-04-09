using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	// Use this for initialization
	HostData[] hostData;
	bool waiting=false;
	bool connecting=false;
	bool enteringNameCreate=false;
	bool enteringNameConnect=false;
	string name="";
	bool isFailed=false;
	int index;
	void OnGUI()
	{
		if(isFailed) GUI.Label(new Rect(200,160,200,30),"Connection error!!!");
		if(!enteringNameCreate&&!enteringNameConnect)
		{
			if(GUI.Button(new Rect(10,240,100,100),"Single Player"))
			{
				Application.LoadLevel(1);
			}
		}
		if(enteringNameCreate&&!waiting)
		{
			GUI.Label(new Rect(200,160,200,30),"Enter your name");
			name = GUI.TextField (new Rect(200,200,100,30), name, 25);
			if(GUI.Button(new Rect(200,240,100,100),"Create"))
			{
				Network.InitializeServer(32, 25000, !Network.HavePublicAddress());
				MasterServer.RegisterHost("M", name, "l33t game for all");
				waiting=true;
			}
		}
		if(enteringNameConnect&&!connecting)
		{
			GUI.Label(new Rect(200,160,200,30),"Enter your friend's name");
			name = GUI.TextField (new Rect(200,200,100,30), name, 25);
			if(GUI.Button(new Rect(200,240,100,100),"Connect"))
			{
				connecting=true; 	
				StartCoroutine(Connection ());
			}
		}
		if(waiting&&!isFailed) GUI.Label(new Rect(150,150,100,200),"Waiting for player");
		if(!waiting&&!connecting&&!enteringNameCreate)
		{
			if(GUI.Button(new Rect(10,10,100,100),"Create"))
			{
				enteringNameCreate=true;

			}
			if(GUI.Button(new Rect(10,120,100,100),"Connect"))
			{
				enteringNameConnect=true;
			}
		}
		if(connecting) GUI.Label(new Rect(150,150,100,200),"Connecting");
	}
	void Update()
	{
		if(Network.connections.Length==1)
		{
			Application.LoadLevel(1);
		}
	}
	void Start()
	{
		//128.68.233.123
		MasterServer.ipAddress = "5.175.138.205";
		MasterServer.port = 23466;
		Network.natFacilitatorIP = "5.175.138.205";
		Network.natFacilitatorPort = 50005;
	}
	IEnumerator Connection()
	{
		MasterServer.RequestHostList("M"); 
		yield return new WaitForSeconds(5);
		hostData = MasterServer.PollHostList();
		Debug.Log(hostData.Length);
		for(int i=0;i<hostData.Length;i++)
		{
			if(hostData[i].gameName==name)
			{
				Network.Connect(hostData[i]);
				break;
			}
		}

	}
	void OnFailedToConnect(NetworkConnectionError error) {
		isFailed=true;
	}

	void OnServerInitialized()
	{
		waiting=true;
	}
	void OnConnectedToServer()
	{
		Application.LoadLevel(1);
	}


	// Update is called once per frame


}
