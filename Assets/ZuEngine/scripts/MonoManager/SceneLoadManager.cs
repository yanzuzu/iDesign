using UnityEngine;
using System.Collections;
using ZuEngine.Event;
using ZuEngine;

namespace ZuEngine.Manager
{
	public class SceneLoadManager : MonoBehaviour , IEventListener  {
		public string BeforeLoadLevelName = string.Empty;
		
		private string m_loadLevelName = string.Empty;
		private bool m_isLoadLevel = false;

		protected void Start()
		{
			ServiceLocator< EventManager >.Instance.RegisterListener (CommonEvents.EVENT_BEFORE_LEVEL_LOAD_OK, this);
		}
		
		public EventResult OnEvent(string eventName, object data)
		{
			switch( eventName )
			{
			case CommonEvents.EVENT_BEFORE_LEVEL_LOAD_OK:
				if( m_loadLevelName != string.Empty )
				{
					Application.LoadLevelAsync( m_loadLevelName );
					m_loadLevelName = string.Empty;
				}
				break;
			}
			return null;
		}
		
		public void LoadLevel( string levelName )
		{
			if( m_isLoadLevel )
			{
				ZuDebug.Log("load level ing...");
				return;
			}
			m_isLoadLevel = true;
			
			if( BeforeLoadLevelName != string.Empty )
			{
				m_loadLevelName = levelName;
				Application.LoadLevelAsync( BeforeLoadLevelName );
				return;
			}
			Application.LoadLevelAsync (levelName);
		}
		
		void OnLevelWasLoaded( int levelIdx )
		{
			m_isLoadLevel = false;
			ServiceLocator< EventManager >.Instance.SendEvent (CommonEvents.EVENT_LOAD_LEVEL_OK, levelIdx);
		}
		
	}
}
