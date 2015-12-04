using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine.Input;
using ZuEngine;
using ZuEngine.Event;
using ZuEngine.Manager;

public class TwoDMapController : MonoBehaviour , IInputListener, IEventListener {
	[SerializeField]
	private Camera UiCamera;

	private BuildData m_selectBuildData;

	void Start()
	{
		ServiceLocator< InputProcessor >.Instance.AddListener (this);
		ServiceLocator< EventManager >.Instance.RegisterListener (EventIDs.EVENT_ON_CLICK_UNIT_TYPE, this);
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
		GameObject GameObj = ServiceLocator<ResourceManager>.Instance.LoadRes (m_selectBuildData.ResourcePath, true);
		GameObj.transform.SetParent (this.gameObject.transform);
		GameObj.transform.localScale = Vector3.one;
		GameObj.GetComponent<UISprite> ().depth = 1;
		Vector3 wordPos = UiCamera.ScreenToWorldPoint (new Vector3( position.x, position.y , 0 ));
		GameObj.transform.position = wordPos;
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
		return false;
	}
	
	public void OnSwipeMoved(Vector2 startPosition, Vector2 currentPosition, List<GameObject> hitObjects)
	{
	}
	
	public void OnSwipeReleased(Vector2 startPosition, Vector2 endPosition, List<GameObject> hitObjects)
	{
	}
	#endregion
}
