using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class WarLordUpgrade : BaseUpgrade {
		public int enemiesToKill = 5;
		
		public override void init()
		{
			PackageManager fullton = (PackageManager)GameObject.FindObjectOfType(typeof(PackageManager	));

			if(fullton)
			{
				fullton.enemiesToKill -=enemiesToKill;
			}
		}
	}
}