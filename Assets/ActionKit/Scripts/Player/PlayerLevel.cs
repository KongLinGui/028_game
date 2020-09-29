using UnityEngine;
using System.Collections;
using InaneGames;
using UnityEngine.UI;

namespace InaneGames {

	public class PlayerLevel : MonoBehaviour {

		//an array of levels...
		public int[] level;
		//the current level
		public int currentLevel=0;
		//the current exp
		public int currentExp = 0;

		//a ref to the scrollbar
		public Scrollbar bar;

		//a ref to the text
		public Text text;


		public void Awake()
		{
			updateBar(level[currentLevel]);
		}
		public void OnDisable()
		{
			BaseGameManager.onEnemyDie -= onEnemyDie;
		}
		public void OnEnable()
		{
			BaseGameManager.onEnemyDie += onEnemyDie;
		}
		public void setLevel(int m)
		{
			currentLevel=m;
			for(int i=0; i<m; i++)
			{
				BaseGameManager.upgradeLevel(i);
			}
			increaseExp(0);
		}
		void onEnemyDie (GameObject go) {
			ZombieEnemy zombieEnemy = go.GetComponent<ZombieEnemy>();
			if(zombieEnemy)
			{

				increaseExp(zombieEnemy.exp);
			}
		}
		void increaseExp(int exp)
		{
			currentExp += exp;
			int cost = level[currentLevel];
			if(currentExp>=cost)
			{
				if(currentLevel < level.Length-1)
				{
					currentExp-=cost;
					currentLevel++;		
					BaseGameManager.upgradeLevel(currentLevel);
				}
			}
			updateBar(cost);

		}
		void updateBar(int cost)
		{
			if(bar)
			{
				float val = (float)currentExp / (float)cost;
				bar.size = val;
				bar.value = 0;
			}
			if(text)
			{
				text.text = "Level: " + (currentLevel+1);
			}
		}

	}
}
