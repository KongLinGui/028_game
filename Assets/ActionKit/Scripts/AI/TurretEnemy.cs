using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class TurretEnemy : BaseEnemy {


		
		public override void handleThink()
		{
			GameObject go = GameObject.Find("Player");		
			if(go==null)
			{
				return;
			}
			Vector3 pos = go.transform.position;
			pos.y = transform.position.y;	
			//transform.LookAt(pos);
			if(go.transform.position.	x > transform.position.x)
			{
				transform.rotation = Quaternion.AngleAxis(90,Vector3.up);
			}else{
				transform.rotation = Quaternion.AngleAxis(-90,Vector3.up);
				
			}

			fireGuns(transform.position + transform.rotation * new Vector3(100,0,0));
		}
	}
}