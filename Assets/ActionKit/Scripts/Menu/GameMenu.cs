using UnityEngine;
using System.Collections;
namespace InaneGames
	{
	public class GameMenu : MonoBehaviour {
		public GameObject pauseMenu;
		public GameObject gamePanel;
		public GameObject gameOverPanel;
		private bool m_gameOver=false;
		public float gameOverPanelWaitTime = 1.25f;
		public float unpauseGameWaitTime = 1.25f;
		public void OnEnable()
		{
			BaseGameManager.onPlayerDie += onPlayerDie;
			BaseGameManager.onGameOver += onGameOver;
		}
		public void OnDisable()
		{
			BaseGameManager.onPlayerDie -= onPlayerDie;
			BaseGameManager.onGameOver -= onGameOver;

		}
		public void onQuit()
		{
			Application.Quit();	
		}
		void onGameOver(bool vic)
		{
			Debug.Log ("onGameOver" + vic);
			if(gameOverPanel)
			{
				gameOverPanel.SetActive(true);
				if(vic)
				{
					if(gamePanel)
					{
						gamePanel.SetActive(false);
					}
					StartCoroutine(showGameOverPanelIE(gameOverPanelWaitTime));

				}
			}
		}


		void onPlayerDie()
		{
			m_gameOver=true;
			gamePanel.SetActive(false);
			StartCoroutine(showGameOverPanelIE(gameOverPanelWaitTime));

		}
		IEnumerator showGameOverPanelIE(float waitTime){
			yield return new WaitForSeconds(waitTime);
			Constants.fadeInFadeOut(gameOverPanel,null);

		}

		IEnumerator unpauseGameIE(float waitTime){
			yield return new WaitForSeconds(waitTime);
			Time.timeScale = 1;

				
		}
		public void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape) && m_gameOver==false && Time.timeScale!=0)
			{

				Time.timeScale = 0;
				Constants.fadeInFadeOut(pauseMenu,null);
				gamePanel.SetActive(false);
				pauseMenu.SetActive(true);
			}
		}

		public void onCommand(string str)
		{
			
			if(str.Equals("Restart"))
			{
				Destroy(GameObject.Find("Player"));
				BaseGameManager.setNomEnemies(0);

				Time.timeScale = 1;
				Application.LoadLevel(0);
			}
			
			if(str.Equals("Unpause"))
			{
				Time.timeScale = 1;
				Constants.fadeInFadeOut(null,pauseMenu);
				gamePanel.SetActive(true);


			}
			if(str.Equals("MainMenu"))
			{
				Destroy(GameObject.Find("Player"));

				Time.timeScale = 1;
				Application.LoadLevel(0);

			}
			
		}
	}
}