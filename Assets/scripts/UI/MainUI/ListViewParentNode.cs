using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Event;

public class ListViewParentNode : MonoBehaviour {
	[SerializeField]
	private UILabel m_name;
	[SerializeField]
	private ListViewHistoryBar m_historyBar;

	private MainUiParentNode m_nodeData;
	private MainUiParentNode m_parentNode;

	public void Setup( MainUiParentNode pParentNode, MainUiParentNode pNodeData )
	{
		m_nodeData = pNodeData;
		m_parentNode = pParentNode;
		m_name.text = m_nodeData.name;
	}

	public void OnClickParentNode()
	{
		ZuDebug.Log ("OnClickParentNode");
		if( m_nodeData.NextNode != null )
		{
			ServiceLocator<EventManager>.Instance.SendEvent(EventIDs.EVENT_ON_CLICK_PARENT_NODE,m_nodeData);
		}
		if( m_nodeData.Items != null )
		{
			ServiceLocator<EventManager>.Instance.SendEvent(EventIDs.EVENT_ON_CLICK_CHILD_NODE,m_nodeData);
		}
		m_historyBar.AddNode(m_parentNode,m_nodeData);
		
	}
}
