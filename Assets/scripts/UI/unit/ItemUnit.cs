using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Manager;

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
		GameObject itemObj = ServiceLocator< ResourceManager >.Instance.LoadRes (string.Format (RES_PATH, m_unitData.ResourcePath));
		itemObj.transform.position = Vector3.zero;
	}
}
