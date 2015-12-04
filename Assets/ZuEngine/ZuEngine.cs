using UnityEngine;
using System.Collections;
using ZuEngine.Input;

namespace ZuEngine
{
	public class ZuEngine : MonoBehaviour {
		private MouseInput mouseInput = new MouseInput();
		private TouchInput touchInput= new TouchInput();

		// Use this for initialization
		void Start () {
			DontDestroyOnLoad (this);
		}
		
		// Update is called once per frame
		void Update () {
			mouseInput.Update (Time.deltaTime);
			touchInput.Update (Time.deltaTime);
		}
	}
}
