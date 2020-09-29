using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class RocketSkinUpgrade : BaseUpgrade {
		
		public float rocketSkinScalar = 0.8f;
		public float aoeScalar = 0f;
		public override void init()
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(m_damagable)
			{
				m_damagable.aoeScalar = aoeScalar;
				m_damagable.rocketScalar = rocketSkinScalar;	
			}
		}
	}
}