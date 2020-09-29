using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class Magnet : MonoBehaviour {
		private BasePowerup[] m_powerups;

		public float distance = 10;
		public float moveSpeed = 4f;

		public void Start()
		{
		//	StartCoroutine(getPowerupListIE());

		}
		IEnumerator getPowerupListIE()
		{
			yield return new WaitForSeconds(0.1f);
			m_powerups = (BasePowerup[])GameObject.FindObjectsOfType(typeof(BasePowerup));
			StartCoroutine(getPowerupListIE());
		}


		void Update () {
		
			handleMagnet();
		}

		void handleMagnet()
		{
			m_powerups = (BasePowerup[])GameObject.FindObjectsOfType(typeof(BasePowerup));

			for(int i=0; i<m_powerups.Length; i++){
				BasePowerup pow = m_powerups[i];
				if(pow)
				{
					Vector3 vec =  transform.position - pow.transform.position;
					float d0 = vec.magnitude;
	//					Debug.Log ("Distance" + d0);
					if(d0 < distance && pow.isMagnatized)
					{
						pow.GetComponent<Rigidbody>().velocity += vec.normalized * moveSpeed;
					}
				}
			}
		}
	}
}