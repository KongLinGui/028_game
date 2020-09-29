using UnityEngine;
using System.Collections;
using InaneGames;

namespace InaneGames {

	public class SlowerEnemyBulletUpgrade : BaseUpgrade
	{
		public float bulletScalar = 0.8f;
		public override void init()
		{
			Rocket.ENEMY_BULLET_SCALAR = bulletScalar;
		}
	}
}