//#define ITWEEN
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InaneGames {

	public class ChunkManager : MonoBehaviour {
		//a ref to the list picker
		private ListPicker m_picker;

		//the number of levels
		public int maxChunkIndex = 3;

		//the number of chunks per level
		public int chunksPerLevel = 10;
		public ColorTo fadeOut;
		public ColorTo fadeIn	;
		public bool loadAdditive = true;
		public int chunksToGO = 3;
		public Text text;
		private int m_chunkIndex = 1;

		public int floorOffset = 0;
		public int floorsPerFloor = 10;
		private int m_count = 0;
		private bool m_lastLevel=false;
		public bool sequentially=false;

		public string areasCleansed = "Areas Cleansed";
		public void Awake()
		{
			m_picker = new ListPicker( maxChunkIndex,sequentially);
			m_count = floorsPerFloor;
		}
		public void updateFloor(int fi)
		{
			floorOffset = fi;
			m_chunkIndex= fi * maxChunkIndex;
			if(fi>1)
			{
				m_chunkIndex++;
			}
		}
		public void OnEnable()
		{
			BaseGameManager.onLoadNextLevel += onLoadNextLevel;
		}
		public void OnDisable()
		{
			BaseGameManager.onLoadNextLevel -= onLoadNextLevel;
		}
		public void onLoadNextLevel()
		{
			
			if(m_chunkIndex>=chunksToGO)
			{
				BaseGameManager.gameover(true);
				text.text = "";

			}else{
				StartCoroutine(LoadNextChunk());
			}
		}
		public void loadFinalChunk()
		{
			m_lastLevel = true;
		}
		
		IEnumerator LoadNextChunk()
		{
			if(fadeOut)
			{	
				fadeOut.turnOn();
			}

			if(m_picker==null)
			{
				m_picker = new ListPicker(m_chunkIndex);
			}

			int levelIndex = m_picker.pickRandomIndex()+1 * (floorsPerFloor*floorOffset);
			levelIndex = Mathf.Clamp(levelIndex,1,Application.levelCount-1);
			if(m_chunkIndex==chunksToGO-1)
			{
				m_lastLevel=true;
			}

			if(m_lastLevel)
			{
				levelIndex = Application.levelCount-1;	
			}
			int leveloffset = 0;
			int chunkIndex = m_chunkIndex;
			while(chunkIndex>=chunksPerLevel)
			{
				chunkIndex-=chunksPerLevel;
				leveloffset++;
			}


			int maxLevel = InaneGames.Constants.getMaxLevel();
			if(leveloffset>maxLevel)
			{
				InaneGames.Constants.setMaxLevel(leveloffset);
			}

			m_count--;
			if(m_count==0)
			{
				floorOffset++;
				m_count  = floorsPerFloor;
			}
			if(text)
			{
				text.text =areasCleansed + " : " + m_chunkIndex + " / " + (chunksToGO-1);// + "\n" + "chunkIndex " + m_chunkIndex;
				m_chunkIndex++;
			}

			yield return new WaitForSeconds(1);
			Destroy(GameObject.Find ("Room"));
			GameObject playerGo = GameObject.Find("Player");
			playerGo.transform.position = new Vector3(1000000,0,0);

			if(loadAdditive)
			{
				Application.LoadLevelAdditive(levelIndex);
			}else{
				Application.LoadLevel(levelIndex);

			}
			yield return new WaitForSeconds(.5f	);

			if(fadeIn)
			{	
				fadeIn.turnOn();
			}

			StartLocation location = (StartLocation)GameObject.FindObjectOfType(typeof(StartLocation));
			Vector3 pos = playerGo.transform.position;
			if(location)
			{
				pos.x= location.transform.position.x;
				pos.z=location.transform.position.z;
			}else{
				pos.x= 24;
				pos.z=-27	;
			}
			playerGo.transform.position = pos;
			
			GameObject cam = GameObject.Find ("Camera");
			pos = cam.transform.position;
			pos.x=-120;
			cam.transform.position = pos;

		}

	}
}