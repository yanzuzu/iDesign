using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Event;

public class ListViewChildNode : MonoBehaviour {
	[SerializeField]
	private UISprite m_itemImg;
	[SerializeField]
	private MyUIButton m_uiBtn;

	private BuildData m_itemData;

	public void Setup(BuildData pData)
	{
		m_itemData = pData;
		m_uiBtn.normalSprite = m_itemData.ImageName;
	}

	public void OnClickChildNode()
	{
		ServiceLocator< EventManager >.Instance.SendEvent (EventIDs.EVENT_ON_CLICK_UNIT_TYPE, m_itemData);
	}
}
