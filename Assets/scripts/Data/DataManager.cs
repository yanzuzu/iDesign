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
		parentNode.Items.Add(new UiItemData(1,""));
		parentNode.Items.Add(new UiItemData(2,""));
		parentNode.NextNode = null;

		MainUiParentNode parentNode2 = new MainUiParentNode ();
		parentNode2.name = "Rooms";
		parentNode2.Items = null;

		MainUiParentNode parentNode3 = new MainUiParentNode ();
		parentNode3.name = "lights";
		parentNode2.NextNode = null;
		parentNode.Items = new List<UiItemData> ();
		parentNode.Items.Add(new UiItemData(3,""));
		parentNode.Items.Add(new UiItemData(4,""));

		return data;
	}
}
