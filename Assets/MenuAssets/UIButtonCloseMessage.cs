using UnityEngine;
using System.Collections;

public class UIButtonCloseMessage : MonoBehaviour {

	MessageController mesCtrl;

	void Start() 
	{
		mesCtrl = MessageController.Get ();
	}

	void OnClick () {
		mesCtrl.DestroyMessage ();
	}
}
