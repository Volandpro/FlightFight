using UnityEngine;
using System.Collections;

public class Serial : MonoBehaviour {
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition;
	private Vector3 syncEndPosition;
	private Quaternion syncStartRotation;
	private Quaternion syncEndRotation;
	Vector3 syncPosition;
	// Use this for initialization
	void Start () {
		if(Network.peerType==NetworkPeerType.Disconnected)
		{
			 this.enabled=false;
		}
		//if(!networkView.isMine) this.enabled=false;
		syncStartPosition = transform.position;
		syncEndPosition = transform.position;
		syncStartRotation=Quaternion.identity;
		syncEndRotation=Quaternion.identity;
	}


	// Update is called once per frame
	void Update()
	{
		if(!networkView.isMine)
		{
			syncTime += Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, syncEndPosition, syncTime / syncDelay);
		transform.rotation = Quaternion.Lerp(transform.rotation, syncEndRotation, syncTime / syncDelay);
			//transform.position = syncEndPosition;
			//transform.rotation = syncEndRotation;
		}
	}
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if(Network.peerType!=NetworkPeerType.Disconnected)
		{
			//if(networkView.isMine)
			//{
				/*if (stream.isWriting)
				{
					Vector3 pos = transform.position;
					Quaternion rot = transform.localRotation;
					stream.Serialize(ref pos);
					stream.Serialize(ref rot);
				}
				else
				{
					Vector3 pos = Vector3.zero;
					Quaternion rot = Quaternion.identity;
					stream.Serialize(ref pos);
					transform.position = pos;
					//transform.position = Vector3.Lerp(transform.position,pos, 0.9f);
					stream.Serialize(ref rot);
					transform.rotation = rot;
				}*/
				if (stream.isWriting) {
					Quaternion rot = transform.rotation;
					syncPosition = transform.position;
					stream.Serialize(ref syncPosition);
					stream.Serialize(ref rot);
				} else {
					Quaternion rot = transform.rotation;
					stream.Serialize(ref syncPosition);
					stream.Serialize(ref rot);
					//transform.rotation = rot;
					
					// Расчеты для интерполяции
					
					// Находим время между текущим моментом и последней интерполяцией
					syncTime = 0f;
					syncDelay = Time.time - lastSynchronizationTime;
					lastSynchronizationTime = Time.time;
					syncStartRotation=transform.rotation;;
					syncEndRotation=rot;
					syncStartPosition = transform.position;
					syncEndPosition = syncPosition;
				/*if(!networkView.isMine)
				{
					syncTime += Time.deltaTime;
					Debug.Log(transform.position+":::::"+syncEndPosition);
					transform.position = Vector3.Lerp(transform.position, syncEndPosition, syncTime / syncDelay);
					transform.rotation = Quaternion.Lerp(transform.rotation, syncEndRotation, syncTime / syncDelay);
					//transform.position = syncEndPosition;
					//transform.rotation = syncEndRotation;
				}*/
				//}
			}
		}
	}
}
