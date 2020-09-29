using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class Package : MonoBehaviour 
	{

		//the weapon to create
		public GameObject weapon;

		//the health gem
		public GameObject healthGem;

		//the mana gem
		public GameObject manaGem;

		//the inital force
		public float shellBaseForce = 10f;

		//the extra force
		public float shellFExtraForce = 10f;

		//the ammount of torque
		public Vector2 shellTorque = new Vector2(10,10);

		//a random ammount of torque
		public float shellTorqueRandom = 10;


		public void onDeathCBF()
		{
			Instantiate(weapon,transform.position,Quaternion.identity);
			ejectObject(healthGem);
			ejectObject(manaGem);
		}

		void ejectObject(GameObject go)
		{
			GameObject newObject = (GameObject)Instantiate(go,transform.position,Quaternion.identity);

			if(newObject)
			{
				float r = shellBaseForce + Random.Range(0, shellFExtraForce);
				
				Vector2 v = Random.insideUnitCircle;
				Vector3 vec = new Vector3(v.x,1f, v.y);
				
				newObject.GetComponent<Rigidbody>().AddRelativeForce(vec * r, ForceMode.Impulse);
				
				vec = new Vector3(shellTorque.x + Random.Range(-shellTorqueRandom,shellTorqueRandom), 
				                  shellTorque.y + Random.Range(-shellTorqueRandom, shellTorqueRandom), 0);
				newObject.GetComponent<Rigidbody>().AddRelativeTorque(vec,ForceMode.Impulse);
			}
		}
	}
}
