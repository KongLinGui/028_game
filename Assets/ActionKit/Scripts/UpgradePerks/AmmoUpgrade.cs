using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class AmmoUpgrade : BaseUpgrade {
		public float chanceToReloadSecondaryWeapon = 10f;
		public GameObject floatingtext;

		public override void OnEnable()
		{		
			base.OnEnable();
			BaseGameManager.onEnemyDie += onEnemyDie;
		}
		public override void OnDisable()
		{
			base.OnDisable();
			BaseGameManager.onEnemyDie -= onEnemyDie;
		}

		public void onEnemyDie(GameObject go)
		{
			IsometricPlayer IsometricPlayer = gameObject.GetComponentInParent<IsometricPlayer>();

			if(IsometricPlayer)
			{
				float r = Random.Range(0,100);

				if(r<chanceToReloadSecondaryWeapon)
				{
					BaseGameManager.floatText("LUCKY RELOAD",go,Color.yellow);
					Weapon weapon=	IsometricPlayer.getCurrentWeapon();
					if(weapon && weapon.infiniteAmmo==false)
					{
						weapon.reload();
					}
				}
			}
			
		}


	}
}
