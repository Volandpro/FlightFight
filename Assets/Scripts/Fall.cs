using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {
	float angle;
	float fallingSpeed=1;
	public float fallingSpeedDown=0;
	Move move;
	// Use this for initialization
	void Start () {
		move=this.GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () {
		angle=Vector3.Angle(transform.forward,Vector3.forward);
		if(angle<40)
		{
			move.Falling();
			fallingSpeed+=Time.deltaTime/2;
		}
		else
		{
			if(fallingSpeed>0)
			fallingSpeed-=Time.deltaTime;
			if(fallingSpeed<0)fallingSpeed=0;
			move.NotFalling();
		}
		if(angle>130)
		{
			move.Accel();
		}
		else
		{
			move.NotAccel();
		}
		if(this.transform.position.z>930)
		{
			fallingSpeedDown=this.transform.position.z-930;
			fallingSpeedDown/=5;
		}
		else
			fallingSpeedDown=0;
		transform.position-=fallingSpeed*Vector3.forward/10;
		transform.position-=fallingSpeedDown*Vector3.forward/15;
	}
}
