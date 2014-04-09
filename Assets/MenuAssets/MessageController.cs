using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void CallAfterDestructDelegate();

public class MessageController : MonoBehaviour	{

	public GameObject anchor; 
	public GameObject messagePrefab;

	GameObject message;

	CallAfterDestructDelegate callAfterDestr;

	public static MessageController Get()
	{
		return GameObject.Find ("Anchor").GetComponent<MessageController>();
	}

	public void DestroyMessage()
	{
		GameObject.DestroyImmediate (message);
		message = null;
		if (callAfterDestr != null) 
		{
			callAfterDestr();
			callAfterDestr = null;
		}
	}

	public void CreateMessage(string text)
	{
		CreateNewMessage ();
		SetText(text);
	}
	public void CreateMessage(string text, bool isCoroutineNeeded)
	{
		CreateMessage (text);
		AddText("\n\r");
		if(isCoroutineNeeded) StartCoroutine (Waiting ());
	}

	public void CreateMessage(string text, bool isCoroutineNeeded, CallAfterDestructDelegate del)
	{
		CreateMessage (text, isCoroutineNeeded);
		callAfterDestr = del;
	}

	IEnumerator Waiting()
	{
		int i = 0;
		const int numberOfPoints = 3;
		while (message != null) {
			if(i >= 0 && i != numberOfPoints) AddText(".");
			if(i == numberOfPoints) i = -numberOfPoints;
			if(i < 0)
			{ 
				string curText = GetText();
				SetText(curText.Remove(curText.Length - 1));
			}
			i++;
			yield return new WaitForSeconds(0.5f);
		}
	}
	
	void CreateNewMessage()
	{
		if (message != null) DestroyMessage ();

		NGUITools.AddChild (anchor, messagePrefab);
		message = GetMessage();
		message.transform.position = new Vector3 (0, 0, -0.1f); //выводим перед всеми элементами
	}


	GameObject GetMessage()
	{
		return GameObject.FindGameObjectWithTag ("Message");
	}

	GameObject GetMessagePanel()
	{
		return message.transform.FindChild ("Panel").gameObject;
	}

	UILabel GetMessageLabel()
	{
		return GetMessagePanel().transform.FindChild ("lblMessage").gameObject.GetComponent<UILabel>();
	}

	string GetText()
	{
		return GetMessageLabel ().text;
	}

	void AddText(string text)
	{
		SetText (GetText() + text);
	}

	void SetText(string text)
	{
		GetMessageLabel ().text = text;
	}
}