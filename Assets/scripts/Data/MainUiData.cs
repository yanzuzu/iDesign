using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainUiParentNode{
	public string name = string.Empty;
	public List<MainUiParentNode> NextNode = new List<MainUiParentNode>();
	public List<BuildData> Items;
	public MainUiParentNode(){}
}

