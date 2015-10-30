using System;
using System.Collections.Generic;

namespace ZuEngine
{
	public class TimerManager : IUpdatable
	{
		private List<Timer> m_timers;


		public TimerManager()
		{
			m_timers = new List<Timer>();
		}


		public void Add(Timer timer)
		{
			m_timers.Add(timer);
		}


		public void Schedule(float duration, Timer.OnTimerUp callback = null, object data = null)
		{
			m_timers.Add(new Timer(duration, data, callback));
		}


		public void Update(float deltaTime)
		{
			foreach(Timer t in m_timers.ToArray())
			{
				t.Update(deltaTime);

				if(t.IsDone)
				{
					m_timers.Remove(t);
				}
			}
		}
	}
}

