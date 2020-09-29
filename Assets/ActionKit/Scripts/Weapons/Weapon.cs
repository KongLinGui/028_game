using UnityEngine;
using System.Collections;


namespace InaneGames {
/// <summary>
/// Our weapon classs
/// </summary>
	public class Weapon : MonoBehaviour 
	{
		public enum WeaponType{
			WeaponTypeRay,
			WeaponTypeProjectile,
			WeaponTypeBeam
		};
		public WeaponType weaponType;


		public bool owned = false;


		public bool useDisperse = true;

		public float disperseScalar = 25f;

		public float disperse = 0;

		public float disperseMax = 1f;


		public float gunRange = Mathf.Infinity;

		public bool useAmmo = true;

		///the maximum number of bullets
		public int maxNomBullets = 10;
		
		///the current number of bullets
		public int currentNomBullets = 10;
		
		///the cooldown time
		public float cooldownTime = 0.125f;
		//the gun mask
		public LayerMask gunMask;

		///the time it takes to reload the gun
		public float reloadTime = 1f;
		
		///does this weapon have infinite ammo
		public bool infiniteAmmo = false;

		public float damagePerHit = 5f;
		/// <summary>
		/// The spread of the gun.
		/// </summary>
		public float spread = 0;

		public int nomBounces = 1;
		public bool useBeam = true;

		public bool useSound = true;

		public bool useProjectile = true;
		/// <summary>
		/// The projectiles per shot.
		/// </summary>
		public int projectilesPerShot = 1;
		public bool isAutomatic = false;
		public enum State
		{
			IDLE,
			COOLDOWN,
			EMPTY,
			RELOAD
		};
		public State initalState = State.IDLE;
		public int shellsPerShot = 1;

		public float projectileSpeed  = 10f;


		protected State m_state;
		protected float m_reloadTime;
		protected float m_cooldownTime;
		
		public Vector3 gunUp = Vector3.up;

		public GameObject bulletSpawnPoint;
		public GameObject muzzleFlashGO;
		public GameObject hitEffectGO;
		public float knockBackForce;

		//is the gun active. 
		public bool active = true;


		public Material laserMat;
		float refVel;
		public float currentTorque;
		public Damagable.DamagableType damagableType;

		public int gunSlotIndex = 1;

		public GameObject projectilePrefab;

		public float gunDelayTime = 0f;


		public GameObject shellGameObject;
		public Transform shellSpawn;

		public float shellMinForce = 10f;
		public float shellUpForce = 10f;
		public Vector2 shellTorque = new Vector2(10,10);
		public float shellTorqueRandom = 10;


		public bool useShell=true;

		public int currentNomClips = 0;
		public int maxNomClips = 3;

		public float beamRadius = 10;
		private LineRenderer[] m_lineRenderer;
		public GameObject weaponObject;
		public Sprite weaponTexture;
		public AudioClip fireAC;
		public AudioClip reloadAC;
		public AudioClip emptyAC;
		public float rigidBodyForce = 1000f;
		private Vector3[] m_targetPos;

		public void OnDisable()
		{
			fireOff();
		}
		public void Start()
		{
			m_state = initalState;
			currentNomClips = maxNomClips;
			currentNomBullets  = maxNomBullets;



			createBeam();
			hideBeam();
		}
		void createBeam()
		{
			if(weaponType==WeaponType.WeaponTypeBeam || weaponType==WeaponType.WeaponTypeProjectile)
			{
				m_targetPos = new Vector3[projectilesPerShot];
				m_lineRenderer = new LineRenderer[ projectilesPerShot];
				for(int i=0; i<m_lineRenderer.Length; i++)
				{
					GameObject laserGO = new GameObject();
					m_lineRenderer[i] = laserGO.AddComponent<LineRenderer>();
					laserGO.transform.parent = transform;
					laserGO.transform.localPosition = Vector3.zero;
					laserGO.transform.rotation = Quaternion.identity;
					
					
					m_lineRenderer[i].material = laserMat;
					m_lineRenderer[i].SetWidth(beamRadius,beamRadius);
					m_lineRenderer[i].SetVertexCount(2);
				}
			}
		}

		public void handleReload()
		{
			if(m_state==State.IDLE)
			{
				GetComponent<AudioSource>().PlayOneShot(reloadAC);
				m_reloadTime = reloadTime;
				m_state = State.RELOAD;
			}
		}
		public void reload()
		{
			currentNomBullets = maxNomBullets;
		}
		public virtual void Update()
		{
			float dt = Time.deltaTime;
			switch(m_state)
			{
				case State.RELOAD:
					handleReloadTime(dt);
				break;
				case State.COOLDOWN:
					handleCooldownTime(dt);
				break;		
			}

		}
		void handleCooldownTime(float dt)
		{
			m_cooldownTime -= dt;
			if(m_cooldownTime<0)
			{
				//set it back to idle state and refill the bullets
				m_state = State.IDLE;
			}
		}
		void handleReloadTime(float dt)
		{
			m_reloadTime -= dt;
			if(m_reloadTime<0)
			{
				//set it back to idle state and refill the bullets
				m_state = State.IDLE;
				currentNomBullets = maxNomBullets;

			}
		}
		public void refill()
		{
			currentNomBullets = maxNomBullets;
			currentNomClips = maxNomClips;
			m_state = State.IDLE;

		}

		public void autoReload()
		{
			m_state = State.IDLE;
			currentNomBullets = maxNomBullets;
		}

		public void  altFire(Vector3 dir)
		{
			_fire(dir);
		}
		public virtual bool fire(Vector3 dir)
		{
//			Debug.Log ("fire");

			return _fire(dir);
		}



		public virtual bool _fire(Vector3 vdir)
		{
			bool rc = false;
//			Debug.Log ("_fire" + currentNomBullets	);

			int bulletsPerShot = 1;
			if(currentNomBullets < bulletsPerShot)
			{
				bulletsPerShot = currentNomBullets;
			}

			//if we have enough bullets 
			if(currentNomBullets > 0)
			{
				if(m_state == State.IDLE )
				{
					//if(gunDelayTime<0)
					{
						Transform t0 = transform;


						//fire the weapon
						_fire(t0.position,
						      vdir,
						      projectilesPerShot * bulletsPerShot);
						rc=true;

						//decrease the number of bullets
						currentNomBullets-= bulletsPerShot;
						
						if(currentNomBullets>0)
						{
							//call a cooldown function
							m_state = State.COOLDOWN;
							m_cooldownTime = cooldownTime;
						}else{
							handleGunEmpty();
						}
					}
				}
			}else{
					handleGunEmpty();
			}		
			return rc;
		}
		public void handleGunEmpty()
		{

			if(infiniteAmmo==false && currentNomClips<1)
			{
				if(GetComponent<AudioSource>().isPlaying==false)
						GetComponent<AudioSource>().PlayOneShot(emptyAC);
				hideBeam();
				m_state = State.EMPTY;
			}else{

				if(m_state!=State.RELOAD)
				{
					hideBeam();
					currentNomClips--;
					handleReload();

				}
					
			}		
		}

		public virtual void _fire(Vector3 currentPos, Vector3 dir, int p)
		{
			float tmpSpread = spread;
			if(p==1)tmpSpread=0;
			float spreadX = -tmpSpread/2f;
			float dx = tmpSpread / (float)(p-1);
//			Debug.Log ("FIRE");
			Transform t1 = transform;
			if(bulletSpawnPoint)
			{
				t1 = bulletSpawnPoint.transform;
			}

			//create our muzzle flash
			if(muzzleFlashGO && t1)
			{
				GameObject go = (GameObject)Instantiate(muzzleFlashGO,t1.position,t1.rotation);
				if(go)
				{
					go.transform.parent =  transform;
				}
			}
			if(useShell)
			{
				ejectShell();
			}

			if(GetComponent<AudioSource>())
				GetComponent<AudioSource>().PlayOneShot(fireAC);
			for(int i=0; i<p; i++)
			{
				float heightDev = Random.Range (-disperse, disperse);

				Vector3 spreadVec = new Vector3 (Random.Range (-disperse, disperse), heightDev, Random.Range (-disperse, disperse)) / 100f;
				Vector3 newDir = Quaternion.AngleAxis(spreadX,gunUp) * dir;	
				fireWeapon(currentPos,newDir + spreadVec,i);
				spreadX+=dx;
			}

			if(isAutomatic)
			{
				disperse+=Time.deltaTime * disperseScalar;
				if(disperse>disperseMax)
				{
					disperse = disperseMax;
				}
			}
		}


		public void fireOff()
		{
			if(m_lineRenderer!=null)
			{
				for(int i=0; i<m_lineRenderer.Length; i++)
				{
					m_lineRenderer[i].enabled=false;
				}
			}
		}
		public void LateUpdate()
		{
			if(m_lineRenderer!=null)
			{
				for(int i=0; i<m_lineRenderer.Length; i++)
				{
					m_lineRenderer[i].SetPosition(0,transform.position);	
					m_lineRenderer[i].SetPosition(1, m_targetPos[i]);

				}
			}
		}
		void createTracer(Vector3 startPos,Vector3 endPos)
		{
			GameObject laserGO = new GameObject();
			if(laserGO)
			{
				LineRenderer lr = laserGO.AddComponent<LineRenderer>();
				laserGO.name = "Tracer";
				lr.material = laserMat;
				lr.SetWidth(beamRadius,beamRadius);
				lr.SetVertexCount(2);
				lr.SetPosition(0,startPos);
				lr.SetPosition(1,endPos);
				Destroy(laserGO,tracerFadeTime	);
			}
			
		}
		public float tracerFadeTime = 0.1f;

		public virtual void fireWeapon(Vector3 currentPos, Vector3 dir,int i)
		{

			if(weaponType == WeaponType.WeaponTypeBeam)
			{
				RaycastHit rch;
				m_lineRenderer[i].enabled=true;
				 
				Ray r = new Ray(currentPos,dir.normalized);

				bool hitSomething = Physics.Raycast(r,out rch,gunRange,gunMask.value);
				m_lineRenderer[i].SetPosition(0,transform.position);
				if(hitSomething)
				{
					m_targetPos[i] = rch.point;
					onHitTarget(rch.point,rch.normal,rch.transform.gameObject);
					m_lineRenderer[i].SetPosition(1, m_targetPos[i]);

					if(hitEffectGO)
						Instantiate(hitEffectGO,m_targetPos[i],Quaternion.identity);
				}else{
					m_lineRenderer[i].enabled=true;

					m_lineRenderer[i].SetPosition(0,transform.position);
					if(hitSomething)
					{
						m_targetPos[i] = rch.point;
						m_lineRenderer[i].SetPosition(1, m_targetPos[i]);
					}

					m_targetPos[i] = r.GetPoint(1000f);
					m_lineRenderer[i].SetPosition(1, m_targetPos[i]);

				}
			}
			if(weaponType == WeaponType.WeaponTypeRay)
			{
				RaycastHit rch;
				Ray r = new Ray(currentPos,dir);
				bool hitSomething = Physics.Raycast(r,out rch,gunRange,gunMask.value);
				if(hitSomething)
				{
					createTracer(rch.point,currentPos);

					onHitTarget(rch.point,rch.normal,rch.transform.gameObject);
				}else
				{
					Vector3 targetPos = r.GetPoint(1000f);
					createTracer(currentPos,targetPos);

				}
			}
			if(weaponType == WeaponType.WeaponTypeProjectile)
			{
				Transform t1 = transform;

				if(projectilePrefab)
				{
					GameObject go = (GameObject)Instantiate(projectilePrefab,currentPos,t1.rotation);
					if(go)
					{
						Rocket rocket = go.GetComponent<Rocket>();
						if(rocket)
						{
							GameObject roomObject = GameObject.Find("Room");
							if(roomObject)
							{
								go.transform.parent = roomObject.transform;
							}
							rocket.fire(currentPos,
							            dir,
							            projectileSpeed,
							            gameObject.layer);
						}
					}
				}
			}

		}
	
		public void onHitTarget(Vector3 targetPos,Vector3 normal,GameObject go)
		{
			Damagable dam = go.GetComponent<Damagable>();
			if(dam)
			{
				dam.damage(damagePerHit,targetPos,damagableType,knockBackForce);
			}

			Rigidbody rb = go.GetComponent<Rigidbody>();
			if(rb)
			{
				Vector3 vec = targetPos -transform.position;// - targetPos;
				rb.AddForce( vec.normalized * rigidBodyForce);
			}

			if(hitEffectGO)
			{
				GameObject ngo = (GameObject)Instantiate( hitEffectGO,targetPos+normal*.1f,Quaternion.FromToRotation(Vector3.up, normal));
				if(ngo && go)
				{
					ngo.transform.parent = go.transform;
				}
			}
		}
		void ejectShell()
		{
			// Instantiate shell props
			if (shellGameObject && shellSpawn)
			{
				GameObject newObject = Instantiate(shellGameObject, shellSpawn.position, shellSpawn.rotation) as GameObject;
				if(newObject)
				{
					Vector3 vec = new Vector3(shellMinForce + Random.Range(0, shellUpForce),0, 0);
					
					newObject.GetComponent<Rigidbody>().AddRelativeForce(vec, ForceMode.Impulse);

					
					vec = new Vector3(shellTorque.x + Random.Range(-shellTorqueRandom,shellTorqueRandom), 
					                  shellTorque.y + Random.Range(-shellTorqueRandom, shellTorqueRandom), 0);
					
					
					newObject.GetComponent<Rigidbody>().AddRelativeTorque(vec,ForceMode.Impulse);
				}
			}
		}


		public virtual void hideBeam()
		{
			if(m_lineRenderer!=null)
			{
				for(int i=0; i<m_lineRenderer.Length; i++)
				{
					if(m_lineRenderer[i])
					{
						m_lineRenderer[i].enabled=false;
					}
				}
			}
		}
		public string getAmmoString()
		{
			string clipsPostfix = "∞";
			if(infiniteAmmo==false)
			{
				clipsPostfix =  (maxNomBullets*currentNomClips).ToString();

			}
			return currentNomBullets.ToString() + " / " + clipsPostfix;
		}
		public bool isFull()
		{
			return currentNomBullets==maxNomBullets;
		}
		public bool getReloading()
		{
			return m_state == State.RELOAD;
		}
		public float getReloadAsScalar()
		{
			return 1.0f - m_reloadTime / reloadTime;
		}
		public float getAmmoNormalized()
		{
			return (float)currentNomBullets / (float)maxNomBullets;
		}
		public bool isEmpty()
		{
			return m_state == State.EMPTY;
		}
		public void fillAmmo()
		{
			currentNomBullets = maxNomBullets;
			currentNomClips = maxNomClips;
			m_state = State.IDLE;
		}
		public string getBulletsAsString()
		{
			return currentNomBullets.ToString() + " / " + maxNomBullets.ToString();
		}

	}
}
