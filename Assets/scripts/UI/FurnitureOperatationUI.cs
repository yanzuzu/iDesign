using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Event;
public class FurnitureOperatationUI : MonoBehaviour {

	public void PressTurnRight()
	{
		ServiceLocator< EventManager >.Instance.SendEvent (EventIDs.EVENT_TURN_RIGHT, true);
	}

	public void ReleaseTurnRight()
	{
		ServiceLocator< EventManager >.Instance.SendEvent (EventIDs.EVENT_TURN_RIGHT, false);
	}

	public void PressTurnLeft()
	{
		ServiceLocator< EventManager >.Instance.SendEvent (EventIDs.EVENT_TURN_LEFT, true);
	}

	public void ReleaseTurnLeft()
	{
		ServiceLocator< EventManager >.Instance.SendEvent (EventIDs.EVENT_TURN_LEFT, false);
	}
}
