using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class SlowDownTime : BaseSpell {
		GrayscaleEffect m_grayscaleEffect;
		public static float SLOW_DOWN_TIME_SCALAR = 1f;
		private Music m_music;
		public float slowTime = 4f;
		public float slowMusicPitch = 0.25f;

		public override	 void Start()
		{
			base.Start();
			m_spellID = SpellID.SlowDownTime;
			m_music =  (Music)GameObject.FindObjectOfType(typeof(Music));
			m_grayscaleEffect =  (GrayscaleEffect)GameObject.FindObjectOfType(typeof(GrayscaleEffect));
			if(m_grayscaleEffect)
			{
				m_grayscaleEffect.enabled=false;
			}

			SlowDownTime.SLOW_DOWN_TIME_SCALAR = 1f;
		}

		public override void spellEnd()
		{
			if(m_grayscaleEffect)
			{
				m_grayscaleEffect.enabled=false;
			}
			SLOW_DOWN_TIME_SCALAR = 1;	

			if(m_music)
				m_music.setPitch(1f);

		}
		
		public override void spellStart()
		{
			if(m_grayscaleEffect)
			{
				m_grayscaleEffect.enabled=true;
			}
			if(m_music)
				m_music.setPitch(slowMusicPitch);


			SLOW_DOWN_TIME_SCALAR = 0f;	
			StartCoroutine(slowDownTimeIE(slowTime));

		}

		
		IEnumerator slowDownTimeIE(float ttl)
		{
			SpellManager.spellStart(m_spellID);
			yield return new WaitForSeconds(ttl);
			SpellManager.spellEnd(m_spellID);
			spellEnd();
		}



	}
}