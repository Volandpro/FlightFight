using UnityEngine;
using System.Collections;

public class ConnectionController : MonoBehaviour {

	MessageController mesCtrl;
	HostData[] hostData;
	string roomName;
	bool isWaitingForHostList = false;

	enum Action {CheckRoomNameUniqueness, ConnectToRoom};
	Action currentAction;

	const int mainLevelIndex = 1;
	//Переменные для мастер сервера и нетворка
	const int playerLimit = 2;
	const string gameType = "OnlineWarplanesGame";
	const string comment = "MaxImbaComment";
	const int listenPort = 25000;
	

	public static ConnectionController Get()
	{
		return GameObject.Find ("Anchor").GetComponent<ConnectionController>();
	}

	void Start () 
	{
		mesCtrl = MessageController.Get ();
		
		MasterServer.ipAddress = "5.175.138.205";
		MasterServer.port = 23466;
		Network.natFacilitatorIP = "5.175.138.205";
		Network.natFacilitatorPort = 50005;
		
		UnregisterRoom();
	}

	//методы перехода на главную сцену
	void Update () 
	{
		if(Network.connections.Length==1)
		{
			Application.LoadLevel(mainLevelIndex);
		}
	}

	void OnConnectedToServer()
	{
		Application.LoadLevel(mainLevelIndex);
	}


	//регистрация и подключение к комнатам
	public void RegisterRoom(string roomName)
	{
		this.roomName = roomName;
		currentAction = Action.CheckRoomNameUniqueness;
		isWaitingForHostList = true;
		MasterServer.RequestHostList(gameType);

		mesCtrl.CreateMessage ("Creating room: \"" + roomName.ToString() + "\"", true, CeaseWaitingForHostList);
	}

	void UnregisterRoom()
	{
		Network.Disconnect();
		MasterServer.UnregisterHost();
		Debug.Log("Room was unregistered");
	}
	
	public void ConnectToRoom(string roomName)
	{
		this.roomName= roomName;
		currentAction = Action.ConnectToRoom;
		isWaitingForHostList = true;
		MasterServer.RequestHostList(gameType);

		mesCtrl.CreateMessage ("Finding room: \"" + roomName.ToString() + "\"", true, CeaseWaitingForHostList);
	}

	void CeaseWaitingForHostList()
	{
		isWaitingForHostList = false;
	}



	//основное событие
	void OnMasterServerEvent(MasterServerEvent msEvent) 
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded) 
		{
			mesCtrl.CreateMessage ("Room: \"" + roomName + "\" was created. \n\rWaiting for other player", true, UnregisterRoom);
			Debug.Log ("Registered");
			
		}
		
		if (msEvent == MasterServerEvent.HostListReceived && isWaitingForHostList) 
		{
			hostData = MasterServer.PollHostList();
			Debug.Log("hostData.Length = " + hostData.Length);

			//------
			if(currentAction == Action.CheckRoomNameUniqueness)
			{
				bool isUniqueRoomName= true;
				for(int i = 0; i < hostData.Length; i++)
				{
					if(hostData[i].gameName == roomName)
					{
						isUniqueRoomName = false;
						mesCtrl.CreateMessage ("The room with the same name already exists!");
						break;
					}
				}
				if(isUniqueRoomName)
				{
					Network.InitializeServer (playerLimit, listenPort, !Network.HavePublicAddress ());
					MasterServer.RegisterHost (gameType, roomName, comment);
				}
			}

			//------
			if(currentAction == Action.ConnectToRoom)
			{
				for(int i = 0; i < hostData.Length; i++)
				{
					if(hostData[i].gameName == roomName)
					{
						Debug.Log("Connected players: " + hostData[i].connectedPlayers.ToString());
					    if (hostData[i].connectedPlayers < playerLimit)
						{
							Network.Connect(hostData[i]);
							break;
						}
						else mesCtrl.CreateMessage ("The room is full already!");
					}
				}
			}

			//------

		}
	}


	//вспомогательные события
	void OnServerInitialized()
	{
		Debug.Log ("Server was initialized!");
	}
	void OnFailedToConnect(NetworkConnectionError error)
	{
		mesCtrl.CreateMessage ("Connection failed.");
		Debug.Log ("Connection failed. " +error.ToString());
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info)
	{
		mesCtrl.CreateMessage ("Could not connect to master server: " + info.ToString());
		Debug.Log("Could not connect to master server: " + info);
	}
	

}
