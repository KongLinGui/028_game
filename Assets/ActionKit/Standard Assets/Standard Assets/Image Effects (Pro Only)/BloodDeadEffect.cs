using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
public class BloodDeadEffect : ImageEffectBase {

	public enum State
	{
		IDLE,
		STROBE,
		PERMA_STROBE
	};
	public State m_state;


	public float bloodTime = 1f;
	public Color color = Color.cyan;

	private static float STROBE_TIME = 0.125f * 0.75f;
	private float m_strobeTime = 0;

	private float m_offTime = STROBE_TIME;
	private float m_onTime = STROBE_TIME;
	private bool m_permaStrobe = false;

	public void strobe(int nomTicks,float strobeScalar,bool permaStrobe)
	{
		m_state = State.STROBE;
		m_onTime=STROBE_TIME;
		m_strobeTime = STROBE_TIME * nomTicks;
		m_permaStrobe = permaStrobe;
		bloodTime=0;

	}
	public void Update()
	{
		m_onTime-=Time.deltaTime;
		m_strobeTime-=Time.deltaTime;
		m_offTime-=Time.deltaTime;

		if(m_strobeTime<0 && m_permaStrobe==false)
		{
			m_state = State.IDLE;
		}
		if(m_offTime<0)
		{
			m_onTime=STROBE_TIME;
		}



	}
	void OnRenderImage (RenderTexture source, RenderTexture destination) 
	{

		if( m_state == State.STROBE && m_onTime>0 )
		{
			material.SetFloat("_bloodLine", bloodTime);
			material.SetColor("_color", color);
			m_offTime=STROBE_TIME;
			Graphics.Blit (source, destination, material);
		}else{
			Color col = Color.white;
			col.a=0;
			material.SetFloat("_bloodLine", 1);
			material.SetColor("_color", col);
			Graphics.Blit (source, destination, material);
		}
	}
}