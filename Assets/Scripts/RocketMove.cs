using UnityEngine;
using System.Collections;

public class RocketMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,2f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position+=transform.forward*Time.deltaTime*50;
	}
}
