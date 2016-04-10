using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine.Manager;
using ZuEngine;

public class FurnitureSelectUI : MonoBehaviour {
	public UIGrid unitGrid;
	public UIScrollView scrollView;

	private Dictionary<string,ItemUnitData> ItemUnitDatas = new Dictionary<string, ItemUnitData>();

	protected void Start()
	{
		ItemUnitDatas = ServiceLocator<DataManager>.Instance.ItemUnitDatas;
		foreach( KeyValuePair<string,ItemUnitData> unitData in ItemUnitDatas )
		{
			GameObject unitObj = ServiceLocator<ResourceManager>.Instance.LoadRes ("Prefab/ItemUnit");
			unitObj.transform.SetParent(unitGrid.gameObject.transform );
			unitObj.transform.localScale = Vector3.one;
			UIDragScrollView dragScrollView = unitObj.AddComponent<UIDragScrollView>();
			dragScrollView.scrollView = scrollView;

			ItemUnit itemUnit = unitObj.GetComponent<ItemUnit>();
			itemUnit.Setup(unitData.Value);
		}
		unitGrid.Reposition ();

	}
}
