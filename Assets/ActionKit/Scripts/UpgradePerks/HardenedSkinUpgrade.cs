using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class HardenedSkinUpgrade : BaseUpgrade {
		
		public float rocketSkinScalar = 0.8f;
		public override void init()
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(m_damagable)
			{
				m_damagable.meleeScalar = rocketSkinScalar;	
			}
		}
	}
}