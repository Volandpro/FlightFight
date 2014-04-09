using UnityEngine;
using System.Collections;

public class HP : MonoBehaviour {
	int hpMax;
	public int hpCur;
	bool show;
	public Move move;
	public GameObject particle;
	public GameObject smoke;
	public GameObject blackSmoke;
	public GameObject greySmoke;
	public GameObject fire;
	ParticleEmitter smokeEmit;
	ParticleEmitter blackSmokeEmit;
	ParticleEmitter greySmokeEmit;
	// Use this for initialization
	void Start () {
		smokeEmit=smoke.GetComponent<ParticleEmitter>();
		blackSmokeEmit=blackSmoke.GetComponent<ParticleEmitter>();
		greySmokeEmit=greySmoke.GetComponent<ParticleEmitter>();
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			if(!networkView.isMine)this.enabled=false;
		}
		hpMax=this.GetComponent<Param>().hpMax;
		hpCur=hpMax;
		if(this.gameObject.tag!="Bot") show=true;
	}
	void OnGUI()
	{
		if(show)
		GUI.Label(new Rect(10,10,100,100),hpCur.ToString());
	}
	// Update is called once per frame
	void Update () {
	
	}
	public void IsHit(int damage)
	{
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
		NetworkViewID id=Network.AllocateViewID();
			if(this.gameObject)
		networkView.RPC("HitNet",RPCMode.All,id,damage);
		}
		else
		{
			Hit(damage);
		}
	}
	void Hit(int dmg)
	{
		hpCur-=dmg;
		if(hpCur<70)
		{
			greySmokeEmit.emit=true;
			smokeEmit.minSize=0;
			smokeEmit.maxSize=0;
		}
		if(hpCur<40)
		{
			blackSmokeEmit.emit=true;
			greySmokeEmit.minSize=0;
			greySmokeEmit.maxSize=0;
		}
		if(hpCur<20) fire.SetActive(true);
		if(hpCur<=0)
		{
			Camera.main.GetComponent<Inst>().LoadMenuM();
			particle=Instantiate(particle,this.transform.position,Quaternion.identity) as GameObject ;
			Destroy(this.gameObject);
		}
		if(hpCur>hpMax) hpCur=hpMax;
	}
	[RPC]
	void HitNet(NetworkViewID id,int dmg)
	{
		hpCur-=dmg;
		if(hpCur<70)
		{
			greySmokeEmit.emit=true;
			smokeEmit.minSize=0;
			smokeEmit.maxSize=0;
		}
		if(hpCur<40)
		{
			blackSmokeEmit.emit=true;
			greySmokeEmit.minSize=0;
			greySmokeEmit.maxSize=0;
		}
		if(hpCur<20) fire.SetActive(true);
		if(hpCur<=0)
		{
			Camera.main.GetComponent<Inst>().LoadMenuM();
			particle=Instantiate(particle,this.transform.position,Quaternion.identity) as GameObject ;
			Destroy(this.gameObject);
		}
		if(hpCur>hpMax) hpCur=hpMax;
	}
}
