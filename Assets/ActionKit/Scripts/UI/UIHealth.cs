using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class UIHealth : MonoBehaviour {
		public UnityEngine.UI.Text healthText;

		public string healthPostfix = "%";
		public string healthPrefix = "Health:";
		public UnityEngine.UI.Scrollbar healthScrollbar;
		public string objectToFind = "Player";
		private GameObject m_playerGO;


		void Update () {
			if(m_playerGO==null)
			{
				m_playerGO = GameObject.Find(objectToFind);;

			}


			if(m_playerGO)
			{
				Damagable dam = m_playerGO.GetComponent<Damagable>();
				if(dam && healthText)
				{
					healthText.text = healthPrefix +  dam.getHealthSTR();
				}
				if(healthScrollbar && dam){
					healthScrollbar.size = dam.getHealthAsScalar();
					healthScrollbar.value  = 0;

				}
			}else{
				if( healthText)
				{
					healthText.text = "Dead";
					Destroy(gameObject,1);
				}
			}
			
			
		}
		

	}
}