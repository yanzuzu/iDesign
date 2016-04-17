using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FakeData : MonoBehaviour {

	public Dictionary<string,ItemUnitData> GetItemUnits()
	{
		Dictionary<string,ItemUnitData> result = new Dictionary<string, ItemUnitData> ();
		result ["1"] = GetItemUnit ("1", "Chair 1", "chair_1", "Armchair");
		result ["2"] = GetItemUnit ("2", "Chair 2", "chair_2", "Armchair");
		result ["3"] = GetItemUnit ("3", "Cabinet 1", "Cabinet_1","Sofa_08");
		result ["4"] = GetItemUnit ("4", "Cabinet 2", "Cabinet_2","Sofa_08");
		result ["5"] = GetItemUnit ("5", "Cabinet 3", "Cabinet_3","Sofa_08");
		result ["6"] = GetItemUnit ("6", "table 1", "table_1","Sofa_08");
		result ["7"] = GetItemUnit ("7", "table 2", "table_2","Sofa_08");

		return result;
	}

	private ItemUnitData GetItemUnit(string id, string name, string imgName, string res)
	{
		ItemUnitData result = new ItemUnitData ();
		result.ItemId = id;
		result.ItemName = name;
		result.ItemImage = imgName;
		result.ResourcePath = res;
		return result;
	}
}
