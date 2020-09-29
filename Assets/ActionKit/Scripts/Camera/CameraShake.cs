using UnityEngine;
using System.Collections;

using System.Collections.Generic;
namespace InaneGames {

	public class CameraShake : MonoBehaviour {

		private bool m_shake=false	; 
		private float m_shakeIntensity;    
		private Vector3 m_orgPos;
		private Quaternion m_orgRot;

		public float shakeIntensity = 0.3f;
		public float shakeDecay = 2;
		void Start()
		{
			m_shake = false;  


		}
		
		
		void Update () 
		{
			if(m_shakeIntensity > 0)
			{
				transform.localPosition = m_orgPos + Random.insideUnitSphere * m_shakeIntensity;
				transform.localRotation = new Quaternion(m_orgRot.x + Random.Range(-m_shakeIntensity, m_shakeIntensity)*.2f,
				                                    m_orgRot.y + Random.Range(-m_shakeIntensity, m_shakeIntensity)*.2f,
				                                    m_orgRot.z + Random.Range(-m_shakeIntensity, m_shakeIntensity)*.2f,
				                                    m_orgRot.w + Random.Range(-m_shakeIntensity, m_shakeIntensity)*.2f);
				
				m_shakeIntensity -= Time.deltaTime*shakeDecay;
			}
			else
			{
				if (m_shake)
				{
					transform.localPosition = m_orgPos;
					transform.localRotation		 = m_orgRot;
					m_shake = false;  
				}
			}
		}
		


		public void shake()
		{
			if(m_shake==false)
			{
				m_orgPos = transform.localPosition;
				m_orgRot = transform.localRotation	;
				m_shakeIntensity = shakeIntensity;
				m_shake = true;
			}
		}    
		
		
	}
}
