using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class WandererEnemy : ZombieEnemy {
		private Vector3 m_targetPos;
		public Weapon weapon;
		public Vector2 boxX;
		public Vector2 boxZ;
		public override void init ()
		{
			base.init ();
			chooseRandomSpot();
		}
		void chooseRandomSpot()
		{
			m_targetPos = Vector3.zero;
			m_targetPos.x = Random.Range(boxX.x,boxX.y);
			m_targetPos.z = Random.Range(boxZ.x,boxZ.y);
		}
		public override void handleAttack()
		{

			GameObject playerGo = GameObject.Find("Player");
			if(weapon && playerGo)
			{
				Vector3 vec = playerGo.transform.position - transform.position;
				weapon.fire(vec);
			}

		}
		public override void handleAttackPlayer()
		{
			handleAttack();
			Vector3 vec = m_targetPos - transform.position;
			
			float d0 = vec.magnitude;
			if(d0 > attackRange)
			{
				if(m_agent && m_agent.isOnNavMesh)
				{
					m_agent.Resume();
					m_agent.SetDestination(m_targetPos);
				}
			}else{
				chooseRandomSpot();
			}
		}
	}
}