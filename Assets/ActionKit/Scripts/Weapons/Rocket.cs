using UnityEngine;
using System.Collections;
namespace InaneGames {
	/// <summary>
	/// Simple rocket class.
	/// </summary>
	public class Rocket  : MonoBehaviour
	{
		//the time the rocket has to live.
		public float ttl = 2.0f;

		//the objet to spawn when the rocket hits something
		public GameObject objectOnHit;
		
		//the area of effect
		public float aoe = -1f;

		//the damage per hit.
		public float damagePerHit = 5f;
		
		//has the rocket been destroyed
		private bool m_removed=false;

		//the speed of the projectile
		private float m_projectileSpeed;

		//do we want to explode on contact
		public bool explodeOnContact = true;

		//is this rocket owned by the player
		public bool ownedByPlayer = false;

		//a refence to the damagable type
		public Damagable.DamagableType damagableType;

		//the scalar of the enemy bullet
		public static float ENEMY_BULLET_SCALAR = 1f;

		//do we want to seek towards a target
		public bool heatSeek = false;

		//our target
		private Transform m_target;

		//the heat seek scalar
		public float seekScalar = 5;

		// a refence to the rigidbody
		private Rigidbody m_body;

		public void Awake()
		{
			m_body=gameObject.GetComponent<Rigidbody>();
		}
	
		public void reflect(int newLayer)
		{

			gameObject.layer = 11;
			GetComponent<Rigidbody>().velocity = -GetComponent<Rigidbody>().velocity.normalized * m_projectileSpeed ;	
			Vector3 targetPos = transform.position + GetComponent<Rigidbody>().velocity.normalized;
			transform.LookAt(targetPos);


		}
		public void Update()
		{
			float dt = Time.deltaTime;
		//	if(ownedByPlayer==false)
			{
				dt *= SlowDownTime.SLOW_DOWN_TIME_SCALAR;
			}
			float speedScalar = 1f;
			if(ownedByPlayer==false){
				speedScalar = SlowDownTime.SLOW_DOWN_TIME_SCALAR;

			}

			if(heatSeek){
				handleSeekRocket();
			}else
			{
				GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * m_projectileSpeed * speedScalar * ENEMY_BULLET_SCALAR;
			}
			ttl-=Time.deltaTime;


			if(ttl< 0f)
			{
				removeMe();
			}
		}
		public void OnTriggerEnter(Collider col)
		{
			if(explodeOnContact==false	)
			{
				Damagable dam = col.gameObject.GetComponent<Damagable>();
				if(dam)
				{
					onHitTarget(dam);
					
				}else{
					removeMe();

				}

			}
		}	
		public void OnCollisionEnter(Collision col)
		{
			if(explodeOnContact)
			{
				Damagable dam = col.gameObject.GetComponent<Damagable>();

				if(dam)
				{
					onHitTarget(dam);
	
				}
				if(aoe!=-1)
				{
					damageArea(transform.position,aoe);
				}
				{
					removeMe();
				}
			}
		}
		public void onHitTarget(Damagable dam)
		{
			dam.damage(damagePerHit,transform.position,damagableType);
			if(explodeOnContact)
					removeMe();
		}
		public void damageArea(Vector3 pos,float aoe)
		{
			Collider[] col = Physics.OverlapSphere(pos,aoe);
			for(int i=0;i<col.Length; i++)
			{
				Damagable dam = col[i].GetComponent<Damagable>();
				if(dam)
				{
					dam.damageArea(damagePerHit,
					           dam.transform.position,
					           damagableType);
				}
			}
		}
		public void fire(Vector3 currentPos,
						 Vector3 dir,
						  float projectileSpeed,
						 int objectLayer)
		{
			Vector3 targetPos = currentPos + dir;
			m_projectileSpeed = projectileSpeed;
			transform.LookAt(targetPos);
			GetComponent<Rigidbody>().velocity = dir.normalized * projectileSpeed;		
		}
		void handleSeekRocket()
		{
			if(m_target==null)
			{
				m_target = getTarget();
			}
			
			
			Vector3 direction;
			Vector3 vel;
			if(m_target)
			{
				direction = transform.forward;
				vel = direction.normalized * m_projectileSpeed;
				
				direction = m_target.position - transform.position;
				direction.Normalize();
				
				
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), seekScalar * Time.deltaTime);
				if(m_body != null)
				{
					m_body.velocity  = vel;
				}
			}
		}
		Transform getTarget()
		{
			Transform target = null;
			float d0 = Mathf.Infinity;
			Damagable[] driver = (Damagable[])GameObject.FindObjectsOfType(typeof(Damagable));

			for(int i=0; i<driver.Length; i++)
			{
				if(driver[i].gameObject.layer != gameObject.layer && driver[i].gameObject.name.Contains("Player")==false)
				{
					float d1 = (driver[i].transform.position - transform.position).magnitude;
					
					if(d1<d0 && driver[i].isAlive())
					{
						target =  driver[i].transform;
						
						d0 = d1;
					}
				}
			}
			return target;
		}
		

		public void removeMe(){
			if(m_removed==false)
			{
				if(GetComponent<AudioSource>())
				{
					GetComponent<AudioSource>().Play();
				}
				if(objectOnHit!=null){
					Instantiate( objectOnHit,transform.position,Quaternion.identity);
				}
				Destroy (gameObject);
					m_removed=true;
			}
		}
		
	}
}
