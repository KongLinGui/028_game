using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class HealthPowerup : BasePowerup {

		public float health;
		public override void handlePowerup(GameObject go)
		{
			GameObject playerObj = GameObject.Find("Player");

			Damagable dam = (Damagable)playerObj.GetComponent<Damagable>();
			if(dam)
			{
				dam.heal( health );
			}
		}
	}
}