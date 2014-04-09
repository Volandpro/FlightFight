using UnityEngine;
using System.Collections;
using AnimationOrTween;

[System.Serializable]
public class Plane1
{
	public string name;
}

public enum Position {Centre, Left, Right, None};

public class UIPanelPlaneChoice : MonoBehaviour {

	public GameObject planeBoxPref;
	public Plane1[] planes;


	int curIndex = -1;
	UIPlaneBox[] planeBoxScripts;

	/// <summary>
	/// The indexes of planeBoxScripts array, which represent chosen plane and its two neighboors on the left and on the right side.
	/// </summary>
	int[] visiblePlaneBoxIndexes = new int[1 + neighboorsAmount*2];
	const int neighboorsAmount = 2;

	void Start ()
	{
		planeBoxScripts = new UIPlaneBox[planes.Length];
		curIndex = neighboorsAmount;

		RefreshVisibleIndexesArray();
		for(int i = 0; i < curIndex-1;i++) CreatePlaneBox(i, Position.None);
		for(int i = curIndex+2; i < planeBoxScripts.Length;i++) CreatePlaneBox(i, Position.None);
		CreatePlaneBox(curIndex, Position.Centre);
		CreatePlaneBox(curIndex+1, Position.Right);
		CreatePlaneBox(curIndex-1, Position.Left);



	}
	void Update() {}



	void CreatePlaneBox(int index, Position pos)
	{
		GameObject planeBox = GameObject.Instantiate(planeBoxPref) as GameObject;
		planeBox.transform.parent = this.transform.FindChild("Window");
		planeBoxScripts[index] = (UIPlaneBox)(planeBox.GetComponent("UIPlaneBox"));
		planeBoxScripts[index].Create(pos, planes[index].name);
	}

	public void MoveList(bool toLeft)
	{
		int beginIndex = 0;
		int lastIndex = visiblePlaneBoxIndexes.Length - 1;
		if(toLeft) beginIndex++;
		else lastIndex--;

		for(int i = beginIndex; i<= lastIndex; i++)
		{
			planeBoxScripts[visiblePlaneBoxIndexes[i]].PlayAnimation(toLeft);
		}

		if(toLeft) 
		{
			curIndex ++;
			if(curIndex == planeBoxScripts.Length) curIndex = 0;  
		}
		else 
		{
			curIndex --;
			if(curIndex < 0) curIndex = planeBoxScripts.Length - 1;  
		}
		RefreshVisibleIndexesArray();
	}



	void RefreshVisibleIndexesArray()
	{
		visiblePlaneBoxIndexes[neighboorsAmount] = curIndex;
		for(int i = 1; i<=neighboorsAmount;i++)
		{
			if(planeBoxScripts.Length > (curIndex + i)) visiblePlaneBoxIndexes[neighboorsAmount + i] = curIndex + i;
			else 
			{
				if( i==2 && visiblePlaneBoxIndexes[neighboorsAmount + i - 1] == 0) visiblePlaneBoxIndexes[neighboorsAmount + i] = 1;
				else visiblePlaneBoxIndexes[neighboorsAmount + i]= 0;
			}
		}
		for(int i = 1; i<=neighboorsAmount;i++)
		{
			if(0 <= (curIndex - i)) visiblePlaneBoxIndexes[neighboorsAmount - i] = curIndex - i;
			else 
			{
				if(visiblePlaneBoxIndexes[neighboorsAmount - i + 1] == planeBoxScripts.Length - 1) visiblePlaneBoxIndexes[neighboorsAmount - i] = planeBoxScripts.Length - 2;
				else visiblePlaneBoxIndexes[neighboorsAmount - i] = planeBoxScripts.Length - 1;
			}

		}
	}

}
