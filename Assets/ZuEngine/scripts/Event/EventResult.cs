using System;

namespace ZuEngine.Event
{
	public class EventResult
	{
		public readonly bool WasEaten;
		public readonly object Response;


		public EventResult(bool wasEaten, object response = null)
		{
			WasEaten = wasEaten;
			Response = response;
		}
	}
}

