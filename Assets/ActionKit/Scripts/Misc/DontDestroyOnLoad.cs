using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class DontDestroyOnLoad : MonoBehaviour {

		// Use this for initialization
		void Start () {
			DontDestroyOnLoad(gameObject);
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}