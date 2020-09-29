using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class TimeStopUpgrade : BaseUpgrade
	{
		public float slowTime = 2f;
		public override void init()
		{
			SlowDownTime sdt = gameObject.GetComponentInParent<SlowDownTime>();
			if(sdt)
			{
				sdt.slowTime += slowTime;
			}
		}
	}

}