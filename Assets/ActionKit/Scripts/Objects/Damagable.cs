using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class Damagable : MonoBehaviour {
		[Tooltip("The number of hits")]
		public float nomHits = 1000;
		private float m_nomHits;

		[Tooltip("The object to create when hit")]
		public GameObject objectOnHit;

		private bool m_spellDamaged =false;
		private bool m_removed=false;

		private bool m_invulnerable = false;


		[Tooltip("The object to create on death")]
		public GameObject objectOnDeath;

		[Tooltip("The points used")]
		public int points = 10;

		private float m_lastHitTime = -1f;
		private float lastHitTime = .1f;
		public float destroyTime = 0.75f;

		[Tooltip("The current Mana")]
		public float currnetMana = 0;
		

		public float aoeScalar = 1f;
		[Tooltip("The max Mana")]

		public float maxMana = 100;
		public float heightOffset = 1f;

		public float manaRegen = 0;
		public float healthRegen = 0;
		private float m_manaScalar 	= 1;
		private float m_healthScalar = 1;
		public enum DamagableType
		{
			MELEE,
			ROCKET,
			FIREBALL,
			SUICIDE
		};
		public DamagableType damagableType;
		public float initalNomHits=100;


		public float fireballScalar =1;
		public float rocketScalar   = 1f;
		public float meleeScalar 	= 1;
		private float m_lastDamage = 0;
		private float m_knockBackForce=0;
		public void Start()
		{
			m_nomHits = initalNomHits;
		}
		public virtual void onDeath()
		{
			gameObject.SendMessage("onDeathCBF",SendMessageOptions.DontRequireReceiver);

		}

		public void heal(float val)
		{
			m_nomHits+=(val*m_healthScalar);
			if(m_nomHits>nomHits)
			{
				m_nomHits=nomHits;
			}
		}

		public void addMana(float mana)
		{
			currnetMana += (mana * m_manaScalar);
			if(currnetMana>maxMana)
			{
				currnetMana = maxMana;
			}
		}
		public virtual void onHit(float dmg)
		{
			gameObject.SendMessage("onHitCBF",dmg,SendMessageOptions.DontRequireReceiver);
		}

		public void killSelf()
		{
			damage(m_nomHits,transform.position,DamagableType.SUICIDE);
		}
		public void damage(float dmg,DamagableType damagableType)
		{
			damage(dmg,transform.position,damagableType);
		}
		public float critialStrike(){
			if(m_invulnerable==false && m_nomHits>0)
			{
				m_nomHits -=m_lastDamage;
				if(m_nomHits<=0)
				{
					removeMe();
				}
			}
			return m_lastDamage;
		}
		public void damageArea(float dmg,
		                   Vector3 pos,
		                   DamagableType damagableType,
		                   float knockBackForce=0)
		{
			dmg*=aoeScalar;

			damage(dmg,pos,damagableType,knockBackForce);
		}

		public void damage(float dmg,
		                   Vector3 pos,
		                   DamagableType damagableType,
		                   float knockBackForce=0)
		{
			m_knockBackForce = knockBackForce;
			if(m_invulnerable==false)
			{
				float dscalar = 1;
				if(damagableType==DamagableType.ROCKET)
				{
					dscalar = rocketScalar;
				}
				if(damagableType==DamagableType.FIREBALL)
				{
					dscalar = fireballScalar;
				}
				if(damagableType==DamagableType.MELEE)
				{
					dscalar = meleeScalar;
				}
	//			Debug.Log ("damageType" + damagableType + "dscalar " + dscalar);
				m_lastDamage=(dmg*dscalar);
				m_nomHits-=m_lastDamage;


				onHit(dmg);

				if(objectOnHit)
				{
					if(m_nomHits>0)
					{
						Instantiate(objectOnHit,pos+new Vector3(0,heightOffset,0),Quaternion.identity);
		
					}
					m_lastHitTime = lastHitTime;
				}


				if(m_nomHits<=0)
				{

					removeMe();
				}
			}
		}
		void removeMe()
		{
			if(m_removed==false)
			{
				if(objectOnDeath)
				{
					Instantiate(objectOnDeath,transform.position+new Vector3(0,heightOffset,0),Quaternion.identity);
				}
				BaseGameManager.damagableDestroy(gameObject);

				onDeath();
				Destroy(gameObject,destroyTime);
				m_removed=true;
			}
		}
		
		public void scaleHealth(float scalar)
		{
			nomHits*=scalar;
			m_nomHits=nomHits;
		}
		public void increaseManaScalar(float val)
		{
			m_manaScalar += val;
		}
		public void increaseHealthScalar(float val)
		{
			m_healthScalar += val;
		}
		public void increaseMaxHealth(float val)
		{
			nomHits += val;
			m_nomHits+=val;
			m_nomHits = Mathf.Clamp(m_nomHits,0,nomHits);
		}
		public void increaseMaxMana(float val)
		{
			maxMana += val;
		}
		
		
		public bool getSpellDamaged()
		{
			return m_spellDamaged;
		}
		public virtual void Awake()
		{
			m_nomHits = nomHits;
		}
		public bool isAlive()
		{
			return m_nomHits>0;
		}
		
		public void setInvincible(bool inv)
		{
			m_invulnerable=inv;
		}
		public float getManaAsScalar()
		{
			return currnetMana / maxMana;
		}
		public string getHealthSTR()
		{
			int iNomHits = (int)m_nomHits;
			return iNomHits + " / " + nomHits;
		}
		public string getManaSTR()
		{
			int ival = (int)currnetMana;

			return ival + " / " + maxMana;
		}
		public float getMaxHealth()
		{
			return nomHits;
		}
		public float getKnockBackForce()
		{
			return m_knockBackForce;
		}
		public float getHealthAsScalar()
		{
			return m_nomHits  / nomHits;
		}
		public void LateUpdate()
		{
			m_nomHits+=healthRegen * Time.deltaTime;
			if(m_nomHits > nomHits)
			{
				m_nomHits = nomHits;
			}
			currnetMana+=manaRegen * Time.deltaTime;
			if(currnetMana > maxMana)
			{
				currnetMana = maxMana	;
			}

			m_lastHitTime-=Time.deltaTime;
		}
		
		public float getCurrentMana()
		{
			return currnetMana;
		}
	}
}
