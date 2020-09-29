using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class StartLocation : MonoBehaviour {

		// Use this for initialization
		void OnDrawGizmos() {
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position, 11);
		}
	}
}