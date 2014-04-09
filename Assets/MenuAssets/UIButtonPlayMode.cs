using UnityEngine;
using System.Collections;

public class UIButtonPlayMode : MonoBehaviour {

	public GameObject btnOtherPlayMode;
	public GameObject panelOtherPlayMode;

	void Start()
	{
		panelOtherPlayMode.SetActive (false);
	}

	void OnClick()
	{
		if(panelOtherPlayMode.activeSelf == true) 
			ActiveAnimation.Play (btnOtherPlayMode.animation, AnimationOrTween.Direction.Reverse);
	}

}
