using UnityEngine;
using System.Collections;

public class SpawnBonus : MonoBehaviour {
	public GameObject[] bonuses;
	GameObject bonus;
	// Use this for initialization
	void Start () {
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			StartCoroutine(SpawnNet());
		}
		else
		{
			StartCoroutine(Spawn());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator Spawn()
	{
		while(true)
		{
			yield return new WaitForSeconds(10);
			bonus=Instantiate(bonuses[Random.Range(0,3)],new Vector3(Random.Range(1290,1350),24.48f,Random.Range(790,930)), Quaternion.identity) as GameObject;
		}
	}
	IEnumerator SpawnNet()
	{
		while(true)
		{
			yield return new WaitForSeconds(25);
			bonus=Network.Instantiate(bonuses[Random.Range(0,3)],new Vector3(Random.Range(1290,1350),24.48f,Random.Range(790,930)), Quaternion.identity,0) as GameObject;
		}
	}
}
