using System;
using UnityEngine;
using ZuEngine.Event;

namespace ZuEngine.Input
{
	public class MouseInput
	{
		public enum ButtonStatus
		{
			None,
			Pressed,
			Held,
			Released
		}

		private Vector2 m_pressedLocation;
		private Vector2 m_releasedLocation;

		public ButtonStatus Status { get; private set; }
		public Vector2 PressedLocation { get { return m_pressedLocation; } }
		public Vector2 ReleasedLocation { get { return m_releasedLocation; } }
		public float HeldTime { get; private set; }


		public MouseInput()
		{
			Status = ButtonStatus.None;
			m_pressedLocation = Vector3.zero;
			m_releasedLocation = Vector3.zero;
			HeldTime = 0.0f;
		}


		public void Update(float deltaTime)
		{
			if(UnityEngine.Input.GetMouseButtonDown(0))
			{
				Status = ButtonStatus.Pressed;
				m_pressedLocation.x = UnityEngine.Input.mousePosition.x;
				m_pressedLocation.y = UnityEngine.Input.mousePosition.y;

				SendEvent(0);

				return;
			}
			else if(UnityEngine.Input.GetMouseButtonUp(0))
			{
				Status = ButtonStatus.Released;
				m_releasedLocation.x = UnityEngine.Input.mousePosition.x;
				m_releasedLocation.y = UnityEngine.Input.mousePosition.y;
				
				SendEvent(0);
				
				return;
			}


			if(Status == ButtonStatus.Pressed)
			{
				Status = ButtonStatus.Held;
				HeldTime = deltaTime;
				m_releasedLocation.x = UnityEngine.Input.mousePosition.x;
				m_releasedLocation.y = UnityEngine.Input.mousePosition.y;

				SendEvent(0);
			}
			else if(Status == ButtonStatus.Released)
			{
				Status = ButtonStatus.None;
			}
			else if(Status == ButtonStatus.Held)
			{
				HeldTime += deltaTime;
				m_releasedLocation.x = UnityEngine.Input.mousePosition.x;
				m_releasedLocation.y = UnityEngine.Input.mousePosition.y;
				SendEvent(0);
			}
		}
		
		
		private void SendEvent(int touchId)
		{
			ServiceLocator<EventManager>.Instance.SendEvent(CommonEvents.EVENT_MOUSE_BUTTON, new CommonEvents.ButtonEvent(Status, touchId, PressedLocation, ReleasedLocation, HeldTime));
		}
	}
}

