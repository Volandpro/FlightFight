using UnityEngine;
using System.Collections;

public class Param : MonoBehaviour {
	public int hpMax;
	// Use this for initialization
	void Start () {
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(!networkView.isMine) this.enabled=false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
