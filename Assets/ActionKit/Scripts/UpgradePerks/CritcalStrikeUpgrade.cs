using UnityEngine;
using System.Collections;
using InaneGames;

namespace InaneGames {

	public class CritcalStrikeUpgrade : BaseUpgrade
	{
		public float precentChance = 10;

		public override void OnEnable()
		{
			base.OnEnable();

			BaseGameManager.onEnemyHit += onEnemyHit;
			
		}
		
		public override void OnDisable()
		{
			base.OnDisable();
			BaseGameManager.onEnemyHit -= onEnemyHit;
		}
		public void onEnemyHit(GameObject go)
		{
			Damagable dam = go.GetComponentInParent<Damagable>();
			
			float r = Random.Range(0,100);
			if(r<precentChance)
			{
				if(dam)
				{

					dam.critialStrike();
					BaseGameManager.floatText("X2",go,Color.red);

				}
			}
		}
	}
}