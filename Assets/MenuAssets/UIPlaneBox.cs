using UnityEngine;
using System.Collections;
using AnimationOrTween;

public class UIPlaneBox : MonoBehaviour {

	

	Vector3 centralPosition = new Vector3(0,64,0);
	Vector3 centralScale = new Vector3(1,1,1);

	Vector3 leftPosition = new Vector3(-392,40,0);
	Vector3 rightPosition = new Vector3(392,40,0);
	Vector3 sideScale = new Vector3(0.6f,0.6f,1);


	Position curPosition;

	void Start () {

	}
	void Update () {
	
	}

	public void PlayAnimation(bool toLeft)
	{
		Direction dir = Direction.Forward;
		DisableCondition disCond = DisableCondition.DoNotDisable;
		EnableCondition enCond = EnableCondition.DoNothing;

		string clipName = "";
		if(toLeft)
		{
			if(curPosition == Position.Left) 
			{
				clipName = "PlaneBoxToLeft2";
				curPosition = Position.None;
				disCond = DisableCondition.DisableAfterForward;
			}
			else{
				if(curPosition == Position.Centre)
				{
					clipName = "PlaneBoxToLeft";
					curPosition = Position.Left;
				}
				if(curPosition == Position.Right)
				{
					clipName = "PlaneBoxToRight";
					dir = Direction.Reverse;
					curPosition = Position.Centre;
				}
				if(curPosition == Position.None) 
				{
					clipName = "PlaneBoxToRight2";
					dir = Direction.Reverse;
					enCond = EnableCondition.EnableThenPlay;
					curPosition = Position.Right;
				}
			}
		}
		else
		{
			if(curPosition == Position.Right) 
			{
				clipName = "PlaneBoxToRight2";
				curPosition = Position.None;
				disCond = DisableCondition.DisableAfterForward;
			}
			else
			{
				if(curPosition == Position.Centre)
				{
					clipName = "PlaneBoxToRight";
					curPosition = Position.Right;
				}
				if(curPosition == Position.Left)
				{
					clipName = "PlaneBoxToLeft";
					dir = Direction.Reverse;
					curPosition = Position.Centre;
				}
				if(curPosition == Position.None) 
				{
					clipName = "PlaneBoxToLeft2";
					dir = Direction.Reverse;
					enCond = EnableCondition.EnableThenPlay;
					curPosition = Position.Left;
				}
			}
		}

		ActiveAnimation.Play (this.gameObject.animation, clipName, dir, enCond, disCond);
	}

	public void Create(Position pos, string description)
	{
		curPosition = pos;
		if(pos == Position.Centre)
		{
			this.transform.localPosition = centralPosition;
			this.transform.localScale = centralScale;
		}
		else
		{
			if(pos == Position.Left) this.transform.localPosition = leftPosition;
			if(pos == Position.Right) this.transform.localPosition = rightPosition;
			this.transform.localScale = sideScale;
		}
		if(pos == Position.None)
		{
			this.gameObject.SetActive(false);
		}
		SetDescription(description);
	}


	UILabel GetDescriptionLabel()
	{
		return this.transform.FindChild ("lblDescription").gameObject.GetComponent<UILabel>();
	}

	void SetDescription(string text)
	{
		GetDescriptionLabel().text = text;
	}
}
