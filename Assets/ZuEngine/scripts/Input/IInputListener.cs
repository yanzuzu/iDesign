using System;
using UnityEngine;
using System.Collections.Generic;

namespace ZuEngine.Input
{
	public interface IInputListener
	{
		//called when a touch starts, returning true will eat the input
		bool OnPress(Vector2 position, List<GameObject> hitObjects);

		// return true if the event is eaten, false to passthrough to other listeners
		bool OnTap(Vector2 position, List<GameObject> hitObjects);

		//return true to be the owner of the swipe, others will not recieve the moved/released
		bool OnSwipeStarted(Vector2 startPosition, Vector2 currentPosition, List<GameObject> hitObjects);

		void OnSwipeMoved(Vector2 startPosition, Vector2 currentPosition, List<GameObject> hitObjects);

		void OnSwipeReleased(Vector2 startPosition, Vector2 endPosition, List<GameObject> hitObjects);
	}
}

