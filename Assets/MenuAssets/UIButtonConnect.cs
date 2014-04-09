using UnityEngine;
using System.Collections;

public class UIButtonConnect : MonoBehaviour {

	public UIInput roomNameInput;

	ConnectionController conCtrl;
	string roomName;


	//string roomName;
	//bool isConnectionCancelled = false;
	//int tryNumber = 1;

	void Start() 
	{
		conCtrl = ConnectionController.Get();
	}

	void OnClick() 
	{
		roomName = "q";
		if (roomNameInput.text != "")
			roomName = roomNameInput.text;

		conCtrl.ConnectToRoom(roomName);
		//StartCoroutine(Connection ());
		//isConnectionCancelled = false;
		//ConnectToMasterServer ();
		//mesCtrl.CreateMessage ("Finding room: \"" + roomName.ToString() + "\"", true, CeaseConnection);
	}

	/*
	void CeaseConnection()
	{
		isConnectionCancelled = true;
	}


	IEnumerator Connection()
	{
		while(!isConnectionCancelled)
		{
			MasterServer.RequestHostList("M"); 
			yield return new WaitForSeconds(4 + tryNumber);
			tryNumber++;

		}	
	}

	void ConnectToMasterServer()
	{
		if(!isConnectionCancelled) MasterServer.RequestHostList("M"); 
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived) 
		{
			hostData = MasterServer.PollHostList();
			Debug.Log("hostData.Length = " + hostData.Length);
			for(int i = 0; i < hostData.Length; i++)
			{
				if(hostData[i].gameName == roomName)
				{
					Network.Connect(hostData[i]);
					break;
				}
			}
		}
	}

	void OnConnectedToServer()
	{
		Application.LoadLevel(1);
	}

	void OnFailedToConnect(NetworkConnectionError error)
	{
		mesCtrl.CreateMessage ("Connection failed.");
		Debug.Log ("Connection failed. " +error.ToString());
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("Could not connect to master server: " + info);
	}

	
	*/
}
