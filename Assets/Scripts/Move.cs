using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public float speed;
	public float maxSpeed;
	public float speedBonus=1;
	public float rotationSpeed=1;
	public float fall=1;
	Vector3 direction;
	DeviceType device;
	string s;
	Rect rect;
	Quaternion directionNew;
	float angle;
	float angleVertical;
	public GameObject finger;
	GUITexture fingerTexture;
	public Vector3 pos;
	bool flightOff;
	public float posX;
	public float posZ;
	// Use this for initialization
	// Use this for initialization
	void Start () {
		speed=maxSpeed;
		finger=GameObject.Find("Finger");
		fingerTexture=finger.GetComponent<GUITexture>();
		fingerTexture.pixelInset=new Rect(-Screen.width/2,-Screen.height/2,100,100);
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
		if(!networkView.isMine) this.enabled=false;
			else Camera.main.GetComponent<CameraMove>().Attach(this.transform);
		}
		else Camera.main.GetComponent<CameraMove>().Attach(this.transform);
		rect = new Rect (0, 0, 200, 200);
	}
	public Vector3 movePoint;
	public void Accel()
	{
		speed+=Time.deltaTime*3;
		if(fall<0) fall=0;
	}
	public void NotAccel()
	{
		if(speed>maxSpeed)
		speed-=Time.deltaTime*6;
		if(speed<maxSpeed)speed=maxSpeed;
	}
	public void Falling()
	{
		fall-=Time.deltaTime/5;
		if(fall<0) fall=0;
	}
	public void NotFalling()
	{
		if(fall<1)
		fall+=Time.deltaTime;
		if(fall>1) fall=1;
	}
	void MoveAndTurn()
	{
		pos=Camera.main.WorldToScreenPoint(transform.position);
		if(pos.x<150||pos.x>Screen.width-200) flightOff=true;
		else flightOff=false;
		if(SystemInfo.deviceType==DeviceType.Desktop)
		{
			if (rect.Contains(Input.mousePosition))
			{
				direction=(-new Vector3(75,75,0)+Input.mousePosition).normalized;
				fingerTexture.pixelInset=new Rect(-Screen.width/2+Input.mousePosition.x-30,-Screen.height/2+Input.mousePosition.y-30,60,60);
			}
		}
		else
		{
			try
			{
				if(Input.GetTouch(0).phase==TouchPhase.Began||Input.GetTouch(0).phase==TouchPhase.Moved)
				{
					if (rect.Contains(Input.GetTouch(0).position))
					{
						direction=(-new Vector3(75,75,0)+new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y)).normalized;
						fingerTexture.pixelInset=new Rect(-Screen.width/2+Input.GetTouch(0).position.x-30,-Screen.height/2+Input.GetTouch(0).position.y-30,100,100);
					}
				}
			}
			catch
			{
			}
		}
		angleVertical=Vector3.Angle(movePoint,transform.right);
		if(!flightOff)
		{
			movePoint=transform.position+new Vector3(direction.x,0,direction.y)*100;
		}
		else
		{
			movePoint=new Vector3(1350,22,830);
		}
		movePoint=movePoint-transform.position;
		angle=Vector3.Angle(movePoint,transform.forward);
		if(angle>1&&angle<90)
		{
			if(angleVertical<90)
				transform.Rotate(Vector3.up*90*Time.deltaTime*rotationSpeed);
			else transform.Rotate(-Vector3.up*90*Time.deltaTime*rotationSpeed);
		}
		if(angle>90)
		{
			if(angleVertical<90)
				transform.Rotate(Vector3.up*90*Time.deltaTime*rotationSpeed);
			else transform.Rotate(-Vector3.up*90*Time.deltaTime*rotationSpeed);
		}
		//irection = Quaternion.LookRotation(movePoint);
		//transform.rotation =Quaternion.Slerp (transform.rotation, direction,Time.deltaTime* 1f);
		this.transform.position+=transform.forward*speed*fall*speedBonus*Time.deltaTime;
	}
	// Update is called once per frame
	void Update () {
		MoveAndTurn();
		if(this.transform.position.x<1350&&this.transform.position.x>1270)
			posX=this.transform.position.x;
		if(this.transform.position.z<930&&this.transform.position.z>790)
			posZ=this.transform.position.z;
		if(posX!=0&&posZ!=0)
			Camera.main.transform.position=new Vector3(posX,Camera.main.transform.position.y,posZ);
	}
}
