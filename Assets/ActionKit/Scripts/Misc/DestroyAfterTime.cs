using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class DestroyAfterTime : MonoBehaviour {
		public float timeToLive  = 5;
		// Use this for initialization
		void Start () {
			Destroy(gameObject,timeToLive);
		}
		

	}
}