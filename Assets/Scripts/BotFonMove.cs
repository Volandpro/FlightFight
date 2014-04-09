using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotFonMove : MonoBehaviour {
	public Vector3 movePoint;
	Quaternion direction;
	public GameObject target;
	List<GameObject> bots;
	public GameObject player;
	public bool way;
	public GameObject[] smokes;
	int rnd;
	// Use this for initialization
	void Start () {
		rnd=Random.Range(0,3);
		smokes[rnd].SetActive(true);
		if(Random.Range(0,3)==0&&rnd==1) smokes[3].SetActive(true);
		bots =Camera.main.GetComponent<FonBots>().bots;
		bots.Add(this.gameObject);
		target=bots[Random.Range(0,bots.Count)];
		StartCoroutine(ChangeTarget());
		StartCoroutine(FindPlayer());
	}
	IEnumerator FindPlayer()
	{
		yield return new WaitForSeconds(1);
		player=GameObject.FindGameObjectWithTag("Player");
	}
	IEnumerator ChangeTarget()
	{
		while(true)
		{
			yield return new WaitForSeconds(5+Random.Range(-1,2));
			if(Random.Range(0,2)==0)
			{
				while(true)
				{
				target=bots[Random.Range(0,bots.Count)];
					if(target!=this.gameObject)break;
				}
				way=false;
			}
			else
			{
				movePoint=new Vector3(player.transform.position.x+ Random.Range(-20,21),-32,player.transform.position.z+ Random.Range(-10,11));
				movePoint-=transform.position;
				way=true;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if(!way)
			movePoint = target.transform.position - transform.position;
			direction = Quaternion.LookRotation(movePoint);
			transform.rotation =Quaternion.Lerp (transform.rotation, direction,Time.deltaTime);
			this.transform.position+=transform.forward*20*Time.deltaTime;
	}
}
