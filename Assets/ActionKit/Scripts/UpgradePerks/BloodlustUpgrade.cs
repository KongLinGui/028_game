using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class BloodlustUpgrade : BaseUpgrade {
		public float healAmmount = 5;
		public float precentChanceToHeal = 10;
		public override void OnEnable()
		{		
			base.OnEnable();
			BaseGameManager.onEnemyDie += onEnemyDie;
		}
		public override void OnDisable()
		{
			base.OnDisable();
			BaseGameManager.onEnemyDie -= onEnemyDie;
		}

		public void onEnemyDie(GameObject go)
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();

			float r = Random.Range(0,100);
			if(r<precentChanceToHeal)
			{
				if(m_damagable)
				{
					m_damagable.heal(healAmmount);
					int iHeal = (int)healAmmount;
					BaseGameManager.floatText("Lifesteal + "+iHeal.ToString(),go,Color.green);
				}
			}
		}
	}
}