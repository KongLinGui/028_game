//#define ITWEEN



using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class LightChanger : MonoBehaviour {
		public Color initalColor = Color.blue;
		public Color bossColor = Color.red;

		public Color victoryColor = Color.white;
		public GameObject rainSystem;

		private bool m_bossRoom=false;
		void OnEnable()
		{
			BaseGameManager.onLoadNextLevel += onNextLoadLevel;
			BaseGameManager.onEnemiesCleared += onEnemiesCleared;
		}

		void OnDisable()
		{
			BaseGameManager.onLoadNextLevel -= onNextLoadLevel;
			BaseGameManager.onEnemiesCleared -= onEnemiesCleared;
		}
		public void changeColor(Color color)
		{
			initalColor = color;
		}
		public void onNextLoadLevel()
		{
			if(m_bossRoom==false)
			{
#if ITWEEN
				iTween.ColorTo(gameObject,initalColor,1f);
#endif
				if(rainSystem){	
					rainSystem.SetActive(true);
				}
			}
		}
		public void onEnemiesCleared()
		{
			#if ITWEEN

			iTween.ColorTo(gameObject,victoryColor,1f);
#endif
			if(rainSystem){	
				rainSystem.SetActive(false);
			}
		}
	}
}