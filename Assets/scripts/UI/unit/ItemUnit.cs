using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.Manager;

public class ItemUnit : MonoBehaviour {
	public UITexture ItemImg;
	public MyUILabel ItemName;

	private string IMAGE_PATH = "Item/{0}";
	private ItemUnitData m_unitData;

	public void Setup(ItemUnitData unitData)
	{
		m_unitData = unitData;

		ItemImg.mainTexture = ServiceLocator<ResourceManager>.Instance.LoadImage (string.Format(IMAGE_PATH,unitData.ItemImage));
		ItemName.text = unitData.ItemName;
	}
}
