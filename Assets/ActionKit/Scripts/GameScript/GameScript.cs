using UnityEngine;
using System.Collections;
using InaneGames;
using UnityEngine.UI;

namespace InaneGames {

	public class GameScript : MonoBehaviour {
		private Image m_fadeOutImage;
		private float m_fadeTime;	

		public bool fadeOutOnGameOver=true;

		public AudioClip portalAC;
		public AudioClip enemyDieAC;
		private AudioSource m_audioSource;
		public GameObject fireWorks;

		public void Start()
		{
			BaseGameManager.setNomEnemies(0);

		}
		public void Awake()
		{
			m_audioSource = gameObject.GetComponent<AudioSource>();
			BaseGameManager.setNomEnemies(0);
			Rocket.ENEMY_BULLET_SCALAR = 1;
			GameObject	 go = GameObject.Find("FadeOut");
			Time.timeScale=1;

			if(go)
			{
				m_fadeOutImage = go.GetComponent<Image>();
			}
		}
		IEnumerator fadeOutIE()
		{
			yield return new WaitForSeconds(0.1f);
			m_fadeTime+=0.1f;

			Color color = Color.black;
			color.a = Mathf.Lerp(0,1,m_fadeTime);
			m_fadeOutImage.color = color;
			if(m_fadeTime<1)
			{
				StartCoroutine(fadeOutIE());
			}
		}
		public void OnEnable()
		{
			BaseGameManager.onSuckedIntoPortal += onSuckedIntoPortal;
			BaseGameManager.onPlayerDie  += onPlayerDie;
			BaseGameManager.onEnemyDie += onEnemyDie;
			BaseGameManager.onPickUpPowerup += onPickUpPowerup;
		}
		
		public void OnDisable()
		{
			BaseGameManager.onPlayerDie	 	-= onPlayerDie;
			BaseGameManager.onEnemyDie -= onEnemyDie;
			BaseGameManager.onSuckedIntoPortal -= onSuckedIntoPortal;

			BaseGameManager.onPickUpPowerup -= onPickUpPowerup;
		}
		void onSuckedIntoPortal(GameObject go)
		{
			if(fireWorks)
			{	
				Instantiate(fireWorks,go.transform.position,Quaternion.identity);
			}
			if(m_audioSource)
			{
				m_audioSource.PlayOneShot(portalAC);
			}
		}
		void onEnemyDie(GameObject go)
		{
			if(m_audioSource)
			{
				m_audioSource.PlayOneShot(enemyDieAC);
			}
		}

		void onPlayerDie()
		{
			Cursor.lockState = CursorLockMode.None;
			if(fadeOutOnGameOver)
				StartCoroutine(fadeOutIE());

		}
		public void onPickUpPowerup(BasePowerup pow)
		{
			if(pow)
			{
				GetComponent<AudioSource>().PlayOneShot(pow.pickupAC);
			}
		}


	}
}
