using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class HealthBoostUpgrade : BaseUpgrade {
		public float healthVal = 25;
		public override void init()
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(m_damagable)
			{
				m_damagable.increaseMaxHealth(healthVal);



			}
		}
	}
}