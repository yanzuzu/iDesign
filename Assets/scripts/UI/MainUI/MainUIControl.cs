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
			List<MainUiParentNode> nodes = data as List<MainUiParentNode>;
			m_listViewUI.refreshParentNode(nodes);
		}else if( EventIDs.EVENT_ON_CLICK_CHILD_NODE == eventName )
		{
			List<UiItemData> nodes = data as List<UiItemData>;
			m_listViewUI.refreshChildNode(nodes);
		}
		return null;
	}
	#endregion
}
