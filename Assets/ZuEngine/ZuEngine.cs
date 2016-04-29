using UnityEngine;
using System.Collections;
using ZuEngine.Input;
using ZuEngine.StateManagement;
namespace ZuEngine
{
	public class ZuEngine : MonoBehaviour {
		private MouseInput mouseInput = new MouseInput();
		private TouchInput touchInput= new TouchInput();

		private float m_deltaTime = 0;
		private StateManager m_stateMgr;
		private InputManager m_inputMgr;

		void Awake()
		{
			ServiceLocator<DataManager>.Instance.GetData ();
		}

		// Use this for initialization
		void Start () {
			DontDestroyOnLoad (this);

			m_stateMgr = new StateManager ();
			m_inputMgr = new InputManager ();

			m_stateMgr.ChangeState (new MainGameState (m_stateMgr));
		}
		
		// Update is called once per frame
		void Update () {
			m_deltaTime =  Time.deltaTime;
			m_stateMgr.Update (m_deltaTime);
			m_inputMgr.Update (m_deltaTime);
		}
	}
}
