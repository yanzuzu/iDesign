using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

	public MainUIData mainUIData;

	public void Setup()
	{
		mainUIData = CreateMainUIData ();
	}

	private MainUIData CreateMainUIData()
	{
		MainUIData data = new MainUIData ();

		data.UiDatas = new List<MainUiParentNode> ();
		MainUiParentNode parentNode = new MainUiParentNode ();
		parentNode.name = "Walls";
		parentNode.Items = new List<UiItemData> ();
		parentNode.Items.Add(new UiItemData(1,"item_wall_straight"));
		parentNode.Items.Add(new UiItemData(2,"item_wall_multiStraight"));
		parentNode.NextNode = null;
		data.UiDatas.Add (parentNode);

		MainUiParentNode parentNode2 = new MainUiParentNode ();
		parentNode2.name = "Rooms";
		parentNode2.Items = null;

		MainUiParentNode parentNode3 = new MainUiParentNode ();
		parentNode3.name = "lights";
		parentNode3.NextNode = null;
		parentNode3.Items = new List<UiItemData> ();
		parentNode3.Items.Add(new UiItemData(3,"item_light_1"));
		parentNode3.Items.Add(new UiItemData(4,"item_light_2"));

		parentNode2.NextNode.Add(parentNode3);

		data.UiDatas.Add (parentNode2);

		return data;
	}
}
