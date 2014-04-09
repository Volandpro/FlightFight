using UnityEngine;
using System.Collections;

public class CleverRocketMove : MonoBehaviour {
	public GameObject target;
	GameObject[] targets;
	public GameObject player;
	Vector3 relativePos;
	Quaternion rotation1;
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,7f);
		player=this.gameObject.GetComponent<RocketHit>().player;
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(!networkView.isMine)this.enabled=false;
			targets=GameObject.FindGameObjectsWithTag("Player");
				for(int i=0;i<targets.Length;i++)
				{
					if(targets[i]!=player)
					{
					target=targets[i];
					}
				}
		}
		else
		{
		target=GameObject.FindGameObjectWithTag("Bot");
		}

	}
	[RPC]
	void Enable(NetworkViewID viewID) {
		NetworkView view = NetworkView.Find(viewID);
		target=view.gameObject;
	}
	// Update is called once per frame
	void Update () {
		relativePos = target.transform.position+Vector3.up*2 - transform.position;
		rotation1 = Quaternion.LookRotation(relativePos);
		this.transform.rotation=Quaternion.Lerp(this.transform.rotation,rotation1,2.4f*Time.deltaTime);
		this.transform.position+=transform.forward*Time.deltaTime*27;
	}
}
