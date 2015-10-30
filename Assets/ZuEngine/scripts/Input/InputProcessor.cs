using System;
using ZuEngine.Event;
using UnityEngine;
using System.Collections.Generic;

namespace ZuEngine.Input
{
	public class InputProcessor : MonoBehaviour , IEventListener, IDestroyable
	{
		public float SwipeDistance = 0.05f;


		private class ListenerData
		{
			public IInputListener Listener;
			public int Priority;
		}

		private List<ListenerData> m_listeners;
		private bool m_swipeStarted;
		private IInputListener m_attachedListener;


		public InputProcessor ()
		{
			m_listeners = new List<ListenerData>();


		}

		void Awake()
		{
			ServiceLocator< EventManager >.Instance.RegisterListener ( CommonEvents.EVENT_MOUSE_BUTTON, this );
			ServiceLocator< EventManager >.Instance.RegisterListener ( CommonEvents.EVENT_TOUCH, this );
		}

		public void AddListener(IInputListener listener, int priority = 0)
		{
			foreach(ListenerData l in m_listeners)
			{
				// don't add duplicate listeners
				if(l.Listener == listener)
				{
					return;
				}
			}


			ListenerData ld = new ListenerData();
			ld.Listener = listener;
			ld.Priority = priority;


			m_listeners.Add(ld);

			m_listeners.Sort(
				delegate(ListenerData p1, ListenerData p2)
				{
					return p1.Priority.CompareTo(p2.Priority);
				}
			);
		}


		public void RemoveListener(IInputListener listener)
		{
			foreach(ListenerData ld in m_listeners)
			{
				if(ld.Listener == listener)
				{
					m_listeners.Remove(ld);
					break;
				}
			}
		}


		#region IDestroyable implementation
		public void Destroy()
		{
			m_listeners.Clear();

			ServiceLocator< EventManager >.Instance.UnregisterListener(CommonEvents.EVENT_MOUSE_BUTTON, this);
			ServiceLocator< EventManager >.Instance.UnregisterListener(CommonEvents.EVENT_TOUCH, this);
		}
		#endregion


		#region IEventListener implementation
		public EventResult OnEvent (string eventName, object data)
		{
			if(eventName == CommonEvents.EVENT_MOUSE_BUTTON)
			{
				CommonEvents.ButtonEvent e = data as CommonEvents.ButtonEvent;
				
				if(e.Status == MouseInput.ButtonStatus.Pressed)
				{
					OnTouchPressed(e.PressedPosition);
				}
				else if(e.Status == MouseInput.ButtonStatus.Released)
				{
					OnTouchReleased(e.PressedPosition, e.ReleasedPosition);
				}
				else if(e.Status == MouseInput.ButtonStatus.Held)
				{
					OnTouchHeld(e.PressedPosition, e.ReleasedPosition);
				}
			}
			else if(eventName == CommonEvents.EVENT_TOUCH)
			{
				CommonEvents.TouchEvent e = data as CommonEvents.TouchEvent;
				
				if(e.Status == TouchInput.TouchStatus.Pressed)
				{
					OnTouchPressed(e.PressedPosition);
				}
				else if(e.Status == TouchInput.TouchStatus.Released)
				{
					OnTouchReleased(e.PressedPosition, e.ReleasedPosition);
				}
				else if(e.Status == TouchInput.TouchStatus.Held)
				{
					OnTouchHeld(e.PressedPosition, e.ReleasedPosition);
				}
			}
			
			return null;
		}
		#endregion


		private void OnTouchPressed(Vector2 pressedPosition)
		{
			m_swipeStarted = false;
			m_attachedListener = null;


			List<GameObject> hits = GetHits(pressedPosition);
			
			foreach(ListenerData ld in m_listeners)
			{
				if(ld.Listener.OnPress(pressedPosition, hits))
				{
					break;
				}
			}
		}


		private void OnTouchReleased(Vector2 pressedPosition, Vector2 releasedPosition)
		{
			if(m_swipeStarted)
			{
				if(m_attachedListener != null)
				{
					m_attachedListener.OnSwipeReleased(pressedPosition, releasedPosition, GetHits(releasedPosition));
				}
			}
			else
			{
				List<GameObject> hits = GetHits(releasedPosition);

				foreach(ListenerData ld in m_listeners)
				{
					if(ld.Listener.OnTap(releasedPosition, hits))
					{
						break;
					}
				}
			}
		}
		
		
		private void OnTouchHeld(Vector2 pressedPosition, Vector2 releasedPosition)
		{
			if(m_swipeStarted)
			{
				if(m_attachedListener != null)
				{
					m_attachedListener.OnSwipeMoved(pressedPosition, releasedPosition, GetHits(releasedPosition));
				}
			}
			else if(CheckSwipe(pressedPosition, releasedPosition))
			{
				m_swipeStarted = true;

				foreach(ListenerData ld in m_listeners)
				{
					if(ld.Listener.OnSwipeStarted(pressedPosition, releasedPosition, GetHits(releasedPosition)))
					{
						m_attachedListener = ld.Listener;
						break;
					}
				}
			}
		}

		
		private bool CheckSwipe(Vector2 pressedPosition, Vector2 releasedPosition)
		{
			return Vector2.Distance(pressedPosition, releasedPosition) >= (Screen.width * SwipeDistance);
		}


		private List<GameObject> GetHits(Vector2 position)
		{
			RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(new Vector3(position.x, position.y, 0)));
			List<GameObject> goHits = new List<GameObject>();
			
			foreach(RaycastHit rch in hits)
			{
				goHits.Add(rch.collider.gameObject);
			}

			return goHits;
		}
	}
}

