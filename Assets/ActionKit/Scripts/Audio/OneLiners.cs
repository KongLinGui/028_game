using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class OneLiners : MonoBehaviour {

	
		public AudioClip[] onHitAC;
		public AudioClip[] onDieAC;
		public AudioClip[] onEnemyKillOneLiners	;


		private float m_hitTime;
		public void OnEnable()
		{
			BaseGameManager.onEnemyDie	 += onEnemyDie	;
		}
		public void OnDisable()
		{
			BaseGameManager.onEnemyDie 	-= onEnemyDie;
		}

		void Update()
		{
			m_hitTime-=Time.deltaTime;
		}
		
		void onEnemyDie(GameObject go)
		{
			playRandomClip(onEnemyKillOneLiners);
		}
		void onUpgrade(int i)
		{
			//	playRandomClip(onEnemyKillOneLiners);
		}
		void onHitCBF(float dmg)
		{
			if(m_hitTime<0)
			{
				m_hitTime = playRandomClip(onHitAC);
			}
		}
		void onDeathCBF()
		{
			playRandomClip(onDieAC);
		}


		float playRandomClip(AudioClip[] clips)
		{
			float len = 0;
			if(clips!=null && clips.Length>0)
			{
				int r = Random.Range(0,clips.Length);
				if(GetComponent<AudioSource>()){
					GetComponent<AudioSource>().PlayOneShot(clips	[r]);
				}
				if(clips.Length > 0 && clips[r])
				{
					len = clips[r].length	;
				}
			}
			return len;
		}

	}
}
