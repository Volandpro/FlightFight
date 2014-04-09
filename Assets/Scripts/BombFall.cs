using UnityEngine;
using System.Collections;

public class BombFall : MonoBehaviour {
	float speed;
	float fall;
	float speedBonus;
	public Vector3 direction;
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(networkView.isMine) 
			{
		direction-=new Vector3(0,0,0.01f);
		this.transform.position+=direction*Time.deltaTime*50;
			}
		}
		else
		{
			direction-=new Vector3(0,0,0.01f);
			this.transform.position+=direction*Time.deltaTime*50;
		}

	}
	public void GetSpeed(float speed,float fall,float speedBonus)
	{
		this.speed=speed;
		this.fall=fall;
		this.speedBonus=speedBonus;
		direction=transform.forward*Time.deltaTime*speed*fall*speedBonus;
		Debug.Log(direction);
	}
}
