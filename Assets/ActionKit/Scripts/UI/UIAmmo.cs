using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InaneGames;
namespace InaneGames {

	public class UIAmmo : MonoBehaviour {
		public Text ammoText;
		public Image weaponImage;
		public void Update()
		{
			//WeaponInventory wi = (WeaponInventory)GameObject.FindObjectOfType(typeof(WeaponInventory));
			GameObject gameObject = GameObject.Find ("Player");
			if(gameObject)
			{
				IsometricPlayer player = gameObject.GetComponentInChildren<IsometricPlayer>();
				if(player)
				{
					Weapon weapon = player.getCurrentWeapon();
					if(ammoText)
					{
						if(weaponImage)
						{
							weaponImage.sprite = weapon.weaponTexture;
						}

						if(weapon)
						{
							string postFix = "<color=green> ∞</color>";
							if(weapon.infiniteAmmo==false	)
							{
								postFix = "<size=12><color=green> x" + weapon.currentNomClips + "</color></size>";
							}

							if(weapon.isEmpty()==false)
							{
								if(weapon.getReloading ()==false)
								{
									ammoText.text = weapon.getBulletsAsString() + postFix;
								}else
								{
									ammoText.text = "Reloading";
								}
							}else{
								ammoText.text = "Empty";	
							}
						}
						else{
							ammoText.text = "";	
						}
					}
				}
			}
		}
	}
}
