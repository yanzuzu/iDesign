using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Manager;
using ZuEngine.Event;

public class ItemUnit : MonoBehaviour {
	public UITexture ItemImg;
	public MyUILabel ItemName;

	private string IMAGE_PATH = "Item/{0}";
	private string RES_PATH = "Prefab/Item/{0}";
	private ItemUnitData m_unitData;

	public void Setup(ItemUnitData unitData)
	{
		m_unitData = unitData;

		ItemImg.mainTexture = ServiceLocator<ResourceManager>.Instance.LoadImage (string.Format(IMAGE_PATH,unitData.ItemImage));
		ItemName.text = unitData.ItemName;
	}

	public void OnClickItem()
	{
		ZuDebug.Log ("OnClickItem itemUnit = " + m_unitData.ItemName);
		ServiceLocator<FurnitureManager>.Instance.CreateFurniture (m_unitData.ItemId);
		ServiceLocator<EventManager>.Instance.SendEvent (EventIDs.EVENT_SAVE_FURNITURE);
	}
}
