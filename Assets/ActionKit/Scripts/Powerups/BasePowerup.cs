using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class BasePowerup : MonoBehaviour {

		public bool isMagnatized = true;
		private bool m_oneTime=false;
		public float ttl= 10;
		

		public AudioClip pickupAC;
		public GameObject createOnPickup;

		public virtual void handlePowerup(GameObject go){}
		public void OnTriggerEnter(Collider col)
		{
			if(col.name.Contains("Player") )
			{
				if(createOnPickup)
					Instantiate(createOnPickup,transform.position,Quaternion.identity);
				BaseGameManager.pickUpPowerup(this);
				handlePowerup(col.gameObject);
				removeMe();
			}
		}
		
		public void Update()
		{
			ttl -= Time.deltaTime;
			
			//dont go below this point.
			Vector3 pos = transform.position;
			if(pos.y<0)
			{
				pos.y=0;
			}
			transform.position = pos;
			
			//fade out.
			if(ttl<=1 && m_oneTime==false)
			{	

				m_oneTime=true;
			}
			if(ttl<0)
			{
				removeMe();
			}
		}
		
			
		void removeMe()
		{
			Destroy(gameObject);
		}
	}
}