using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position+=transform.forward*Time.deltaTime*speed;
	}
}
