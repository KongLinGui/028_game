using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class PlayerAnimator : MonoBehaviour {
		private Animator m_animator;
		void Start () {
			m_animator = gameObject.GetComponentInChildren<Animator>();
		}
		public void setDead(bool dead)
		{
			if(m_animator)
			{
				m_animator.SetBool("Dead",dead);
			}
		}
		public void setShoot(bool shoot)
		{
			if(m_animator)
			{
				m_animator.SetBool("Shoot",shoot);
			}
		}
		public void setMove(bool moving)
		{
			if(moving)
			{
				m_animator.SetBool("Move",true);
			}else{
				m_animator.SetBool("Move",false);
			}
		}
		

	}
}