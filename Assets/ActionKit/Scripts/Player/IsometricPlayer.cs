using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class IsometricPlayer	 : MonoBehaviour {
		public float moveSpeed = 10;
		private PlayerAnimator m_playerAnimator;

		private float m_fireTime = 1f;
		public float fireTime = 1;
		public Weapon weapon;
		private PlayerWeaponManager m_weaponManager;
		private Weapon m_currentWeapon;
		public float delayFireTime = 0.1f;
		private bool m_dead=false;
		private CharacterController m_controller;


		void Start () {
			m_playerAnimator = gameObject.GetComponent<PlayerAnimator>();
			m_controller  = gameObject.GetComponent<CharacterController>();
			m_weaponManager = gameObject.GetComponent<PlayerWeaponManager>();
			m_currentWeapon=weapon;
		}
		public void activeSecondaryGun(int index)
		
		{
			if(m_weaponManager)
			{
				m_weaponManager.activateSeconaryWeapon(index);
				m_currentWeapon = m_weaponManager.getWeapon();	

			}
		}

		public void onDeathCBF()
		{
			BaseGameManager.gameover(true);
			m_playerAnimator.setDead(true);
			m_dead=true;
		}
		// Update is called once per frame
		void Update () {
			float horz = Input.GetAxis("Horizontal");
			float vert = Input.GetAxis("Vertical");
			if(m_dead)
			{
				return;
			}
			Vector3 vec = Vector3.zero;

			updateTimers();
			vec.x = horz;
			vec.z = vert;

			Weapon secondaryWeapon = m_weaponManager.getWeapon();
			if(secondaryWeapon && secondaryWeapon.isEmpty())
			{
				m_weaponManager.closeWeapon();
				m_currentWeapon = weapon;
			}
			Vector3 targetPos;
			RaycastHit rch;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out rch))
			{
				targetPos = rch.point;
				targetPos.y = transform.position.y;
				
				transform.LookAt( targetPos);

			}

			bool moving = false;
			if(m_fireTime<0)
			{
				if(horz!=0 || vert!=0)
				{
					moving = true;
				}
				if(m_playerAnimator)
				{
				//	m_playerAnimator.setShoot(false);

				}

			}
			if(m_playerAnimator)
			{
				m_playerAnimator.setMove(moving);
			}
			if(Input.GetMouseButton(0))
			{
				if(m_fireTime<0)
				{
				//	m_playerAnimator.setShoot(true);
					if(m_currentWeapon)
					{
						m_currentWeapon.fire(m_currentWeapon.transform.forward);
					}
					m_fireTime = fireTime;
				}

			}else{
				m_currentWeapon.hideBeam();
			}
			m_controller.Move(vec.normalized *moveSpeed * Time.deltaTime);
			//transform.position += vec.normalized * moveSpeed * Time.deltaTime;



		//		transform.rotation = Quaternion.AngleAxis(m_dir * 90, Vector3.up);
		}
		void updateTimers()
		{
			m_fireTime -=Time.deltaTime;
			
		}
		public Weapon getCurrentWeapon()
		{
			return m_currentWeapon;
		}
		IEnumerator delayFire(float delayFireTime)
		{
			yield return new WaitForSeconds(delayFireTime);
			if(weapon)
			{
				weapon.fire(transform.forward);
			}
		}
	}
}
