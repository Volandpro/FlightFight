using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {
	public Animator anim;
	public float angle;
	public float angleLocal;
	public GameObject model;
	public bool b;
	// Use this for initialization
	void Start () {
		anim=model.GetComponent<Animator>();
	}
	IEnumerator Connection()
	{
		anim.SetBool("play",true);
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("play",false);
	}
	IEnumerator Back()
	{
		anim.SetBool("back",true);
		yield return new WaitForSeconds(0);
		anim.SetBool("back",false);
	}
	// Update is called once per frame
	void Update () {
		//model.transform.localRotation=Quaternion.Euler(new Vector3(0,this.transform.localRotation.eulerAngles.y,this.transform.localRotation.eulerAngles.z));
		angleLocal=Vector3.Angle(model.transform.forward,transform.right);
		angle=Vector3.Angle(this.transform.forward,Vector3.right);
		if(angle<90&&angleLocal<179) 
		{
			model.transform.Rotate(Vector3.up*3);
			//anim.Play("Turn",0);
			//anim.SetBool("play",false);
		}
		if(angle>90&&angleLocal>1) 
		{
			model.transform.Rotate(-Vector3.up*3);
		}

		//Debug.Log(angle);
		//if(angle>90&&angleLocal<90) model.transform.Rotate(new Vector3(0,-10,0));
	}
}
