using UnityEngine;
using System.Collections;

public class UIButtonNextPlane : MonoBehaviour {
	
	bool enabled = true;
	void OnClick()
	{
		if(enabled)
		{
			UIPanelPlaneChoice panelScript = (UIPanelPlaneChoice)(GameObject.Find("panelPlaneChoice").GetComponent("UIPanelPlaneChoice"));
			panelScript.MoveList(false);
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
