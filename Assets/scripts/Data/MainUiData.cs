using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainUIData
{
	public List<MainUiParentNode> UiDatas;
	public MainUIData(){}
}

public class MainUiParentNode{
	public string name = string.Empty;
	public List<MainUiParentNode> NextNode = new List<MainUiParentNode>();
	public List<UiItemData> Items;
	public MainUiParentNode(){}
}


public class UiItemData{
	public int Id = 0;
	public string ImageName = string.Empty;
	public UiItemData(){}

	public UiItemData( int pId, string pImageName )
	{
		Id = pId;
		ImageName = pImageName;
	}
}
