using UnityEngine;
using System.Collections;

public class ListViewChildNode : MonoBehaviour {
	[SerializeField]
	private UISprite m_itemImg;
	[SerializeField]
	private MyUIButton m_uiBtn;

	private UiItemData itemData;

	public void Setup(UiItemData pData)
	{
		itemData = pData;
		//m_itemImg.spriteName = itemData.ImageName;
		m_uiBtn.normalSprite = itemData.ImageName;
	}
}
