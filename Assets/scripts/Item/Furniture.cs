using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZuEngine;
using ZuEngine.Manager;

public class Furniture : MonoBehaviour {
	public Rigidbody Rigid;
	public Transform CacheTrans;
	public BoxCollider BoxCollid;

	private GameObject m_outlineObj;
	private Shader m_outlineShader;
	// Use this for initialization
	void Start () {
		Rigid = GetComponent<Rigidbody> ();
		CacheTrans = this.gameObject.transform;
		BoxCollid = GetComponent<BoxCollider> ();
		m_outlineShader = Shader.Find("Custom/outline");
	}

	public void OnClickItem()
	{
		m_outlineObj =  GameObject.Instantiate (this.gameObject);
		m_outlineObj.transform.SetParent (CacheTrans);
		m_outlineObj.transform.localScale = Vector3.one;
		MeshRenderer [] renders = m_outlineObj.GetComponentsInChildren<MeshRenderer> ();
		for (int i = 0; i < renders.Length; i++) {
			renders [i].material.shader = m_outlineShader;
			renders [i].material.SetColor ("_OutlineColor",Color.yellow);
			renders [i].material.SetFloat ("_OutlineWidth",0.02f);
		}
	}

	public void OnReleaseItem()
	{
		if (m_outlineObj != null)
			Destroy (m_outlineObj.gameObject);
	}
}
