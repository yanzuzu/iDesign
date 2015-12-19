using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine.Input;
using ZuEngine;
using ZuEngine.Event;
using ZuEngine.Manager;

public class TwoDMapController : MonoBehaviour , IInputListener, IEventListener {
	#region SerailzeField
	[SerializeField]
	private Camera UiCamera;
	#endregion

	#region private member
	private BuildData m_selectBuildData;
	private WallController wallCtrl;
	#endregion

	void Start()
	{
		ServiceLocator< InputProcessor >.Instance.AddListener (this);
		ServiceLocator< EventManager >.Instance.RegisterListener (EventIDs.EVENT_ON_CLICK_UNIT_TYPE, this);
		wallCtrl = new WallController ();
	}
	
	void Update()
	{
		
	}
	#region Implement Event
	public EventResult OnEvent(string eventName, object data)
	{
		if( eventName == EventIDs.EVENT_ON_CLICK_UNIT_TYPE )
		{
			m_selectBuildData = ( BuildData )data;

		}
		return null;
	}
	#endregion
	#region Implement IInputListener
	//called when a touch starts, returning true will eat the input
	public bool OnPress(Vector2 position, List<GameObject> hitObjects)
	{
		if( m_selectBuildData == null )
		{
			return false;
		}
		ZuDebug.Log ("m_selectBuildData type = " + m_selectBuildData.type);
		switch( m_selectBuildData.type )
		{
		case UnitType.WALL:
			wallCtrl.CreateWall(m_selectBuildData,this.gameObject,position);
			break;
		}
		return false;
	}
	
	// return true if the event is eaten, false to passthrough to other listeners
	public bool OnTap(Vector2 position, List<GameObject> hitObjects)
	{
		return false;
	}
	
	//return true to be the owner of the swipe, others will not recieve the moved/released
	public bool OnSwipeStarted(Vector2 startPosition, Vector2 currentPosition, List<GameObject> hitObjects)
	{
		return m_selectBuildData != null;
	}
	
	public void OnSwipeMoved(Vector2 startPosition, Vector2 currentPosition, List<GameObject> hitObjects)
	{
		switch( m_selectBuildData.type )
		{
		case UnitType.WALL:
			wallCtrl.ScaleWall (startPosition, currentPosition);
			break;
		}
	}
	
	public void OnSwipeReleased(Vector2 startPosition, Vector2 endPosition, List<GameObject> hitObjects)
	{
		//Debug.Log ("OnSwipeReleased");
	}
	#endregion
}
