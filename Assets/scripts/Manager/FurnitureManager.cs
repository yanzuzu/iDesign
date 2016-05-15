using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.Manager;

public class FurnitureManager : MonoBehaviour {
	private int currentGuid = 0;
	private string RES_PATH = "Prefab/Item/{0}";

	public Dictionary<int,Furniture> FurnitureMaps = new Dictionary<int, Furniture>();

	public Furniture CreateFurniture( string id )
	{
		if (!ServiceLocator<DataManager>.Instance.ItemUnitDatas.ContainsKey (id)) {
			ZuDebug.LogError (string.Format("this id = {0} doesn't exist in Furniture data",id));
			return null;
		}

		ItemUnitData unitData = ServiceLocator<DataManager>.Instance.ItemUnitDatas [id];
		GameObject obj = ServiceLocator< ResourceManager >.Instance.LoadRes (string.Format (RES_PATH, unitData.ResourcePath));
		obj.transform.position = Vector3.zero;
		Furniture FurnitureObj = obj.GetComponent<Furniture> ();
		if( FurnitureObj == null )
		{
			ZuDebug.LogError (string.Format("this id = {0} doesnt' have furniture component",id));
			return FurnitureObj;
		}
		FurnitureObj.Guid = currentGuid++;
		FurnitureObj.UnitData = unitData;

		FurnitureMaps [FurnitureObj.Guid] = FurnitureObj;
		return FurnitureObj;
	}
}
