using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.Event
{
	public class EventManager:MonoBehaviour
	{
		public delegate void OnEventSendDelegate(string eventName, object data);

		public OnEventSendDelegate OnEventSentCallback { get; set; }


		private class ListenerContainer
		{
			public IEventListener Listener { get; private set; }
			public int Priority { get; private set; }

			public ListenerContainer(IEventListener listener, int priority)
			{
				Priority = priority;
				Listener = listener;
			}
		}


		private Dictionary<string, List<ListenerContainer>> m_eventListeners;


		public EventManager()
		{
			OnEventSentCallback = null;
			m_eventListeners = new Dictionary<string, List<ListenerContainer>>();
		}

		//higher priority numbers get sent the event first
		public void RegisterListener(string eventName, IEventListener listener, int priority = 0)
		{
			if(!m_eventListeners.ContainsKey(eventName))
			{
				m_eventListeners[eventName] = new List<ListenerContainer>();
			}

			List<ListenerContainer> listeners = m_eventListeners[eventName];

			foreach(ListenerContainer l in listeners)
			{
				if(l.Listener == listener)
				{
					UnityEngine.Debug.LogException(new Exception("Listener is already registered for this object!"));
					return;
				}
			}

			listeners.Add(new ListenerContainer(listener, priority));
			Comparison<ListenerContainer> c = delegate(ListenerContainer lc1, ListenerContainer lc2){ return lc2.Priority.CompareTo(lc1.Priority); };
			listeners.Sort(c);
		}


		public void UnregisterListener(string eventName, IEventListener listener)
		{
			if(m_eventListeners.ContainsKey(eventName))
			{
				foreach(ListenerContainer l in m_eventListeners[eventName].ToArray())
				{
					if(l.Listener == listener)
					{
						m_eventListeners[eventName].Remove(l);
						return;
					}
				}
			}
		}


		public EventResult SendEvent(string eventName, object data = null)
		{
			if(OnEventSentCallback != null)
			{
				OnEventSentCallback(eventName, data);
			}

			if(m_eventListeners.ContainsKey(eventName))
			{
                //clone listeners before iteration to allow removing a listener in the event handler
                foreach(ListenerContainer lc in m_eventListeners[eventName].ToArray())
				{
					EventResult result = lc.Listener.OnEvent(eventName, data);

					if(result == null)
					{
						continue;
					}

					if(result.WasEaten)
					{
						return result;
					}
				}
			}

			return null;
		}
	}
}

