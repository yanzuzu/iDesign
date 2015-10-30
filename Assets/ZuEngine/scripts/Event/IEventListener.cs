using System;

namespace ZuEngine.Event
{
	public interface IEventListener
	{
		//return true if you wish to "eat" the event
		EventResult OnEvent(string eventName, object data);
	}
}

