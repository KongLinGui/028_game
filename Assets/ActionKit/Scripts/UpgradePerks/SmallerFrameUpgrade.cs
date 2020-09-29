using UnityEngine;
using System.Collections;
namespace InaneGames {

	public class SmallerFrameUpgrade : BaseUpgrade {
		public float radius = 2.0f * 0.9f;
		
		public override void init()
		{
			CharacterController character = gameObject.GetComponentInParent<CharacterController>();
			if(character)
			{
				character.radius = radius;
			}
		}
	}
}