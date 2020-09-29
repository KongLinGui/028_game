using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class Music : MonoBehaviour {

		public AudioClip musicAC;
		private static AudioSource K_MUSIC;

		public void Start()
		{
			if(K_MUSIC==null)
			{
				GameObject go = new GameObject();
				K_MUSIC = go.AddComponent<AudioSource>();
				K_MUSIC.loop=true;
				DontDestroyOnLoad(K_MUSIC);
				playMusic(musicAC);
			}else
			{
				playMusic(musicAC);

			}

		}
		public void setPitch(float pitch)
		{
			if(K_MUSIC)
			{
				K_MUSIC.pitch = pitch;
			}
		}

		public void playMusic(AudioClip ac,float pitch=1f)
		{
			if(K_MUSIC)
			{
				K_MUSIC.pitch = pitch;
				K_MUSIC.clip = ac	;
				K_MUSIC.Play();
			}
		}


	}
}