using UnityEngine;
using System.Collections;

public class ListViewParentNode : MonoBehaviour {
	[SerializeField]
	private UILabel m_name;

	public void Setup( MainUiParentNode pNodeData )
	{
		m_name.text = pNodeData.name;
	}
}
