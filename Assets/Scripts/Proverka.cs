using UnityEngine;
using System.Collections;


	
	[RequireComponent(typeof(NetworkView))]
	public class Proverka : MonoBehaviour {
		
		private Transform tTransform = null;
		private NetworkView nNetworkView = null;
		
		private void Awake(){
			
			tTransform = transform;
			nNetworkView = networkView;
		}
		
		private double m_InterpolationBackTime = 0.15;
		
		internal struct State{
			
			internal double timestamp;
			internal Vector3 pos;
			internal Quaternion rot;
		}
		
		State[] m_BufferedState = new State[5];
		int m_TimestampCount;
		
		void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
			
			if(stream.isWriting){
				
				Vector3 pos = tTransform.position;
				Quaternion rot = tTransform.rotation;
				
				stream.Serialize(ref pos);
				stream.Serialize(ref rot);
			}
			else{
				
				Vector3 pos = Vector3.zero;
				Quaternion rot = Quaternion.identity;
				stream.Serialize(ref pos);
				stream.Serialize(ref rot);
				
				for (int i=m_BufferedState.Length-1;i>=1;i--)
				{
					m_BufferedState[i] = m_BufferedState[i-1];
				}
				
				State state;
				state.timestamp = info.timestamp;
				state.pos = pos;
				state.rot = rot;
				m_BufferedState[0] = state;
				
				m_TimestampCount = Mathf.Min(m_TimestampCount + 1, m_BufferedState.Length);
				
				for (int i=0;i<m_TimestampCount-1;i++)
				{
					if (m_BufferedState[i].timestamp < m_BufferedState[i+1].timestamp)
						Debug.Log("State inconsistent");
				}       
			}
		}
		
		void Update(){
			
			if(!nNetworkView.isMine){
				
				double interpolationTime = Network.time - m_InterpolationBackTime;
				
				if(m_BufferedState[0].timestamp > interpolationTime){
					
					for (int i=0;i<m_TimestampCount;i++){
						
						if(m_BufferedState[i].timestamp <= interpolationTime || i == m_TimestampCount-1){
							
							State rhs = m_BufferedState[Mathf.Max(i-1, 0)];
							State lhs = m_BufferedState[i];
							
							double length = rhs.timestamp - lhs.timestamp;
							float t = 0.0f;
							
							if(length > 0.0001f){
								
								t = (float)((interpolationTime - lhs.timestamp) / length);
							}
							
							tTransform.position = Vector3.Lerp(lhs.pos, rhs.pos, t);
							tTransform.rotation = Quaternion.Slerp(lhs.rot, rhs.rot, t);
							
							return;
						}
					}
				}
			}
		}
	}

