using System;
using UnityEngine;
using ZuEngine.Event;

namespace ZuEngine.Input
{
	public class TouchInput
	{
		public enum TouchStatus
		{
			None,
			Pressed,
			Held,
			Released
		}


		public TouchStatus Status { get; private set; }
		public Vector2 PressedLocation { get; private set; }
		public Vector2 ReleasedLocation { get; private set; }
		public float HeldTime { get; private set; }

		public int m_lastFingerId;

		public TouchInput()
		{
			Status = TouchStatus.None;
			PressedLocation = Vector3.zero;
			ReleasedLocation = Vector3.zero;
			HeldTime = 0.0f;
		}


		public void Update(float deltaTime)
		{
			if(UnityEngine.Input.touchCount == 0)
			{
				Status = TouchStatus.None;
				return;
			}

			Touch touch = UnityEngine.Input.GetTouch(0);

			//if you currently aren't pressing the screen then grab the first finger
			if(Status == TouchStatus.None)
			{
				m_lastFingerId = touch.fingerId;
			}

			bool foundTouch = false;

			//find the right touch for the finger id
			for(int i = 0; i < UnityEngine.Input.touchCount; i++)
			{
				if(UnityEngine.Input.GetTouch(i).fingerId == m_lastFingerId)
				{
					touch = UnityEngine.Input.GetTouch(i);
					foundTouch = true;
				}
			}


			if(!foundTouch)
			{
				return;
			}
		


			if(touch.phase == TouchPhase.Began)
			{
				Status = TouchStatus.Pressed;
				HeldTime = 0.0f;
				PressedLocation = touch.position;
				ReleasedLocation = touch.position;
				SendEvent(0);
			}
			else if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
			{
				Status = TouchStatus.Released;
				ReleasedLocation = touch.position;
				SendEvent(0);
			}
			else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{
				Status = TouchStatus.Held;
				HeldTime += deltaTime;
				ReleasedLocation = touch.position;
				SendEvent(0);
			}
			else
			{
				Status = TouchStatus.None;
			}
		}


		private void SendEvent(int touchId)
		{
			ServiceLocator< EventManager >.Instance.SendEvent( CommonEvents.EVENT_TOUCH, new CommonEvents.TouchEvent(Status, touchId, PressedLocation, ReleasedLocation, HeldTime));
		}
	}
}

