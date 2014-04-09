using UnityEngine;
using System.Collections;

public class UIButtonPreviousPlane : MonoBehaviour {

	bool enabled = true;
	void OnClick()
	{
		if(enabled)
		{
			UIPanelPlaneChoice panelScript = (UIPanelPlaneChoice)(GameObject.Find("panelPlaneChoice").GetComponent("UIPanelPlaneChoice"));
			panelScript.MoveList(true);
			StartCoroutine(Waiting());
		}
	}
	
	IEnumerator Waiting()
	{
		enabled = false;
		yield return new WaitForSeconds(0.25f);
		enabled = true;
	}
}
