using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class SimpleLerpCamera : MonoBehaviour {
		public Transform target;
		private float m_initalZ;
		private float m_initalY;
		private Vector3 m_initalPos;
		public bool lockZ = false;
		public bool lockX = false;
		public float cameraLerp = 3;
		void Start () {
			m_initalPos = transform.position;
		}
		
		// Update is called once per frame
		void Update () {
			if(target)
			{
				Vector3 pos = target.position;
				if(lockZ)
				{	
					pos.z = m_initalPos.z;
				}
				if(lockX)
				{	
					pos.x = m_initalPos.x;
				}
				pos.y = m_initalPos.y;

				transform.position = Vector3.Lerp(transform.position, pos, cameraLerp * Time.deltaTime);;
			}
		}
	}
}