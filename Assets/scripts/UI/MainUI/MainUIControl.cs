using UnityEngine;
using System.Collections;
using ZuEngine.UI;
using ZuEngine;

public class MainUIControl : MonoBehaviour {
	[SerializeField]
	private TabUIControl m_tabUIControl;
	[SerializeField]
	private ItemListView m_listViewUI;

	public void Setup()
	{
		m_tabUIControl.SetTabActiveIdx (0);
		m_listViewUI.Setup (ServiceLocator<DataManager>.Instance.mainUIData);
	}
}
