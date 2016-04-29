using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.StateManagement;
using ZuEngine.Input;

public class FurnitureTask : Task , IInputListener {

	private Furniture m_hitFurniture;
	private float m_diffCamDist = 0;

	public FurnitureTask()
	{

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

	}
	
	#region implement Input
	//called when a touch starts, returning true will eat the input
	public bool OnPress(Vector2 position, List<GameObject> hitObjects)
	{
		bool isHit = false;
		for( int i = 0 ; i < hitObjects.Count ; i ++ )
		{
			Furniture hitObj = hitObjects[i].GetComponent< Furniture >();
			if( !hitObj ) continue;
			m_hitFurniture = hitObj;
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
			m_hitFurniture.Rigid.constraints = RigidbodyConstraints.FreezeAll;
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
		m_hitFurniture.Rigid.constraints = RigidbodyConstraints.None;
		m_hitFurniture = null;
	}
	#endregion

}
