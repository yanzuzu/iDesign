using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.Manager;

public class DataManager : MonoBehaviour {
	public Dictionary<string,ItemUnitData> ItemUnitDatas = new Dictionary<string, ItemUnitData>();

	public DataManager()
	{
	}

	public void GetData()
	{
		ItemUnitDatas = ServiceLocator<FakeData>.Instance.GetItemUnits ();
	}
}
