using UnityEngine;
using System.Collections;
namespace InaneGames
{
	public class DontDestroyOnWake : MonoBehaviour {

		// Use this for initialization
		void Start () {

			DontDestroyOnLoad(gameObject);

		}
		

	}
}
