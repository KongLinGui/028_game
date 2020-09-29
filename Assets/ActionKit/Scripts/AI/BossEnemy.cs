using UnityEngine;
using System.Collections;

using InaneGames;
namespace InaneGames {

	public class BossEnemy : WandererEnemy {

		public GameObject[] tankModels;
		public Weapon[] weaponList;
		private float m_currentOffset = 0.75f;
		private float m_offset = 0.25f;

		private int m_weaponIndex = 0;
		private float m_blinkTime;
		public float blinkTime = 0;
		public GameObject blinkGameObject;
		public bool on = false;
		public override void init ()
		{
			base.init ();
			m_offset =  (1.0f / (float)weaponList.Length);
			m_currentOffset = 1.0f - m_offset;
		}
		public void onHitCBF()
		{
			if(m_damagable.getHealthAsScalar() < m_currentOffset)
			{
				m_weaponIndex++;
				if(m_weaponIndex<=	weaponList.Length-1)
				{
					weapon = weaponList[m_weaponIndex];
					Destroy(tankModels[m_weaponIndex-1]);
				}
				m_currentOffset-=m_offset;
			}
		}

		
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