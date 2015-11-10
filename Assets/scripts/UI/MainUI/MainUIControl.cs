using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine.UI;
using ZuEngine;
using ZuEngine.Event;

public class MainUIControl : MonoBehaviour,IEventListener {
	[SerializeField]
	private TabUIControl m_tabUIControl;
	[SerializeField]
	private ItemListView m_listViewUI;

	void Start()
	{
		ServiceLocator<EventManager>.Instance.RegisterListener (EventIDs.EVENT_ON_CLICK_PARENT_NODE, this);
		ServiceLocator<EventManager>.Instance.RegisterListener (EventIDs.EVENT_ON_CLICK_CHILD_NODE, this);
	}
	
	public void Setup()
	{
		m_tabUIControl.SetTabActiveIdx (0);
		m_listViewUI.Setup (ServiceLocator<DataManager>.Instance.mainUIData);
	}

	#region IEventListener implementation
	public EventResult OnEvent (string eventName, object data)
	{
		ZuDebug.Log("MainUIControl onEvent eventName = " + eventName);
		if( EventIDs.EVENT_ON_CLICK_PARENT_NODE == eventName )
		{
			MainUiParentNode nodeParent = data as MainUiParentNode;
			m_listViewUI.refreshParentNode(nodeParent.name, nodeParent.NextNode);
		}else if( EventIDs.EVENT_ON_CLICK_CHILD_NODE == eventName )
		{
			MainUiParentNode nodeParent = data as MainUiParentNode;
			m_listViewUI.refreshChildNode(nodeParent.name, nodeParent.Items);
		}
		return null;
	}
	#endregion
}
