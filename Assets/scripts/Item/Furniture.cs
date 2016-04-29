using UnityEngine;
using System.Collections;

public class Furniture : MonoBehaviour {
	public Rigidbody Rigid;
	public Transform CacheTrans;

	// Use this for initialization
	void Start () {
		Rigid = GetComponent<Rigidbody> ();
		CacheTrans = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
