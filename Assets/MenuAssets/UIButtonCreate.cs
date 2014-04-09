using UnityEngine;
using System.Collections;

public class UIButtonCreate : MonoBehaviour {

	public UIInput roomNameInput;
	
	ConnectionController conCtrl;
	string roomName;

	

	void Start() 
	{
		conCtrl = ConnectionController.Get();
	}

	void Update()
	{

	}



	void OnClick() 
	{
		roomName = "q";
		if (roomNameInput.text != "")
						roomName = roomNameInput.text;

		conCtrl.RegisterRoom(roomName);

	}
	/*
	void Unregister()
	{
		Network.Disconnect();
		MasterServer.UnregisterHost();

	}




	void OnServerInitialized()
	{
		Debug.Log ("Server was initialized!");
	}
	void OnFailedToConnect(NetworkConnectionError error)
	{
		mesCtrl.CreateMessage ("Connection failed.");
		Debug.Log ("Connection failed. " +error.ToString());
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) 
	{
		if (msEvent == MasterServerEvent.RegistrationSucceeded) 
		{
			mesCtrl.CreateMessage ("Room: \"" + roomName + "\" was created. \n\rWaiting for other player", true, Unregister);
			Debug.Log ("Registered");
			Debug.Log("Currently connected: " );
		}

		if (msEvent == MasterServerEvent.HostListReceived) 
		{
			hostData = MasterServer.PollHostList();
			Debug.Log("hostData.Length = " + hostData.Length);
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
				Network.InitializeServer (playerLimit, 25000, !Network.HavePublicAddress ());
				MasterServer.RegisterHost ("M", roomName, "l33t game for all");
			}
			
		}
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info)
	{
		mesCtrl.CreateMessage ("Could not connect to master server: " + info.ToString());
		Debug.Log("Could not connect to master server: " + info);
	}
*/

}
