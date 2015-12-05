using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	private Camera m_uiCamera;
	private Camera m_3dCamera;

	void GetCamera()
	{
		if( m_uiCamera != null && m_3dCamera != null )
		{
			return;
		}
		m_uiCamera = GameObject.FindGameObjectWithTag ("UiCamera").GetComponent<Camera>();
		m_3dCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
	}

	public Camera GetUiCamera()
	{
		GetCamera ();
		return m_uiCamera;
	}

	public Camera Get3dCamera()
	{
		GetCamera ();
		return m_3dCamera;
	}

}
