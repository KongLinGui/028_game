using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class BaseSpell : MonoBehaviour {

		public enum SpellID
		{
			SlowDownTime
		};
		protected SpellID m_spellID;

		public float spellCost = 50;

		protected Damagable m_damagable;

		public KeyCode spellKeyCode = KeyCode.Tab;

		public SpellID getSpellID()
		{
			return m_spellID;
		}
		public  virtual void Start()
		{

			m_damagable = transform.GetComponent<Damagable>();

		}


		public void Update()
		{
			if(Input.GetKeyDown(spellKeyCode))
			{
				if(m_damagable.getCurrentMana() >= spellCost)
				{
					spellStart();
					m_damagable.addMana(-spellCost);
				}
			}
		}
		public virtual void spellStart()
		{
		}
		public void onSpellStart(SpellID sid)
		{
			if(m_spellID == sid)
			{
				spellStart();
			}
		}

		public virtual void spellEnd()
		{
		}
		public  void onSpellEnd(SpellID sid)
		{
			if(m_spellID == sid)
			{
				spellEnd();
			}
		}


	}
}
