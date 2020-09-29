using UnityEngine;
using System.Collections;

namespace InaneGames {

	public class Misc : MonoBehaviour {

		public static void throwObject(GameObject go,
		                               float shellBaseForce,
		                               float shellFExtraForce,
		                               Vector3 shellTorque, 
		                               float shellTorqueRandom )
		{
			float r = shellBaseForce + Random.Range(0, shellFExtraForce);
			
			Vector2 v = Random.insideUnitCircle;
			Vector3 vec = new Vector3(v.x,1f, v.y);
			
			go.GetComponent<Rigidbody>().AddRelativeForce(vec * r, ForceMode.Impulse);
			

		}
		public  static void damageArea(Vector3 pos,float aoe,float dmg,
		                               int layer,int value,
		                               Damagable.DamagableType damagableType)
		{

			Collider[] col = Physics.OverlapSphere(pos,aoe,value);
			for(int i=0;i<col.Length; i++)
			{
				Damagable dam = col[i].GetComponent<Damagable>();
				if(dam && col[i].gameObject.layer!=layer)
				{
					dam.damage(dmg,dam.transform.position,damagableType	);
				}
			}
		}




	}
}