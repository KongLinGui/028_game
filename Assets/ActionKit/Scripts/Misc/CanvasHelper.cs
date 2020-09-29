using UnityEngine;
using System.Collections;
namespace InaneGames {

	public static class CanvasHelper
	{

		public static Vector2 WorldToCanvas( Vector3 world_position,
		                                    Camera camera = null)
		{
			Canvas canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
			if (camera == null)
			{
				camera = Camera.main;
			}
			
			Vector3 viewport_position = camera.WorldToViewportPoint(world_position);
			RectTransform canvas_rect = canvas.GetComponent<RectTransform>();
			
			return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f),
			                   (viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
		}
	}
}