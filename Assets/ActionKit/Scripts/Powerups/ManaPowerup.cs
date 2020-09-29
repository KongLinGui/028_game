using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class ManaPowerup : BasePowerup {
		public int mana;
		public override void handlePowerup(GameObject go)
		{
			GameObject playerObj = GameObject.Find("Player");
			Damagable dam =playerObj.GetComponent<Damagable>();
			if(dam)
			{
				dam.addMana( mana );
			}
		}
	}
}