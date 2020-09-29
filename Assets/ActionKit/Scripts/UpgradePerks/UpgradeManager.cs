using UnityEngine;
using System.Collections;
using InaneGames;

namespace InaneGames {

	public class UpgradeManager : MonoBehaviour {
		public enum UpgradeType
		{
			Bloodlust,			//chance to heal you when you kill enemies		-1
			HealthBoost			//Increases the max health						-2

		};
		private ListPicker m_picker; 
		public BaseUpgrade[] upgrades;
		public int initalPerk=-1;
		public void Awake()
		{
			m_picker = new ListPicker(upgrades.Length);
			m_picker.selectIndex(initalPerk);
			if(initalPerk!=-1)
			{
				upgrade(initalPerk);
			}

		}
		public void OnEnable()
		{
			BaseGameManager.onUpgrade += onUpgrade;

		}

		public void OnDisable()
		{
			BaseGameManager.onUpgrade -= onUpgrade;
		}

		public void onUpgrade(int levelIndex)
		{
			int index = m_picker.pickRandomIndex();
			upgrade(index);
		}

		void upgrade(int index)
		{
	//		ScoreFlash.Push(upgrades[index].gameObject.name);
			FloatingTextManager.pushFloatingText("Leveled Up: " + upgrades[index].gameObject.name,Color.red);	
			GameObject go = (GameObject)Instantiate( upgrades[index].gameObject,Vector3.zero,Quaternion.identity);
			if(go)
			{
				go.transform.parent = transform;
			}
		}

	}
}
