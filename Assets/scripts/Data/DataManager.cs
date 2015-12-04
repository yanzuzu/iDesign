using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

	public MainUiParentNode mainUIData;

	public Dictionary<int, BuildData > BuildDataMap = new Dictionary<int, BuildData>();


	public void Setup()
	{
		CreateBuildMapData ();
		mainUIData = CreateMainUIData ();
	}

	private MainUiParentNode CreateMainUIData()
	{
		MainUiParentNode data = new MainUiParentNode ();

		data.NextNode = new List<MainUiParentNode> ();
		MainUiParentNode parentNode = new MainUiParentNode ();
		parentNode.name = "Walls";
		parentNode.Items = new List<BuildData> ();
		parentNode.Items.Add(BuildDataMap[1]);
		parentNode.Items.Add(BuildDataMap[2]);
		parentNode.NextNode = null;
		data.NextNode.Add (parentNode);

		MainUiParentNode parentNode2 = new MainUiParentNode ();
		parentNode2.name = "Rooms";
		parentNode2.Items = null;

		MainUiParentNode parentNode3 = new MainUiParentNode ();
		parentNode3.name = "lights";
		parentNode3.NextNode = null;
		parentNode3.Items = new List<BuildData> ();
		parentNode3.Items.Add(BuildDataMap[3]);
		parentNode3.Items.Add(BuildDataMap[4]);

		parentNode2.NextNode.Add(parentNode3);

		data.NextNode.Add (parentNode2);

		return data;
	}


	private void CreateBuildMapData()
	{
		AddBuildData (1, UnitType.WALL, "item_wall_straight", "wallUnit");
		AddBuildData (2, UnitType.WALL_MULTI, "item_wall_multiStraight", "wallUnit");
		AddBuildData (3, UnitType.LIGHT_1, "item_light_1", "");
		AddBuildData (4, UnitType.LIGHT_2, "item_light_2", "");
	}

	private void AddBuildData(int pId , UnitType pType, string pImageName, string pResPath )
	{
		BuildData data = new BuildData ();
		data.Id = pId;
		data.type = pType;
		data.ImageName = pImageName;
		data.ResourcePath = pResPath;
		BuildDataMap [data.Id] = data;
	}
}
