using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class BaseEnemy : MonoBehaviour {

		protected CharacterController m_controller;
		protected Weapon[] m_guns;
		protected Damagable m_damagable;
		public float attackRange = 40;
		public float moveSpeed = 15;	
		public float gravitySpeed = 50;

		public bool dropsLoot = true;


		public GameObject[] parts;
		public int minParts = 5;
		public int maxParts = 8;

		public float explosionForce = 10000f;
		public float explosionRadius = 100f;

		public void Start()
		{
			m_controller = gameObject.GetComponentInChildren<CharacterController>();
			m_guns = gameObject.GetComponentsInChildren<Weapon>();
			m_damagable = gameObject.GetComponent<Damagable>();

		}
		public void onDeathCBF(){


			BaseGameManager.enemeyDie(gameObject);

		}
		public void OnEnable()
		{
			BaseGameManager.enemySpawn();
		}
		public void OnDisable()
		{
			BaseGameManager.removeEnemy();
		}



		public void killSelf()
		{
			if(m_damagable)
			{
				m_damagable.killSelf();	
			}
		}
		public virtual void handleThink()
		{
		}
		public void Update()
		{
			handleThink();
		}
		public void fireGuns(Vector3 target)
		{
			for(int i=0; i<m_guns.Length; i++)
			{
				target.y = m_guns[i].transform.position.y;
				m_guns[i].transform.LookAt(target);
				m_guns[i].fire( m_guns[i].transform.forward );
			}

		}

		

	}
}
