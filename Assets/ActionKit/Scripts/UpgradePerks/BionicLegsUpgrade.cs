using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class BionicLegsUpgrade : BaseUpgrade {
		public float moveSpeedBonus = 5;
		
		public override void init()
		{
			IsometricPlayer player = gameObject.GetComponentInParent<IsometricPlayer>();
			if(player)
			{
				player.moveSpeed += moveSpeedBonus;
			}
		}
	}
}