using UnityEngine;
using System.Collections;
using ZuEngine;
using ZuEngine.StateManagement;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using ZuEngine.Event;
using ZuEngine;

public class FurnitureSaveData
{
	public string id = string.Empty;
	public float posX;
	public float posY;
	public float posZ;
	public float rotateX;
	public float rotateY;
	public float rotateZ;
	public FurnitureSaveData(){
	}
}

public class FurnitureSave
{
	public List<FurnitureSaveData> SaveDatas = new List<FurnitureSaveData>();
}

public class FurnitureSaveTask : Task {

	private string SAVE_KEY = "FURNITURE_SAVE";

	public FurnitureSaveTask()
	{
//		PlayerPrefs.DeleteAll ();
//		PlayerPrefs.Save ();
//
		UnpackData ();

		ListenForEvent (EventIDs.EVENT_SAVE_FURNITURE, OnSaveFurniture);
	}

	private EventResult OnSaveFurniture(string name, object args)
	{
		PackData ();
		return null;
	}

#region implement Task
	public override void Pause()
	{
		

	}

	public override void Resume()
	{

	}

	public override void Show(bool show)
	{

	}

	public override void Update(float deltaTime)
	{
	}
#endregion

	private void PackData()
	{
		List<FurnitureSaveData> saveDatas = new List<FurnitureSaveData> ();
		foreach (KeyValuePair<int,Furniture> dataMap in ServiceLocator< FurnitureManager >.Instance.FurnitureMaps) {
			FurnitureSaveData data = new FurnitureSaveData ();
			data.id = dataMap.Value.UnitData.ItemId;
			data.posX = dataMap.Value.gameObject.transform.position.x;
			data.posY = dataMap.Value.gameObject.transform.position.y;
			data.posZ = dataMap.Value.gameObject.transform.position.z;
			data.rotateX = dataMap.Value.gameObject.transform.localEulerAngles.x;
			data.rotateY = dataMap.Value.gameObject.transform.localEulerAngles.y;
			data.rotateZ = dataMap.Value.gameObject.transform.localEulerAngles.z;
			saveDatas.Add (data);
		}
		FurnitureSave result = new FurnitureSave();
		result.SaveDatas = saveDatas;
		string jsonStr =  JsonWriter.Serialize (result);
		PlayerPrefs.SetString (SAVE_KEY, jsonStr);
		PlayerPrefs.Save ();
	}

	private void UnpackData()
	{
		List<FurnitureSaveData> saveDatas = new List<FurnitureSaveData> ();
		string saveData = PlayerPrefs.GetString (SAVE_KEY, string.Empty);
		if (string.Empty != saveData) {
			FurnitureSave save = JsonReader.Deserialize<FurnitureSave>(saveData);
			saveDatas = save.SaveDatas;	 
		}
		for (int i = 0; i < saveDatas.Count; i++) {
			Furniture obj = ServiceLocator< FurnitureManager >.Instance.CreateFurniture (saveDatas [i].id);
			obj.gameObject.transform.position = new Vector3( saveDatas [i].posX, saveDatas [i].posY, saveDatas [i].posZ);
			obj.gameObject.transform.localEulerAngles = new Vector3( saveDatas [i].rotateX, saveDatas [i].rotateY, saveDatas [i].rotateZ);
		}
	}
}
