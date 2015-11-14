using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Manager;
using ZuEngine.Event;

public class HistoryNode : MonoBehaviour {
	[SerializeField]
	private MyUILabel m_nodeTxt;
	[SerializeField]
	private BoxCollider m_collid;
	[SerializeField]
	private ListViewHistoryBar historyBar;

	MainUiParentNode m_parentNode;
	int m_nodeIdx = -1;
	public void Setup(string pShowName, MainUiParentNode pParentNode, int pNodeIdx)
	{
		m_nodeTxt.text = pShowName;
		m_parentNode = pParentNode;
		m_nodeIdx = pNodeIdx;
	}

	public void Resize(int reSize)
	{
		m_collid.size = new Vector3 (reSize, m_collid.size.y, m_collid.size.z);
	}

	public void OnClickHitoryNode()
	{
		ZuDebug.Log (string.Format("OnClickHistoyNode name = {0}",m_nodeTxt.text));
		historyBar.subNodes (m_nodeIdx);
		ServiceLocator< EventManager >.Instance.SendEvent( EventIDs.EVENT_ON_CLICK_PARENT_NODE, m_parentNode );
	}
}
