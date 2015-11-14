using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemListView : MonoBehaviour {
	[SerializeField]
	private ListViewParentNode m_parentNode;
	[SerializeField]
	private UIGrid m_grid;
	[SerializeField]
	private ListViewChildNode m_childNode;
	[SerializeField]
	private UIGrid m_childGrid;

	private MainUiParentNode m_uiData;
	private List<ListViewParentNode> m_parentNodes = new List<ListViewParentNode> ();
	private List<ListViewChildNode> m_childNodes = new List<ListViewChildNode> ();

	public void Setup(MainUiParentNode uiData)
	{
		m_uiData = uiData;
		refreshParentNode (m_uiData, m_uiData.NextNode);
	}

	public void refreshChildNode(MainUiParentNode pParentNode)
	{
		ZuDebug.Log ("refreshChildNode");
		string nodeName = pParentNode.name;
		List<UiItemData> pItems = pParentNode.Items;
		m_grid.gameObject.SetActive (false);
		m_childGrid.gameObject.SetActive (true);
		for( int i = 0 ; i < m_childNodes.Count ; i ++ )
		{
			DestroyImmediate(m_childNodes[i].gameObject);
		}
		m_childNodes.Clear ();

		for( int i = 0 ; i < pItems.Count ; i ++ )
		{
			GameObject unitObj = Instantiate(m_childNode.gameObject) as GameObject;
			unitObj.transform.parent = m_childGrid.transform;
			unitObj.transform.localScale = Vector3.one;
			unitObj.SetActive(true);
			ListViewChildNode unitComponent = unitObj.GetComponent<ListViewChildNode>();
			unitComponent.Setup(pItems[i]);
			m_childNodes.Add(unitComponent);
		}
		m_childGrid.Reposition ();
	}

	public void refreshParentNode(MainUiParentNode parentNode,List<MainUiParentNode> pNextNodes)
	{
		ZuDebug.Log ("refreshParentNode");
		string nodeName = parentNode == null ? string.Empty : parentNode.name;
		m_childGrid.gameObject.SetActive (false);
		m_grid.gameObject.SetActive (true);
		for( int i = 0 ; i< m_parentNodes.Count ; i ++ )
		{
			DestroyImmediate(m_parentNodes[i].gameObject);
		}
		m_parentNodes.Clear ();

		for( int i = 0 ; i < pNextNodes.Count ; i ++ )
		{
			GameObject unitObj = Instantiate(m_parentNode.gameObject) as GameObject;
			unitObj.transform.parent = m_grid.transform;
			unitObj.transform.localScale = Vector3.one;
			unitObj.SetActive(true);
			ListViewParentNode unitComponent = unitObj.GetComponent<ListViewParentNode>();
			unitComponent.Setup(parentNode,pNextNodes[i]);
			m_parentNodes.Add(unitComponent);
		}
		m_grid.Reposition();
	}

}
