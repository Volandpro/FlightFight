using UnityEngine;
using System.Collections;

public class BotFall : MonoBehaviour {
	public float angle;
	public float fallingSpeed=1;
	public float fallingSpeedDown=0;
	BotMove move;
	// Use this for initialization
	void Start () {
		move=this.GetComponent<BotMove>();
	}
	
	// Update is called once per frame
	void Update () {
		angle=Vector3.Angle(transform.forward,Vector3.forward);
		if(angle<40)
		{
			move.Falling();
			fallingSpeed+=Time.deltaTime/5;
		}
		else
		{
			if(fallingSpeed>0)
				fallingSpeed-=Time.deltaTime*2;
			if(fallingSpeed<0)fallingSpeed=0;
			move.NotFalling();
		}
		if(this.transform.position.z>930)
		{
			fallingSpeedDown=this.transform.position.z-930;
			fallingSpeedDown/=5;
		}
		else
			fallingSpeedDown=0;
		transform.position-=fallingSpeed*Vector3.forward/10;
		transform.position-=fallingSpeedDown*Vector3.forward/10;
	}
}
