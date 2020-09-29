using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class BaseUpgrade : MonoBehaviour {
		protected Damagable m_damagable;
		private float healthIncreasePerLevel = 5;
		public virtual void Start()
		{
			m_damagable = gameObject.GetComponentInParent<Damagable>();
			if(m_damagable)
			{
				m_damagable.increaseMaxHealth(healthIncreasePerLevel);
			}
			init ();
		}	
		public virtual void init(){}
		public virtual void OnEnable()
		{		
		}
		public virtual	 void OnDisable()
		{
		}


	}
}