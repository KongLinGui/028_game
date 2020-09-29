using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class SimpleSpawner : MonoBehaviour {
		/// <summary>
		/// The objects to spawn.
		/// </summary>
		public GameObject[] objectsToSpawn;

		private int m_enemyIndex;

		/// <summary>
		/// The minimum to spawn.
		/// </summary>
		public int minToSpawn = 3;
		/// <summary>
		/// The max to spawn.
		/// </summary>
		public int maxToSpawn = 5;
		private int m_toSpawn;
		
		private bool m_finishedRound = false;
		public enum State
		{
			SPAWN,
			COOLDOWN,
			RELOAD
		};

		public State m_state;
		/// <summary>
		/// The reload time.
		/// </summary>
		public float reloadTime = 2f;
		
		private float m_reloadTime;
		
		public float spawnRange = 45f;
		/// <summary>
		/// The cooldown time.
		/// </summary>
		public float cooldownTime = 2f;
		private float m_cooldownTime;
		public int zombiesToSpawn = 5;
		public bool unlimitedZombies = true;

		private Transform m_playerTransform;

		public enum SpawnerType
		{
			POINT,
			PLAYER_OFFSET
		};
		public bool isFactory=false;
		public SpawnerType spawnerType;
		public bool on = true;

		public Vector2 boxSize;
		public Vector2 spawnerExtentsZ;
		public Vector2 spawnerExtentsX;

		public enum SpawnHow
		{
			SEQUENTIAL,
			CONTROLLED_RANDOM
		};
		public SpawnHow spawnHow;
		private ListPicker m_picker;
		public void Start()
		{
			reset();
			m_picker = new ListPicker(objectsToSpawn.Length);
		}
		public void OnEnable()
		{
			BaseGameManager.onGameOver += onGameOver;
		}
		
		public void OnDisable()
		{
			BaseGameManager.onGameOver -= onGameOver;
		}
		void onGameOver(bool vic)
		{
			on=false;
		}
		public void reset()
		{
	//		Debug.Log ("reset");
			m_state = State.SPAWN;
			m_enemyIndex=0;
			m_finishedRound = false;
			m_toSpawn = Random.Range(minToSpawn,maxToSpawn);
			m_cooldownTime = cooldownTime;
			m_reloadTime = reloadTime;
		
		}
		public void Update()
		{
			GameObject go = GameObject.Find("Player");
			if(go)
			{
				m_playerTransform = go.transform;
			}	
			
			if(m_playerTransform && on)
			{
				float d0 = (m_playerTransform.transform.position - transform.position).magnitude;
				if(d0 < spawnRange)
				{
					update (Time.deltaTime);
				}
			}
		}
		public void update(float dt)	{
			
			switch(m_state)
			{
				case State.COOLDOWN:
					handleCooldown(dt);
				break;
				case State.RELOAD:
					handleReload(dt);
				break;
				case State.SPAWN:
					handleSpawn();
				break;
			}
		}
		public void handleCooldown(float dt)
		{
			m_cooldownTime -= dt;
			if(m_cooldownTime<0 )
			{
				m_state = State.SPAWN;
			}
		}
		public void handleReload(float dt)
		{
			m_reloadTime -= dt;
			if(m_reloadTime<0 )
			{
				m_state = State.SPAWN;
				m_toSpawn = Random.Range(minToSpawn,maxToSpawn);
			}
		}
		public virtual void spawn(GameObject go)
		{		
			zombiesToSpawn--;
			if(unlimitedZombies || zombiesToSpawn>0)
			{
				Vector3 spawnPos = transform.position;
				if(spawnerType==SpawnerType.PLAYER_OFFSET){
					spawnPos = m_playerTransform.position;
					spawnPos.z += boxSize.x;
					spawnPos.x -= boxSize.y;

					spawnPos.z = Mathf.Clamp(spawnPos.z,spawnerExtentsZ.x,spawnerExtentsZ.y);
					spawnPos.x = Mathf.Clamp(spawnPos.x,spawnerExtentsX.x,spawnerExtentsX.y);

				}

				GameObject newObject = (GameObject)Instantiate(go,spawnPos,Quaternion.identity);
				GameObject roomGO = GameObject.Find("Room");
				if(newObject && roomGO	)
				{
					newObject.transform.parent = roomGO.transform;
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
		public void handleSpawn()
		{

			if(m_toSpawn > -1)
			{
				if(spawnHow==SpawnHow.CONTROLLED_RANDOM)
				{
					m_enemyIndex = m_picker.pickRandomIndex();
				}
				GameObject enemyPrefab = objectsToSpawn[m_enemyIndex];
				if(enemyPrefab)
				{
					m_toSpawn--;
					
					m_enemyIndex++;
					if(m_enemyIndex >= objectsToSpawn.Length)
					{
						m_enemyIndex=0;
					}
	//				Debug.Log ("spawn" + m_enemyIndex);			
					
					 spawn(enemyPrefab);
					//onPostObjectSpawn(newObject);
					m_state = State.COOLDOWN;
					m_cooldownTime = cooldownTime;				
				}
			}else{
				m_state = State.RELOAD;
				m_reloadTime = reloadTime;
			}
		}
		public void nextWave()
		{
			if(m_toSpawn<0)
			{
				m_finishedRound = true;
			}
			
		}
		
		public bool isFinished()
		{
			return m_finishedRound;
		}
	}
}
