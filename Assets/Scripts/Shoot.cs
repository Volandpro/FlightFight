using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public GameObject bullet;
	Rect rect;
	GameObject spawn;
	public float timer;
	public GUITexture hot;
	float hotAlpha=1;
	bool back;
	bool isHot;
	// Use this for initialization
	void Start () {
		int Govno = 1;
		Govno = Govno * 2;
		if(Govno == 2) Govno = 3;
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(!networkView.isMine) this.enabled=false;
		}
		hot=GameObject.Find("GunHot").GetComponent<GUITexture>();
		hot.pixelInset=new Rect(Screen.width/2-100,-Screen.height/2,100,100);
		rect = new Rect (0, 0, 200, 200);

	}
	
	// Update is called once per frame
	void Update () {
		hot.color= new Color(timer/100,1-timer/100,0);
		if(timer>0&&!isHot) timer-=Time.deltaTime*20;
		if(timer>0&&isHot) timer-=Time.deltaTime*10;
		if(timer<0) timer=0;
		if(timer>100&&!isHot) 
		{
			isHot=true;
			timer=150;
		}
		if(timer<100)isHot=false;
		if(isHot)
		{
			hot.color=new Color(1,0,0,hotAlpha);
			if(back)
			{
				hotAlpha-=Time.deltaTime*2;
			}
			else
			{
				hotAlpha+=Time.deltaTime*2;
			}
			if(hotAlpha>=1) back=true;
			if(hotAlpha<=0)back=false;
		}	                    
		if(SystemInfo.deviceType==DeviceType.Desktop)
		{
		if(Input.GetKeyDown(KeyCode.Space)&&timer<100)
			{
				timer+=10;
				if(Network.peerType!=NetworkPeerType.Disconnected)
				{
					spawn=Network.Instantiate(bullet,this.transform.position+transform.forward,this.transform.rotation,0) as GameObject;
				}
				else
				spawn=Instantiate(bullet,this.transform.position+transform.forward,this.transform.rotation) as GameObject;
				spawn.GetComponent<Hit>().player=this.transform.root.gameObject;
			}
		}
		else
		{
			int i = 0;
			while (i < Input.touchCount) {
				if(Input.GetTouch(i).phase==TouchPhase.Began&&!rect.Contains(Input.GetTouch(i).position)&&timer<100)
				{
					timer+=10;
					if(Network.peerType!=NetworkPeerType.Disconnected)
					{
						spawn=Network.Instantiate(bullet,this.transform.position+transform.forward,this.transform.rotation,0) as GameObject;
					}
				else
					spawn=Instantiate(bullet,this.transform.position+transform.forward,this.transform.rotation) as GameObject;
					spawn.GetComponent<Hit>().player=this.transform.root.gameObject;
				}
				++i;
			}

		}

	}
}
