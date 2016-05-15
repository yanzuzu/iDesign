using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.StateManagement;
using ZuEngine.Input;
using ZuEngine.Event;

public class FurnitureTask : Task , IInputListener {

	private Furniture m_hitFurniture;
	private float m_diffCamDist = 0;
	private int m_isTurnItem = 0;

	public FurnitureTask()
	{
		ListenForEvent (EventIDs.EVENT_TURN_RIGHT, OnTurnRight);
		ListenForEvent (EventIDs.EVENT_TURN_LEFT, OnTurnLeft);
		ListenForEvent (EventIDs.EVENT_DELETE_ITEM, OnDeleteItem);
	}

	public override void Pause()
	{
		ServiceLocator< InputProcessor >.Instance.RemoveListener (this);

	}

	public override void Resume()
	{
		ServiceLocator< InputProcessor >.Instance.AddListener (this);

	}

	public override void Show(bool show)
	{

	}

	public override void Update(float deltaTime)
	{
		if (m_isTurnItem != 0 && m_hitFurniture != null) {
			m_hitFurniture.Rotate (m_isTurnItem);
		}
	}

	private EventResult OnDeleteItem(string name , object args)
	{
		if (m_hitFurniture != null){
			m_hitFurniture.OnReleaseItem ();
			m_hitFurniture.DestroyItem ();
			m_hitFurniture = null;
		}
		return null;
	}
	private EventResult OnTurnRight(string name , object args)
	{
		return TurnItem(1,args);
	}

	private EventResult OnTurnLeft(string name , object args)
	{
		return TurnItem(-1,args);
	}

	private EventResult TurnItem(int turnValue , object args )
	{
		m_isTurnItem = 0;
		if (m_hitFurniture == null)
			return null;
		bool isPress =  (bool)args;
		if (!isPress) {
			m_isTurnItem = 0;
		} else {
			m_isTurnItem = turnValue;
		}
		return null;
	}

	#region implement Input
	//called when a touch starts, returning true will eat the input
	public bool OnPress(Vector2 position, List<GameObject> hitObjects)
	{
		bool isHit = false;
		if (m_hitFurniture != null) {
			//m_hitFurniture.Rigid.constraints = RigidbodyConstraints.None;
			m_hitFurniture.OnReleaseItem ();
			m_hitFurniture = null;
			ServiceLocator<EventManager>.Instance.SendEvent (EventIDs.EVENT_SAVE_FURNITURE);
		}
			
		for( int i = 0 ; i < hitObjects.Count ; i ++ )
		{
			Furniture hitObj = hitObjects[i].GetComponent< Furniture >();
			if( !hitObj ) continue;
			m_hitFurniture = hitObj;
			m_hitFurniture.OnClickItem ();
			return true;
		}
		m_hitFurniture = null;
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
		if (m_hitFurniture)
		{
			//m_hitFurniture.Rigid.constraints = RigidbodyConstraints.FreezeAll;
			m_diffCamDist = Vector3.Distance(Camera.main.transform.position ,m_hitFurniture.CacheTrans.position );
			return true;
		}
		return false;
	}
	
	public void OnSwipeMoved(Vector2 startPosition, Vector2 currentPosition, List<GameObject> hitObjects)
	{
		if (!Camera.main)
			return;
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(currentPosition.x,currentPosition.y,m_diffCamDist));
		m_hitFurniture.gameObject.transform.position = new Vector3( pos.x , m_hitFurniture.CacheTrans.position.y, pos.z );
	}

	public void OnSwipeReleased(Vector2 startPosition, Vector2 endPosition, List<GameObject> hitObjects)
	{
		
	}
	#endregion

}
