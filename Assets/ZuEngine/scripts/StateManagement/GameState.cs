using System;
using System.Collections.Generic;
using ZuEngine.Event;
using ZuEngine;

namespace ZuEngine.StateManagement
{
	public abstract class GameState : IDestroyable, IUpdatable, IEventListener
	{
		public delegate EventResult EventCallback(string eventName, object eventData);


		public StateManager StateManager { get; private set; }
		public TaskManager TaskManager { get; private set; }
		public bool HasBeenInited { get; set; }

		protected Dictionary<string, EventCallback> m_eventListeners;


		public GameState(StateManager statemanager)
		{
			StateManager = statemanager;
			TaskManager = new TaskManager(statemanager);
			HasBeenInited = false;
			m_eventListeners = new Dictionary<string, EventCallback>();
		}


		#region IEventListener implementation
		public EventResult OnEvent(string eventName, object data)
		{
			if(m_eventListeners.ContainsKey(eventName))
			{
				EventCallback callback = m_eventListeners[eventName];

				if(callback != null)
				{
					return callback(eventName, data);
				}
			}


			return null;
		}
		#endregion


		public void ListenForEvent(string eventName, EventCallback callback, int priority = 0)
		{
			m_eventListeners[eventName] = callback;
			ServiceLocator<EventManager>.Instance.RegisterListener (eventName, this, priority);
		}


		public abstract void Init();


		public virtual void Update(float deltaTime)
		{
			TaskManager.Update(deltaTime);
		}


		public virtual void Destroy()
		{
			foreach(string eventName in m_eventListeners.Keys)
			{
				ServiceLocator<EventManager>.Instance.UnregisterListener(eventName, this);
			}
			m_eventListeners.Clear();

			TaskManager.Destroy();
		}
	}
}

