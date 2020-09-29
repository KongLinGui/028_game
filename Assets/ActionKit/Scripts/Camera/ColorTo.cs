using UnityEngine;
using System.Collections;

public class ColorTo : MonoBehaviour {
	public Color endColor;
	public Color startColor;
	public UnityEngine.UI.Image image;

	private float m_time;
	public float time=  1;

	private bool m_on = false;

	public void turnOn()
	{
		m_on = true;
		m_time=0;
	}

	void Update () {

		if(m_on){
			float lerpTime = m_time / time;
			if(image)	
			{
				image.color = Color.Lerp(startColor,endColor,lerpTime);
			}

			if(lerpTime>1)
			{
				m_on=false;
			}
			m_time+=Time.deltaTime;
		}
	}
}
