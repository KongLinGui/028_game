using UnityEngine;
using System.Collections;
using InaneGames;
namespace InaneGames {

	public class ChangeLevelScene : MonoBehaviour 
	{
		//constrain the plyer along this -axis
		public Vector2 xAxis;

		//constrain the player between this z-axis
		public Vector2 zAxis;

		//the music clip to change it to
		public AudioClip musicClip;

		//the new light color
		public Color lightColor = Color.red;
		public string playerGameObjectName = "Player";
		// Use this for initialization
		void Start () {
			IsometricPlayer player = (IsometricPlayer)GameObject.FindObjectOfType(typeof(IsometricPlayer));
			if(player)
			{
				Constraint constraint = player.GetComponent<Constraint>();
				if(constraint)
				{
					constraint.resizeXZ(xAxis,zAxis);
				}
			}

			Music music = (Music)GameObject.FindObjectOfType(typeof(Music));
			if(music)
			{
				music.playMusic(musicClip,1f);
			}
			LightChanger lc =  (LightChanger)GameObject.FindObjectOfType(typeof(LightChanger));
			if(lc)
			{
				lc.changeColor(lightColor);
			}

		}
	}
}