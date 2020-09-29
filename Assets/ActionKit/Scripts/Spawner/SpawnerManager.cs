using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class SpawnerManager : MonoBehaviour {
		public SimpleSpawner[] spawners;
		public void OnEnable()
		{
			BaseGameManager.onLoadNextLevel+=onLoadNextLevel;
			BaseGameManager.onEnemiesCleared+=onEnemiesCleared;

		}
		public void OnDisable()
		{
			BaseGameManager.onLoadNextLevel-=onLoadNextLevel;
			BaseGameManager.onEnemiesCleared-=onEnemiesCleared;

		}
		void onLoadNextLevel()
		{
			StartCoroutine(enableSpawnersDelayed());
		}
		void onEnemiesCleared()
		{
			disableSpawners();
		}

		void disableSpawners()
		{
			for(int i=0; i<spawners.Length; i++)
			{
				spawners[i].on=false;
			}
		}


		IEnumerator enableSpawnersDelayed()
		{
			yield return new WaitForSeconds(2);
			for(int i=0; i<spawners.Length; i++)
			{
				spawners[i].on=true;
				spawners[i].reset();
			}
		}

	}
}