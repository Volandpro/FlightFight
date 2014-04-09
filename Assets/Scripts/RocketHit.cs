using UnityEngine;
using System.Collections;

public class RocketHit : MonoBehaviour {
	public GameObject player;
	public int damage;
	public GameObject boom;
	// Use this for initialization
	void Start () {
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(!networkView.isMine) this.enabled=false;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag!="Bullet"&&other.gameObject.tag!="Block"&&other.gameObject!=player)
		{
			if(Network.peerType!=NetworkPeerType.Disconnected)
			{
				if(networkView.isMine)
				{
					other.gameObject.GetComponent<HP>().IsHit(damage);
					boom=Network.Instantiate(boom,this.transform.position,Quaternion.identity,0) as GameObject;
					Network.Destroy(this.gameObject);
				}
			}
			else
			{
				other.gameObject.GetComponent<HP>().IsHit(damage);
				boom=Instantiate(boom,this.transform.position,Quaternion.identity) as GameObject;
				Destroy(this.gameObject);
			}
		}
	}
}
