using UnityEngine;
using System.Collections;

public class MainUIControl : MonoBehaviour {
	[SerializeField]
	private TabUIControl m_tabUIControl;

	void Start()
	{
		m_tabUIControl.SetTabActiveIdx (0);
	}
}
