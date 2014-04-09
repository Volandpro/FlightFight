using UnityEngine;
using System.Collections;

public class sfg : MonoBehaviour {
	public GUITexture text;
	public GameObject finger;
	// Use this for initialization
	void Start () {
		text.pixelInset=new Rect(-Screen.width/2,-Screen.height/2,200,200);
	}
	
	// Update is called once per frame


}
