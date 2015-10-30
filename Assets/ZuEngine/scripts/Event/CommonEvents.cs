using System;
using UnityEngine;
using ZuEngine.Input;

namespace ZuEngine.Event
{
	public class CommonEvents
	{
		public const string EVENT_TOUCH = "TouchEvent"; //data is TouchEvent
		public const string EVENT_MOUSE_BUTTON = "MouseButtonEvent"; //data is ButtonEvent
		public const string EVENT_BEFORE_LEVEL_LOAD_OK = "EVENT_BEFORE_LEVEL_LOAD_OK"; // no data
		public const string EVENT_LOAD_LEVEL_OK = "EVENT_LOAD_LEVEL_OK"; // int ( level idx )

		public class TouchEvent
		{
			public TouchInput.TouchStatus Status;
			public int TouchId;
			public Vector2 PressedPosition;
			public Vector2 ReleasedPosition;
			public float HeldTime;


			public TouchEvent(TouchInput.TouchStatus status, int touchId, Vector2 pressedPosition, Vector2 releasedPosition, float heldTime)
			{
				Status = status;
				TouchId = touchId;
				PressedPosition = pressedPosition;
				ReleasedPosition = releasedPosition;
				HeldTime = heldTime;
			}
		}


		public class ButtonEvent
		{
			public MouseInput.ButtonStatus Status;
			public int ButtonId;
			public Vector2 PressedPosition;
			public Vector2 ReleasedPosition;
			public float HeldTime;
			
			
			public ButtonEvent(MouseInput.ButtonStatus status, int buttonId, Vector2 pressedPosition, Vector2 releasedPosition, float heldTime)
			{
				Status = status;
				ButtonId = buttonId;
				PressedPosition = pressedPosition;
				ReleasedPosition = releasedPosition;
				HeldTime = heldTime;
			}
		}
	}
}

