using UnityEngine;
using System.Collections;

namespace InaneGames {

	/// <summary>
	/// A simple script that will destroy itself when you pick it up, and activate a secondary gun for the player.
	/// </summary>
	public class WeaponActivator : MonoBehaviour {

		//the gun object to activate
		public int gunIndex = 1;

		//do we want to destroy this object when it gets picked up by the player.
		private bool m_delayedDestroy=false;

		//the name of the player object
		public string playerGameObjectName = "Player";

		public void Update()
		{
			if(m_delayedDestroy)
			{
				Destroy(gameObject);

			}
		}
		public void OnTriggerEnter(Collider col)
		{
			if(col.name.Equals(playerGameObjectName))
			{
				//lets grab our isometric player script, if we find it, lets activate that gun and destroy ourself
				IsometricPlayer player = (IsometricPlayer)GameObject.FindObjectOfType(typeof(IsometricPlayer	));
				if(player)
				{
					player.activeSecondaryGun(gunIndex);
				}
				m_delayedDestroy=true;
			}
		}
	}
}