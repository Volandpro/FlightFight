using UnityEngine;
using System.Collections;

public class Inst : MonoBehaviour {
	public GameObject playerPrefab;
	public GameObject botPrefab;
	public float pingTime;
	// Use this for initialization
	void Start () {
		StartCoroutine(Check());
		this.transform.position+=new Vector3(0,Screen.width/100,0);
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(Network.peerType==NetworkPeerType.Client)
				Network.Instantiate(playerPrefab, new Vector3(1349,22.23f,890), Quaternion.identity, 0);
			else Network.Instantiate(playerPrefab, new Vector3(1271,22.23f,890), Quaternion.identity, 0);
		}
		else
		{
			GameObject player= Instantiate(playerPrefab, new Vector3(1349,22.23f,890), Quaternion.identity) as GameObject;
			GameObject bot= Instantiate(botPrefab, new Vector3(1271,22.23f,890), Quaternion.identity) as GameObject;
		}
	}
	IEnumerator Check() {
		Ping pingTest = new Ping("5.175.138.205");               
		while (!pingTest.isDone) yield return new WaitForSeconds(0.1f);
		pingTime = pingTest.time;
	}
	public void LoadMenuM()
	{
		StartCoroutine(LoadMenu());
	}
	// Update is called once per frame
	IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(4);
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
		Network.Disconnect();
		MasterServer.UnregisterHost();
		}
		Application.LoadLevel(0);
	}
}
