using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace InaneGames {

	public class FloatingTextManager : MonoBehaviour {
		public FloatingText floatingtext;

		public void OnDisable()
		{
			BaseGameManager.onFloatText-= onFloatText;
		}
		public void OnEnable()
		{
			BaseGameManager.onFloatText += onFloatText;
		}
		public static void pushFloatingText(string str, Color color)
		{
			GameObject go = GameObject.Find("Player");
			if(go)
			{
				BaseGameManager.floatText(str,go,color);
			}
		}
		

		public  void onFloatText(string str,
		                          GameObject obj,
		                           Color color)
		{
			GameObject go = (GameObject)Instantiate(floatingtext.gameObject,
			                                        transform.position,Quaternion.identity);
			if(go && GameObject.Find("Canvas"))
			{
				go.transform.SetParent(GameObject.Find("Canvas").transform);
				FloatingText ft = go.GetComponent<FloatingText>();
				if(ft)
				{
					ft.init(obj.transform.position,obj.transform.position+new Vector3(0,160,0),str,color);
				}
			}
		}
	}
}