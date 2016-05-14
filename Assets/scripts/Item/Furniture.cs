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

	}

	public void OnClickItem()
	{
		if (m_outlineShader == null)
		{
			m_outlineShader = Shader.Find ("Custom/outline");
		}
		m_outlineObj =  GameObject.Instantiate (this.gameObject);
		m_outlineObj.transform.SetParent (CacheTrans);
		m_outlineObj.transform.localScale = Vector3.one;
		BoxCollider boxCollid = m_outlineObj.GetComponent<BoxCollider> ();
		Destroy (boxCollid);
//		Rigidbody rigibody = m_outlineObj.GetComponent<Rigidbody> ();
//		Destroy (rigibody);
		MeshRenderer [] renders = m_outlineObj.GetComponentsInChildren<MeshRenderer> ();
		for (int i = 0; i < renders.Length; i++) {
			renders [i].material.shader = m_outlineShader;
			renders [i].material.SetColor ("_OutlineColor",Color.yellow);
			renders [i].material.SetFloat ("_OutlineWidth",0.02f);
		}

		UIPanel obj =  ServiceLocator<UIManager>.Instance.GetUI (UIType.FURNITURE_OPERATOR);
		obj.gameObject.SetActive (true);
	}

	public void OnReleaseItem()
	{
		if (m_outlineObj != null)
			Destroy (m_outlineObj.gameObject);

		UIPanel obj =  ServiceLocator<UIManager>.Instance.GetUI (UIType.FURNITURE_OPERATOR);
		obj.gameObject.SetActive (false);
	}

	public void Rotate(int turnValue)
	{
		transform.RotateAround (transform.position, Vector3.up, turnValue * Time.deltaTime*90f);
	}
}
