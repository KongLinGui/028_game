using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class ChunkLoader : MonoBehaviour {
		public int levelOffset = 0;
		public Teleporter teleporter;
		public bool debug=false;
		public bool isFinalChunk = false;
		private bool m_loadFinalChunk=false;
		public void Start	()
		{
			teleporter = gameObject.GetComponent<Teleporter>();	
			if(levelOffset > InaneGames.Constants.getMaxLevel() && debug==false)
			{
				teleporter.disabled();
			}	
		}
		public void loadNextChunkCBF()
		{
			ChunkManager cm = (ChunkManager)GameObject.FindObjectOfType(typeof(ChunkManager));
			if(cm)
			{
				cm.updateFloor(levelOffset);

				if(isFinalChunk && m_loadFinalChunk==false)
				{
					cm.loadAdditive=false;
				}
			}
			PlayerLevel ok = (PlayerLevel)GameObject.FindObjectOfType(typeof(PlayerLevel));
			if(ok)
			{
		//		ok.setLevel(levelOffset*2);
				
			}

		}
		
	}
}