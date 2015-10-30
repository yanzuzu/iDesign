using System;

namespace ZuEngine.Input
{
	public class InputManager
	{
		public TouchInput Touch { get; private set; }
		public MouseInput Mouse { get; private set; }


		public InputManager()
		{
			//only enable touch input for mobile devices
			if(UnityEngine.Application.platform == UnityEngine.RuntimePlatform.Android || 
				UnityEngine.Application.platform == UnityEngine.RuntimePlatform.IPhonePlayer ||
				UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WP8Player)
			{
				Touch = new TouchInput();
				Mouse = null;
			}
			else
			{
				Mouse = new MouseInput();
				Touch = null;
			}
		}


		public void Update(float deltaTime)
		{
			if(Touch != null)
			{
				Touch.Update(deltaTime);
			}

			if(Mouse != null)
			{
				Mouse.Update(deltaTime);
			}
		}
	}
}

