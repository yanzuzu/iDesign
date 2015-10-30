using System;

namespace ZuEngine
{
	public class Timer
	{
		public delegate void OnTimerUp(Timer timer);

		public float TimePassed { get; private set; }
		public float TargetTime { get; private set; }
		public bool IsDone { get { return TimePassed >= TargetTime; } }
		public object Data { get; set; }

		public float TimeLeft { get { return Math.Max(0.0f, TargetTime - TimePassed); } }

		private bool m_firedCallback;
		private OnTimerUp m_callback;

		public Timer(float targetTime, object data = null, OnTimerUp callback = null)
		{
			TimePassed = 0.0f;
			TargetTime = targetTime;
			m_firedCallback = false;
			Data = data;
			m_callback = callback;
		}


		public static string TimeToString(float time)
		{
			int secs = (int)time % 60;
			int mins = (int)time / 60;
			return String.Format("{0}:{1:00}", mins, secs); 
		}


		public void Update(float deltaTime)
		{
			TimePassed += deltaTime;

			if(TimePassed >= TargetTime && !m_firedCallback)
			{
				m_firedCallback = true;

				if(m_callback != null)
				{
					m_callback(this);
				}
			}
		}


		public static long EpocTime()
		{
			DateTime JanFirst1970 = new DateTime(1970, 1, 1);
			return (long)((DateTime.Now.ToUniversalTime() - JanFirst1970).TotalMilliseconds + 0.5);
		}
	}
}

