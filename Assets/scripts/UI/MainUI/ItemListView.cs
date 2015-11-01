using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemListView : MonoBehaviour {
	[SerializeField]
	private ListViewParentNode m_parentNode;
	[SerializeField]
	private UIGrid m_grid;

	private MainUIData m_uiData;

	public void Setup(MainUIData uiData)
	{
		m_uiData = uiData;
		createParentNode ();
	}

	private void createParentNode()
	{
		for( int i = 0 ; i < m_uiData.UiDatas.Count ; i ++ )
		{
			GameObject unitObj = Instantiate(m_parentNode.gameObject) as GameObject;
			unitObj.transform.parent = m_grid.transform;
			unitObj.transform.localScale = Vector3.one;
			unitObj.SetActive(true);
			ListViewParentNode unitComponent = unitObj.GetComponent<ListViewParentNode>();
			unitComponent.Setup(m_uiData.UiDatas[i]);
		}
	}
}
