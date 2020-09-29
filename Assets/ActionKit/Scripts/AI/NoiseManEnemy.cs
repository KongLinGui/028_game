using UnityEngine;
using System.Collections;

namespace InaneGames {

	public class NoiseManEnemy : DemonEnemy {
		private float m_blinkTime;
		public float blinkTime = 0;
		public GameObject blinkGameObject;
		public bool on = false;

		public override void onDeathCBF()
		{
			base.onDeathCBF();
			Destroy(blinkGameObject);
		}
		public override void updateUnit()
		{
			m_blinkTime -= Time.deltaTime;
			if(m_blinkTime<0)
			{
				if(blinkGameObject)
				{
					blinkGameObject.SetActive( on );
					m_damagable.setInvincible(on);	
				}
				if(on==false)
				{
					on=true;
				}else{
					on=false;
				}
				m_blinkTime = blinkTime;
			}
		}
	}
}