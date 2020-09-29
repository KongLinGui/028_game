using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class ManaRegenUpgrade : BaseUpgrade	 {
		public float manaRegen = 1f;
		public override void init()
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(m_damagable)
			{
				m_damagable.manaRegen += manaRegen;
			}
		}
	}
}