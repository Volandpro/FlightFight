using UnityEngine;
using System.Collections;

public class BotMove : MonoBehaviour {
	public Vector3 movePoint;
	public GameObject target;
	bool change=true;
	Quaternion direction;
	Vector3 oldPositin;
	public float fall=1;
	// Use this for initialization
	void Start () {
		target=GameObject.FindGameObjectWithTag("Player");
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
	// Update is called once per frame
	void Update () {
		if(target)
		{
			if(Vector3.Distance(this.transform.position,target.transform.position)>10)
			{
			movePoint = target.transform.position - transform.position;
			}
			else
			{
				if(Vector3.Angle(target.transform.position - transform.position, transform.forward)<15)
					movePoint = target.transform.position - transform.position;
				else
				{
					if(Vector3.Distance(this.transform.position,oldPositin)<5) change=true;
					if(change)
					{
						movePoint=new Vector3(target.transform.position.x+Random.Range(-10,11),22.23f,target.transform.position.z+Random.Range(-10,11));
						oldPositin=movePoint;
						change=false;
						movePoint=movePoint- transform.position;
					}
					else
					{
						movePoint=oldPositin- transform.position;
					}
				}


			}
			direction = Quaternion.LookRotation(movePoint);
			transform.rotation =Quaternion.Lerp (transform.rotation, direction,Time.deltaTime);
			this.transform.position+=transform.forward*20*fall*Time.deltaTime;
		}
	}
}
