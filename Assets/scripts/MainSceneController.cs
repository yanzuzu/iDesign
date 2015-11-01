using UnityEngine;
using System.Collections;
using ZuEngine;

public class MainSceneController : MonoBehaviour {
	[SerializeField]
	private MainUIControl m_mainUIController;

	void Awake()
	{
		ServiceLocator<DataManager>.Instance.Setup ();
		m_mainUIController.Setup ();
	}
}
