using UnityEngine;
using System.Collections;
using ZuEngine;

public class MainSceneController : MonoBehaviour {

	void Awake()
	{
		ServiceLocator<DataManager>.Instance.Setup ();
	}
}
