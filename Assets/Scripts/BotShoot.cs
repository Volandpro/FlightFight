using UnityEngine;
using System.Collections;

public class BotShoot : MonoBehaviour {
	GameObject target;
	public GameObject bullet;
	GameObject spawn;
	public float timer;
	// Use this for initialization
	void Start () {
		target=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		timer-=Time.deltaTime;
		//Debug.Log(Vector3.Angle(target.transform.position - transform.position, transform.forward));
		if(target)
		{
			if (Vector3.Angle(target.transform.position - transform.position, transform.forward)<15&&timer<=0)
			{
				timer=0.3f;
				spawn=Instantiate(bullet,this.transform.position+transform.forward*1.7f,this.transform.rotation) as GameObject;
			}
		}
	}
}
