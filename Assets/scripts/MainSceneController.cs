using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.Input;

public class MainSceneController : MonoBehaviour {
	[SerializeField]
	private MainUIControl m_mainUIController;

	void Awake()
	{
		ServiceLocator<DataManager>.Instance.Setup ();
		m_mainUIController.Setup ();
	}
	
}
