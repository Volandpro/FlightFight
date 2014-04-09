using UnityEngine;
using System.Collections;

public class PropRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		///хуючка
		transform.Rotate(Vector3.up*20);
	}
}
