using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine.Data;

public class ListViewHistoryBar : MonoBehaviour {
	[SerializeField]
	private HistoryNode m_nodeObj;
	[SerializeField]
	private UIGrid m_grid;

	private const int TOTAL_UI_WIDTH = 300;
	private List<HistoryNode> m_nodeObjs = new List<HistoryNode>();
	//private Dictionary<int , List<MainUiParentNode> > m_historyDatas = new Dictionary<int, List<MainUiParentNode>>();

	public void AddNode(MainUiParentNode pPatentNode, MainUiParentNode pCurrentNode)
	{
		CreateNode (pPatentNode, pCurrentNode);
		//m_historyDatas [m_nodeObjs.Count - 1] = parentNode;
		refreshSize ();
	}

	private void CreateNode(MainUiParentNode pPatentNode, MainUiParentNode pCurrentNode)
	{
		GameObject nodeObj = GameObject.Instantiate(m_nodeObj.gameObject) as GameObject;
		nodeObj.transform.SetParent (m_grid.transform);
		nodeObj.transform.localScale = Vector3.one;
		nodeObj.SetActive (true);
		HistoryNode historyNodeObj = nodeObj.GetComponent<HistoryNode> ();
		m_nodeObjs.Add(historyNodeObj);
		historyNodeObj.Setup (pCurrentNode.name + " >", pPatentNode, m_nodeObjs.Count - 1 );
	}

	public void subNodes(int pIdx)
	{
		if( pIdx >= m_nodeObjs.Count )
		{
			return;
		}
		for( int i = m_nodeObjs.Count - 1 ; i >= 0 ; i -- )
		{
			if( i >= pIdx )
			{
				DestroyImmediate( m_nodeObjs[i].gameObject );
				m_nodeObjs.RemoveAt(i);
			}else
			{
				break;
			}
		}
		refreshSize ();

	}

	private void refreshSize()
	{
		int nodeWidth = TOTAL_UI_WIDTH;
		if( m_nodeObjs.Count != 0 )
		{
			nodeWidth = TOTAL_UI_WIDTH/m_nodeObjs.Count;
		}
		for( int i = 0 ; i < m_nodeObjs.Count ; i ++ )
		{
			m_nodeObjs[i].Resize(nodeWidth);
		}
		m_grid.cellWidth = nodeWidth;
		m_grid.Reposition ();
	}
}

