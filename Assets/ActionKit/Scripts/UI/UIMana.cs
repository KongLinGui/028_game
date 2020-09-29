using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class UIMana : MonoBehaviour {

		public UnityEngine.UI.Text manaText;
		
		public string manaPostfix = "%";
		public string manaPrefix = "Mana:";

		private GameObject m_playerGO;
		public UnityEngine.UI.Scrollbar manaScrollbar;

		
		public UnityEngine.UI.Text pressTabText;
		private float m_oddTime;
		public float oddTime=1;
		private int m_odd=0;
		void Update () {
			if(m_playerGO==null	)
			{
				m_playerGO = GameObject.Find("Player");;

			}
			if(m_playerGO)
			{
				Damagable dam = m_playerGO.GetComponent<Damagable>();
				if(dam && manaText)
				{
					int imana = (int)(dam.getManaAsScalar() * 100f);
					blinkSpell(imana);

					manaText.text = manaPrefix + imana + " %" ;
				}
				if(manaScrollbar && dam){
					manaScrollbar.size =  dam.getManaAsScalar();
					manaScrollbar.value  = 0;

				}

			}else{

				if(manaText)
				{
					manaText.text = "";
				}
			}
			
			
		}

		void blinkSpell(int iMana)
		{
			if(iMana==100)
			{
				m_oddTime-=Time.deltaTime;
				
				if(m_oddTime<0)
				{
					if(m_odd==0)
					{
						pressTabText.text = "TIME STOP READY - PRESS TAB";
					}else{
						pressTabText.text = "";
						
					}
					m_odd^=1;
					m_oddTime = oddTime;
				}
			}else{
				pressTabText.text = "";
				
			}
		}

	}
}
