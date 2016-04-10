using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZuEngine.Manager
{
	public class ResourceManager : MonoBehaviour 
	{
		private Dictionary< string , Object > m_resMap = new Dictionary<string, Object>();

		public Texture2D LoadImage(string path)
		{
			return Resources.Load( path ) as Texture2D;
		}

		public GameObject LoadRes( string path , bool forceLoad = true )
		{
			if( m_resMap.ContainsKey( path ) )
			{
				return GameObject.Instantiate( m_resMap[ path ] as GameObject );
			}

			if( forceLoad )
			{
				// TODO: should create a assetBundle res flow
				Object loadObj =  Resources.Load( path );
				if( null == loadObj )
				{
					ZuDebug.LogError(string.Format("this res = {0} can't find in res" , path ) );
					return null;
				}
				m_resMap[ path ] = loadObj;
				return GameObject.Instantiate( m_resMap[ path ] as GameObject );
			}else
			{
				ZuDebug.LogError(string.Format( "no this res  = {0} is loaded before" , path ) );
				return null;
			}		           
		}

		public void CheckOutRes( string path )
		{
			if( !m_resMap.ContainsKey( path ) )
			{
				ZuDebug.LogError(string.Format("this res = {0} isn't loaded before " , path ));
				return;
			}
			m_resMap.Remove (path);
		}
					
	}
}
