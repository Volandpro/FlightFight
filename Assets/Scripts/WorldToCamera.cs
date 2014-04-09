using UnityEngine;
using System.Collections;

public class WorldToCamera : MonoBehaviour {
	public Vector3 pos;
	public GameObject markObject;
	float posX;
	float posY;
	GUITexture mark;
	// Use this for initialization
	void Start () {
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(networkView.isMine) 
			{
				this.enabled=false;
			}
		}
		else
		{
			if(this.gameObject.tag!="Bot") this.enabled=false;
		}
		markObject=GameObject.Find("Mark");
		mark =markObject.GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {
		pos = Camera.main.WorldToScreenPoint (transform.position);
		if(pos.x>0&&pos.y>0&&pos.x<Screen.width&&pos.y<Screen.height)
		{
			mark.enabled=false;
		}
		else
		{
			//ужас
			mark.enabled=true;
			if(pos.y>Screen.height)
			{
				posY=Screen.height/2-30;
			}
			if(pos.y<Screen.height&&pos.y>0)
			{
				posY=pos.y-Screen.height/2;
			}
			if(pos.y<0)
			{
				posY=-Screen.height/2;
			}
			if(pos.x>Screen.width)
			{
				posX=Screen.width/2-30;
			}
			if(pos.x<Screen.width&&pos.x>0)
			{
				posX=pos.x-Screen.width/2;
			}
			if(pos.x<0)
			{
				posX=-Screen.width/2;
			}
			mark.pixelInset=new Rect(posX,posY,50,50);
		}
	}
}
