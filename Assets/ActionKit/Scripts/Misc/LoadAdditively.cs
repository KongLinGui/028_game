using UnityEngine;
using System.Collections;
namespace InaneGames
{
	public class LoadAdditively : MonoBehaviour {
		public bool loadAdditively=true;
		// Use this for initialization
		void Start () {
			ChunkManager cm = (ChunkManager)GameObject.FindObjectOfType(typeof(ChunkManager));
			if(cm)
			{
				cm.loadAdditive=loadAdditively;
			}
		}
		
	}
}
