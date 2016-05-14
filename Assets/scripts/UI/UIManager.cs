using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.Manager;

public enum UIType
{
	FURNITURE_OPERATOR,
}
public class UIManager : MonoBehaviour {
	private Dictionary<UIType,UIPanel> m_uiMaps = new Dictionary<UIType, UIPanel>();
	private UICamera m_uiCamera;

	public void Init(UICamera uiCamera)
	{
		m_uiCamera = uiCamera;
		// reload some ui
		CreateUI(UIType.FURNITURE_OPERATOR);
	}

	public UIPanel GetUI(UIType type)
	{
		CreateUI(type);
		return m_uiMaps [type];
	}
	private string GetUIPath(UIType type)
	{
		switch (type) {
		case UIType.FURNITURE_OPERATOR:
			return "UI/FurnitureOperationUI";
		default:
			return string.Empty;
		}
	}

	private UIPanel CreateUI(UIType type)
	{
		if (m_uiMaps.ContainsKey (type))
			return null;
		
		string uiPath = GetUIPath (type);
		if (uiPath == string.Empty) {
			ZuDebug.LogWarning (string.Format("this type = {0} doesn't have assets",type));
			return null;
		}
		GameObject obj = ServiceLocator< ResourceManager >.Instance.LoadRes (uiPath);
		UIPanel panel = obj.GetComponent<UIPanel> ();
		if( panel == null )
		{
			ZuDebug.LogWarning (string.Format ("this ui = {0} doesn't have UPanel obj", type));
			Destroy (obj);
			return null;
		}
		obj.transform.SetParent (m_uiCamera.transform);
		obj.transform.localScale = Vector3.one;
		obj.SetActive (false);
		m_uiMaps [type] = panel;
		return panel;
	}
}
