using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class FireskinUpgrade : BaseUpgrade {

		public float fireballScalar = 0.8f;
		public override void init()
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(m_damagable)
			{
				m_damagable.fireballScalar = fireballScalar;	
			}
		}
	}
}