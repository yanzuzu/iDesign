using System;
using ZuEngine.Event;
using System.Collections.Generic;

namespace ZuEngine.StateManagement
{
	public abstract class Task : IDestroyable, IUpdatable, IEventListener
	{
		public delegate EventResult EventCallback(string eventName, object eventData);


		public TaskManager TaskManager;
		public StateManager StateManager { get { return TaskManager.StateManager; } }


		protected class EventListenerData
		{
			public EventCallback Callback;
			public bool CallWhenInactive;
		}


		protected Dictionary<string, EventListenerData> m_eventListeners;


		public Task()
		{
			m_eventListeners = new Dictionary<string, EventListenerData>();
		}


		public void ListenForEvent(string eventName, EventCallback callback, bool callWhenInactive = false, int priority = 0)
		{
			EventListenerData eld = new EventListenerData();
			eld.Callback = callback;
			eld.CallWhenInactive = callWhenInactive;

			m_eventListeners[eventName] = eld;
			ServiceLocator<EventManager>.Instance.RegisterListener (eventName, this, priority);
		}


		public bool Active
		{ 
			get
			{ 
				return m_active; 
			}

			set 
			{
				m_active = value;

				if(m_active)
				{
					Resume();
				}
				else
				{
					Pause();
				}
			}
		}


		public bool Visible
		{ 
			get
			{ 
				return m_visible;
			}
			
			set 
			{
				m_visible = value;
				Show(m_visible);
			}
		}


		public bool AlwaysUpdates = false;


		#region IEventListener implementation
		public EventResult OnEvent(string eventName, object data)
		{
			if(m_eventListeners.ContainsKey(eventName))
			{
				EventListenerData eld = m_eventListeners[eventName];

				if(!Active && !eld.CallWhenInactive)
				{
					return null;
				}

				if(eld.Callback != null)
				{
					return eld.Callback(eventName, data);
				}
			}
			
			
			return null;
		}
		#endregion


		private bool m_active;
		private bool m_visible;



		public abstract void Pause();
		public abstract void Resume();
		public abstract void Show(bool show);
		public abstract void Update(float deltaTime);

		public virtual void Destroy()
		{
			foreach(string eventName in m_eventListeners.Keys)
			{
				ServiceLocator<EventManager>.Instance.UnregisterListener(eventName, this);
			}
			m_eventListeners.Clear();
		}
	}
}

