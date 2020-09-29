//#define ITWEEN



using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class ZombieEnemy : MonoBehaviour {

		protected Animator m_animator;
		public float hitTime = 0.1f;
		protected float m_hitTime;
		public int exp = 10;

		public enum Mode
		{
			WAIT_AROUND,
			ATTACK_PLAYER,
			STUNNED,
			DEAD
		};
		protected Mode m_mode;
		protected GameObject m_target;

		public float attackRange = 10;

		public float attackTime = 0.1f;
		protected float m_attackTime;

		public float speed = 26;
		public float acceleration = 32;

		public float attackDamage = 5;

		protected bool m_dead=false;

		protected UnityEngine.AI.NavMeshAgent m_agent;
		public float attackRotOffset = 0;
		public bool explodeOnAttack =false;
		public GameObject bloodSplater;
		protected Damagable m_damagable;

		public void Start()
		{
			m_damagable = gameObject.GetComponent<Damagable>();
			m_animator = gameObject.GetComponentInChildren	<Animator>();
			m_target = GameObject.Find("Player");
			Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();
			for(int i=0; i<renders.Length; i++)
			{
				renders[i].gameObject.AddComponent<FrozenMaterialChanger>();
			}
			m_agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
			m_agent.stoppingDistance = attackRange;
			m_agent.speed = speed;
			m_agent.acceleration = acceleration;

			m_mode = Mode.ATTACK_PLAYER;
			init();
		}
		public virtual void init()
		{
		}
		public void OnEnable()
		{
			BaseGameManager.onGameOver += onGameOver;
			BaseGameManager.onEnemiesCleared += onEnemiesCleared;
		}

		public void OnDisable()
		{
			BaseGameManager.onGameOver -= onGameOver;
			BaseGameManager.onEnemiesCleared -= onEnemiesCleared;

		}
		public void onEnemiesCleared()
		{
		}
		public void onGameOver(bool vic)
		{
			m_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
			m_animator.SetBool("Cheer",true);
			m_agent.enabled=false;
			transform.rotation = Quaternion.AngleAxis(180,Vector3.up);
		}
		public void Update()
		{

			handlePause();
			if(SlowDownTime.SLOW_DOWN_TIME_SCALAR!=0 && m_damagable.isAlive() && m_target && m_target.GetComponent<Damagable>().isAlive())
			{
				updateUnit();
				updateTimers();

				if(m_mode==Mode.ATTACK_PLAYER)
				{
					handleAttackPlayer();
				}
				if(m_mode==Mode.STUNNED)
				{
					updateStunned();
				}
			}	


		}
		public virtual void updateUnit()
		{
		}
		public virtual void handleAttack()
		{
			
			if(explodeOnAttack)
			{
				CameraShake cs = (CameraShake)GameObject.FindObjectOfType(typeof(CameraShake));
				if(cs)
				{
					cs.shake();
				}
				m_damagable.killSelf();
			}
			if(m_animator)
			{
				m_animator.SetBool("Attack",true);
			}
			if(m_agent && m_agent.isOnNavMesh)
			{
				m_agent.Stop();
			}
			Damagable dam = m_target.GetComponent<Damagable>();
			if(dam){
				dam.damage(attackDamage,Damagable.DamagableType.MELEE);
			}
			m_attackTime = attackTime;
		}
		public bool isAttacking()
		{
			return m_mode==Mode.ATTACK_PLAYER;
		}
		public void attack()
		{
			m_mode = Mode.ATTACK_PLAYER;
		}
		void handlePause()
		{
			if(SlowDownTime.SLOW_DOWN_TIME_SCALAR==0)
			{
				stop();
				if(m_animator)
				{
					m_animator.speed=0.001f;
				}
			}else{
				if(m_animator)
				{
					m_animator.speed=1;
				}
				if(m_damagable.isAlive())
				{
					if(m_agent)
					{
						m_agent.enabled=true;
						if(m_agent.isOnNavMesh)
						{
							m_agent.Resume();
						}
					}
				}
			}
		}
		public virtual void handleAttackPlayer()
		{
			if(m_target && m_attackTime<0 && m_hitTime<0)
			{
				Vector3 vec = m_target.transform.position - transform.position;

				float d0 = vec.magnitude;

				if(m_target.transform.position.	x > transform.position.x)
				{
					transform.rotation = Quaternion.AngleAxis(90,Vector3.up);
				}else{
					transform.rotation = Quaternion.AngleAxis(-90,Vector3.up);

				}
				if(d0 > attackRange)
				{
					if(m_attackTime<0 && m_hitTime<0)
					{

						m_animator.SetBool("Walk",true);
						m_animator.SetBool("Attack",false);


						if(m_agent && m_agent.isOnNavMesh)
						{
							m_agent.Resume();

							m_agent.SetDestination(m_target.transform.position);
						}else
						{
	//						putOnNavMesh(m_target.transform.position);
	//						Debug.Log ("Put On Path");
						}
					}
				}else{
					handleAttack();
				}

			}
		}
		void putOnNavMesh(Vector3 sourcePos)
		{
			UnityEngine.AI.NavMeshHit closestHit;
			if( UnityEngine.AI.NavMesh.SamplePosition(  sourcePos, out closestHit, 500, 1 ) )
			{
				transform.position = closestHit.position;
			}
		}
		void updateTimers()
		{
			m_hitTime -= Time.deltaTime;
			m_attackTime -=Time.deltaTime;
		}

		void updateStunned()
		{
			if(m_hitTime<0 && m_dead==false)
			{
				m_agent.enabled=true;
				m_agent.Resume();
				m_animator.SetBool("Hit",false);
				m_mode = Mode.ATTACK_PLAYER;
			}
		}
		void stop()
		{
			if(m_agent && m_agent.isOnNavMesh)
			{
				m_agent.Stop();
			}
			if(m_agent)
			{
				m_agent.velocity = Vector3.zero;
				m_agent.angularSpeed=0;
				m_agent.enabled=false;
			}
		}

		public virtual void onDeathCBF()
		{
			if(gameObject.GetComponent<CapsuleCollider>())
			{
				gameObject.GetComponent<CapsuleCollider>().enabled=false;
			}
			#if ITWEEN
				gameObject.AddComponent<FlyToPortal>();
			#endif
			BaseGameManager.removeEnemy();

			m_mode = Mode.DEAD;
			BaseGameManager.enemeyDie(gameObject);
			stop();
			//make our blood splatter add it to the game object called "Room"
			Vector3 bloodPos = transform.position;
			bloodPos.y = 1f;

			if(bloodSplater)
			{
				GameObject go = (GameObject)Instantiate(bloodSplater,bloodPos,Quaternion.identity);
				if(go)
				{
					GameObject roomGO = GameObject.Find("Room");
					if(roomGO)
					{	
						go.transform.parent = roomGO.transform;
					}
				}
			}
			if(m_animator)
			{
				m_animator.SetBool("Die",true);
			}
			m_dead=true;	
		}
		void onHitCBF(float dmg)
		{
			if(hitTime>0)
			{
				BaseGameManager.enemyHit(gameObject	);
				if(m_animator)
				{
					m_animator.SetBool("Hit",true);
				}
				stop();

				float knockBack =m_damagable.getKnockBackForce();

				if(knockBack>0)
				{

#if ITWEEN
					Vector3 vec = new Vector3(1,0,0	);
					if(m_target.transform.position.x > transform.position.x)
					{
						vec = new Vector3(-1	,0,0);
					}
					iTween.MoveTo(gameObject,transform.position + vec.normalized*knockBack	,0.5f);
#endif
				}
				//m_agent.velocity =vec.normalized * moveSpeed*5f;

				m_agent.enabled=false;
				m_mode = Mode.STUNNED;	
				m_hitTime = hitTime;
			}
		}
		public bool isAlive()
		{
			return m_dead==false;
		}
	}
}
