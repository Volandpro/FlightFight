using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public Transform plane;
	public float posX;
	public float posZ;
	float fps;
	float pingTime;
	Ping pingTest;
	// Use this for initialization
	void Start () {
		pingTest = new Ping("5.175.138.205");    
		StartCoroutine(Check());
		QualitySettings.vSyncCount=1;
	}
	IEnumerator Check() {            
		while (true)
		{
		yield return new WaitForSeconds(1f);
		pingTime = pingTest.time;
		}
	}
	void OnGUI()
	{
		GUI.Label(new Rect(200,10,100,30),fps.ToString());
		GUI.Label(new Rect(310,10,100,30),pingTime.ToString());
	}
	// Update is called once per frame
	void Update () {
		fps=1.0f/Time.deltaTime;
		//this.transform.rotation=Quaternion.Euler(new Vector3(90,0,0));
	if(plane!=null)
		{
			if(plane.position.x<1350&&plane.position.x>1270)
			posX=plane.position.x;
			if(plane.position.z<930&&plane.position.z>790)
			posZ=plane.position.z;
			if(posX!=0&&posZ!=0)
			this.transform.position=new Vector3(posX,this.transform.position.y,posZ);
		}
	}
	public void Attach(Transform plane)
	{
		//this.plane=plane;
		//this.transform.parent=plane.transform;
		//this.transform.localPosition=new Vector3(0,this.transform.localPosition.y,0);
	}
}
