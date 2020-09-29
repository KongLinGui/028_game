using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class PlayerWeaponManager : MonoBehaviour {
		public Weapon[] secondaryWeapons;
		private Weapon m_currentWeapon;
		private int m_currentIndex = 0;

		public void closeWeapon()
		{
		}
		public void activateSeconaryWeapon(int weaponIndex)
		{
			secondaryWeapons[m_currentIndex].hideBeam();
			secondaryWeapons[m_currentIndex].gameObject.SetActive(true);

			m_currentWeapon = secondaryWeapons[weaponIndex];
			m_currentWeapon.refill();
			FloatingTextManager.pushFloatingText("Picked Up: " + m_currentWeapon.name,Color.green);	


			m_currentIndex = weaponIndex;
		}

		public Weapon getWeapon()
		{
			return m_currentWeapon;
		}
	}
}