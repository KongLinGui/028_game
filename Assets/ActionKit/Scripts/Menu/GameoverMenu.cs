using UnityEngine;
using System.Collections;
using System;
namespace InaneGames {

	public class GameoverMenu : MonoBehaviour {
		public UnityEngine.UI.Text gameOverTimeText;
		public UnityEngine.UI.Text killsText;
		public GameObject gameOverPanel;
		public void OnDisable()
		{
			BaseGameManager.onGameOver -= onGameOver;
			BaseGameManager.onPlayerDie -= onPlayerDie;

		}
		public void OnEnable()
		{
			BaseGameManager.onGameOver += onGameOver;
			BaseGameManager.onPlayerDie += onPlayerDie;
		}
		void onPlayerDie()
		{
			StartCoroutine(onPlayerDieIE(1.5f));

		}
		void onGameOver(bool vic)
		{
			StartCoroutine(onPlayerDieIE(0));
		}
		public void onRestart()
		{
			BaseGameManager.setNomEnemies(0);

			Application.LoadLevel(0);	
		}
		IEnumerator onPlayerDieIE(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			updateText();
			if(gameOverPanel)
			{
				gameOverPanel.SetActive(true);
			}
		}

		void updateText () {
			int time0 = (int)Time.timeSinceLevelLoad;
			string minSec = string.Format("{0}:{1:00}", time0 / 60, time0 % 60); 

			if(gameOverTimeText)
			{
				gameOverTimeText.text = "Time:" + minSec;
			}

			if(killsText)
			{	
				killsText.text = "Kills:" + BaseGameManager.getNomKilled();
			}
		}

	}
}
