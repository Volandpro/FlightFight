using UnityEngine;
using System.Collections;

public class AdditionalWeapon : MonoBehaviour {
	public GameObject[] weapons;
	GameObject spawn;
	int weapon;
	int count=3;
	Rect rect;
	public float timer;
	// Use this for initialization
	void Start () {
		rect=new Rect(Screen.width-100,150,100,100);
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(!networkView.isMine) this.enabled=false;
		}
		weapon=Random.Range(0,2);
	}
	[RPC]
	void Enable(NetworkViewID viewID) {
		NetworkView view = NetworkView.Find(viewID);
		view.gameObject.GetComponent<Proverka>().enabled=true;
	}
	void OnGUI()
	{
		GUI.Label(new Rect(400,10,90,30),count+"\\"+timer);
		GUI.Box(new Rect(Screen.width-100,Screen.height/2,100,100),"Weap");
	}
	// Update is called once per frame
	void Update () {
		if(rect.Contains(Input.mousePosition))Debug.Log("ADS");
		if(timer>0)timer-=Time.deltaTime;
		if(timer<0)timer=0;
		if(SystemInfo.deviceType==DeviceType.Desktop)
		{
			if(Input.GetKeyDown(KeyCode.F)&&count>0&&timer==0) 
			{
				count--;
				timer=15;
				if(Network.peerType!=NetworkPeerType.Disconnected)
				{
					if(weapon==2)
					{
						spawn=Network.Instantiate(weapons[weapon],this.transform.position+Vector3.up*2-transform.forward,this.transform.rotation,0)as GameObject;
						Move move=this.GetComponent<Move>();
						spawn.GetComponent<BombFall>().GetSpeed(move.speed,move.fall,move.speedBonus);
						networkView.RPC("Enable", RPCMode.AllBuffered, spawn.GetComponent<NetworkView>().viewID);
					}
					else spawn=Network.Instantiate(weapons[weapon],this.transform.position+Vector3.up*2,this.transform.rotation,0)as GameObject;
				}
				else
				{
					if(weapon==2)
					{
						spawn=Instantiate(weapons[weapon],this.transform.position+Vector3.up*2-transform.forward,this.transform.rotation)as GameObject;
						Move move=this.GetComponent<Move>();
						spawn.GetComponent<BombFall>().GetSpeed(move.speed,move.fall,move.speedBonus);
					}
					else spawn=Instantiate(weapons[weapon],this.transform.position+Vector3.up*2,this.transform.rotation)as GameObject;
				}
				spawn.GetComponent<RocketHit>().player=this.transform.root.gameObject;
			}
		}
		else
		{
			int i = 0;
			while (i < Input.touchCount) {
				if(Input.GetTouch(i).phase==TouchPhase.Began&&rect.Contains(Input.GetTouch(i).position)&&count>0&&timer==0)
				{
					count--;
					timer=1;
					if(Network.peerType!=NetworkPeerType.Disconnected)
					{
						if(weapon==2)
						{
							spawn=Network.Instantiate(weapons[weapon],this.transform.position+Vector3.up*2-transform.forward,this.transform.rotation,0)as GameObject;
							Move move=this.GetComponent<Move>();
							spawn.GetComponent<BombFall>().GetSpeed(move.speed,move.fall,move.speedBonus);
							networkView.RPC("Enable", RPCMode.AllBuffered, spawn.GetComponent<NetworkView>().viewID);
						}
						else spawn=Network.Instantiate(weapons[weapon],this.transform.position+Vector3.up*2,this.transform.rotation,0)as GameObject;
					}
					else
					{
						if(weapon==2)
						{
							spawn=Instantiate(weapons[weapon],this.transform.position+Vector3.up*2-transform.forward,this.transform.rotation)as GameObject;
							Move move=this.GetComponent<Move>();
							spawn.GetComponent<BombFall>().GetSpeed(move.speed,move.fall,move.speedBonus);
						}
						else spawn=Instantiate(weapons[weapon],this.transform.position+Vector3.up*2f,this.transform.rotation)as GameObject;
					}
					spawn.GetComponent<RocketHit>().player=this.gameObject;

				}
				++i;
			}
		}
	}
}
