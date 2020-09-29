using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InaneGames {

	public class FloatingText : MonoBehaviour {
		public float floatSpeed = 10;
		public float destroyTime;
		private Vector3 m_pos;
		private float m_val=0;
		private RectTransform m_rectTransform;
		public float valTime = 4;
		public Text text;
		public Animator animator;
		public float ttl = 2;
		public float waitTime = 1f;

		private Vector3 m_endPos;	
		void Awake () {
			m_pos = transform.position;
			m_rectTransform = transform.GetComponent<RectTransform>();
			Destroy(gameObject,ttl);
		}


		public void init(Vector3 startPos,
		                 Vector3 endPos,
		                 string str,
		                 Color color)
		{

			m_pos = CanvasHelper.WorldToCanvas(startPos);
			m_endPos = CanvasHelper.WorldToCanvas(endPos);
			text.text = str;
			text.color = color;
			m_rectTransform.anchoredPosition = m_pos;
			m_val=0;
		}

		void Update () 
		{
			m_val+=Time.deltaTime;
			float lerpVal = m_val / (waitTime*3f);
			if(m_val>=waitTime)
			{
				m_rectTransform.anchoredPosition = Vector3.Lerp(m_pos,m_endPos,lerpVal);
				//animator.enabled=true;
			}
		}
	}
}
