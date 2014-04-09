using UnityEngine;
using System.Collections;

public enum TypeBonus
{
	speed,
	qua,
	heal
}
public class BonusEnter : MonoBehaviour {
	public TypeBonus type;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Act(GameObject plane)
	{
		switch(type)
		{
		case TypeBonus.speed:
		StartCoroutine(Speed(plane));
			break;
		case TypeBonus.qua:
		StartCoroutine(Qua(plane));
			break;
		case TypeBonus.heal:
			StartCoroutine(Heal(plane));
			break;
		}
	}
	IEnumerator Speed(GameObject plane)
	{
		plane.GetComponent<Move>().speedBonus=2;
		yield return new WaitForSeconds(4);
		plane.GetComponent<Move>().speedBonus=1;
		Destroy(this.gameObject);
	}
	IEnumerator Heal(GameObject plane)
	{
		plane.GetComponent<HP>().IsHit(-10);
		yield return new WaitForSeconds(0);
		Destroy(this.gameObject);
	}
	IEnumerator Qua(GameObject plane)
	{
		plane.GetComponent<Move>().rotationSpeed=2;
		yield return new WaitForSeconds(4);
		plane.GetComponent<Move>().rotationSpeed=1;
		Destroy(this.gameObject);
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player")
		{
			Act(other.gameObject);
			if(this.gameObject.GetComponent<MeshRenderer>())
			this.gameObject.GetComponent<MeshRenderer>().enabled=false;
			this.gameObject.GetComponent<SphereCollider>().enabled=false;
		}
	}
}
