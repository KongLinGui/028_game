using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace InaneGames {

	public class PackageManager	 : MonoBehaviour 
	{
		//a refence to the player
		private Transform m_player;

		//the name of the player gameObject
		public string playerName = "Player";

		//a list of packages
		public GameObject [] packages;


		//the listpicker
		private ListPicker m_picker;

		//the clip to play when spawning a package
		public AudioClip clip;

		//a reference to the audiosource
		private AudioSource m_audio;

		//the number of enemies to kill before spawning a package
		public int enemiesToKill=20;

		private int m_enemiesToKill ;

		//the distance from the player to spawn the package
		public Vector3 startOffset = new Vector3(40,100,0);

		//the distance from the player to move the package to
		public Vector3 endOffset = new Vector3(40,6,0);

		void Awake()
		{
			m_audio = gameObject.GetComponent<AudioSource>();
			m_enemiesToKill=enemiesToKill;	
			m_picker = new ListPicker(packages.Length);

			GameObject go = GameObject.Find(playerName);
			if(go)
			{
				m_player = go.	transform;
			}
			createPackage();

		}
		public void OnEnable()
		{
			BaseGameManager.onEnemyDie += onEnemyDie;
		}
		public void OnDisable()
		{
			BaseGameManager.onEnemyDie -= onEnemyDie;
		}
		void onEnemyDie(GameObject go){
			m_enemiesToKill--;
			if(m_enemiesToKill<0)
			{
				createPackage();
				m_enemiesToKill=enemiesToKill;
			}
		}

		void createPackage()
		{
			if(m_player)
			{
				int n = m_picker.pickRandomIndex();	
				m_audio.PlayOneShot(clip);
				GameObject go = (GameObject)Instantiate( packages[n],
				                                        m_player.position+startOffset,
				                      Quaternion.identity);
				if(go)
				{
	#if ITWEEN
					iTween.MoveTo(go,m_player.position+endOffset,1f);
	#endif
				}
			}
		}

	}
}
