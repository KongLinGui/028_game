using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class DemonEnemy : ZombieEnemy {
		public Weapon weapon;
		public override void handleAttack()
		{
			if(m_attackTime<0 && m_hitTime<0)
			{
				if(m_animator)
				{
					m_animator.SetBool("Attack",true);
				}
				if(m_agent && m_agent.isOnNavMesh)
				{
					m_agent.Stop();
				}

				GameObject playerGo = GameObject.Find("Player");
				if(weapon && playerGo)
				{
					Vector3 vec = playerGo.transform.position - transform.position;

					weapon.fire(vec);
				}
				m_attackTime = attackTime;

			}
		}
	}
}