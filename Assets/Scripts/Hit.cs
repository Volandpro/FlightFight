using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
	public GameObject player;
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
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag!="Bullet"&&other.gameObject.tag!="Block"&&other.gameObject!=player)
		{
			if(Network.peerType!=NetworkPeerType.Disconnected)
			{
				if(networkView.isMine)
				{
					other.gameObject.GetComponent<HP>().IsHit(5);
					Network.Destroy(this.gameObject);
				}
			}
			else
			{
				other.gameObject.GetComponent<HP>().IsHit(5);
					Destroy(this.gameObject);
			}
		}
	}
}
