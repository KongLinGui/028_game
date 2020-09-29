using UnityEngine;
using System.Collections;

namespace InaneGames
{
	public class BackgroundScene : MonoBehaviour {
		private static GameObject K_SCENE;
		// Use this for initialization
		void Start () {

			if(K_SCENE)
			{
				Destroy(K_SCENE);
			}
			DontDestroyOnLoad(gameObject);
			K_SCENE=gameObject;
		}
		
		
	}
}
