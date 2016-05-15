using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FakeData : MonoBehaviour {

	public Dictionary<string,ItemUnitData> GetItemUnits()
	{
		Dictionary<string,ItemUnitData> result = new Dictionary<string, ItemUnitData> ();
		result ["1"] = GetItemUnit ("1", "Chair 1", "chair_1", "Armchair");
		result ["2"] = GetItemUnit ("2", "Chair 2", "chair_2", "Chair_Tolix");
		result ["3"] = GetItemUnit ("3", "Cabinet 1", "Cabinet_1","Cupboard");
		result ["4"] = GetItemUnit ("4", "Cabinet 2", "Cabinet_2","Cupboard _Short");
		result ["5"] = GetItemUnit ("5", "Sofa 3", "sofa_1","Sofa_08");
		result ["6"] = GetItemUnit ("6", "table 1", "table_1","Table");
		result ["7"] = GetItemUnit ("7", "table 2", "table_2","Table_Square");
		result ["8"] = GetItemUnit ("8", "table 3", "table_3","Console");
		result ["9"] = GetItemUnit ("9", "table 4", "table_4","Console_Round");
		result ["10"] = GetItemUnit ("10", "table 5", "table_5","Fireplace");
		result ["11"] = GetItemUnit ("11", "light 1", "light_1","Lamp_Lexinton");

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
