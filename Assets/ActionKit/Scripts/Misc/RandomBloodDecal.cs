using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class RandomBloodDecal : MonoBehaviour {
		public Renderer rend;

		// Use this for initialization
		void Start () {
			transform.rotation = Quaternion.AngleAxis(Random.Range(0,360),Vector3.up);	
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}