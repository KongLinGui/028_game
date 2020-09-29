using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class GameoverAnimations : MonoBehaviour {
		public GameObject[] objects;
		// Use this for initialization
		IEnumerator Start () {
			for(int i=0; i<objects.Length; i++)
			{
				objects[i].SetActive(true);
				yield return new WaitForSeconds(1);

				GetComponent<AudioSource>().Play();
			}
		}

	}
}