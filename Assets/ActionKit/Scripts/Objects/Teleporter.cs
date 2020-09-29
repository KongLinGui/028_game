using UnityEngine;
using System.Collections;
namespace InaneGames {

public class Teleporter : MonoBehaviour {
	private bool m_loadChunk = false;

	//the current field renderer
	public Renderer fieldRenderer;
	//the disabled field material
	public Material fieldMAT;
	//the original field mateiral
	private Material m_orgFieldMat;
	
	//the poles render
	public Renderer polesRenderer;
		//the disabled poles material
	public Material polesMAT;
	//the original poles material
	private Material m_orgPolesMAT;
	
	//have the enemies been cleared
	private static bool m_enemiesCleared=false;

	//the time to load the next level
	private float m_nextLevelTime;
	private float nextLevelTime = 2;

	private bool m_enabled=true;

	public void Awake()
	{
		m_orgFieldMat = fieldRenderer.material;
		m_orgPolesMAT = fieldRenderer.material;

	}

	public void OnEnable()
	{
		BaseGameManager.onLoadNextLevel+=onLoadNextLevel;

	}

	public void Disable()
	{
		BaseGameManager.onLoadNextLevel-=onLoadNextLevel;
	}
		public void disabled()
		{
			m_enabled = false;
			fieldRenderer.material = m_orgFieldMat;
			polesRenderer.material = m_orgPolesMAT;
		}
	public void onLoadNextLevel()
	{
		m_enemiesCleared	=false;
		m_nextLevelTime=nextLevelTime;
	}
	public void OnTriggerEnter(Collider col)
	{
		if(col.name.Equals("Player"))
		{
			int enemyCount = getEnemyCount();

			if(enemyCount ==0  && m_loadChunk==false && m_enabled)
			{
				if(gameObject.GetComponent<AudioSource>())
				{
					gameObject.GetComponent<AudioSource>().Play();
				}

				gameObject.SendMessage("loadNextChunkCBF",SendMessageOptions.DontRequireReceiver);
				BaseGameManager.loadNextLevel();
				m_loadChunk=true;
				
			}
		}
	}
	public void Update()
	{
		int enemyCount = getEnemyCount();
		m_nextLevelTime -= Time.deltaTime;
		if(enemyCount==0 && m_nextLevelTime<0 && m_enabled)
		{
			fieldRenderer.material = fieldMAT;
			polesRenderer.material = polesMAT;
			if(m_enemiesCleared==false)
			{
				BaseGameManager.enemiesCleared();
				m_enemiesCleared=true;

			}
		}
	}
	public int getEnemyCount()
	{
		TurretEnemy[] turrets = (TurretEnemy[])GameObject.FindObjectsOfType(typeof(TurretEnemy));
		int aliveCount = 0;
		aliveCount = turrets.Length;
		SimpleSpawner[] spawners = (SimpleSpawner[])GameObject.FindObjectsOfType(typeof(SimpleSpawner));
		for(int i=0; i<spawners.Length; i++)
		{
			if(spawners[i].isFactory)
			{
				aliveCount++;
			}
		}


		ZombieEnemy[] zombies = (ZombieEnemy[])GameObject.FindObjectsOfType(typeof(ZombieEnemy));
		for(int i=0; i<zombies.Length; i++)
		{
			if(zombies[i].isAlive())
			{
				aliveCount++;
			}
		}
		return aliveCount;
	}


}
}
