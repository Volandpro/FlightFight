using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Block")
		{
			this.gameObject.GetComponent<HP>().IsHit(1000);
		}
	}
}
