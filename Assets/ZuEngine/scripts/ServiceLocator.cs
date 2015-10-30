using UnityEngine;
using System.Collections;

//---------------------------------------
// usage 
//---------------------------------------
// class Hello : MonoBehaviour {
//   public void hello() { print("Hello");} 
// }
// 
// call hello method
// ServiceLocator< Hello >.Instance.hello();
namespace ZuEngine
{
	public static class ServiceLocator< T >
		where T : MonoBehaviour
		{
		private static T implementation_ = null;
		private static System.WeakReference refTarget_ = null;
		private static bool isInitialized_g = false;

		public static void set( T instance ) {
			if ( implementation_ != null ) {
				Debug.LogWarning( "multi call ServiceLocator::set" );
			}
			if ( instance != null) {
				implementation_ = instance;
			} else { 
				implementation_ = new GameObject( typeof( T ).Name ).AddComponent< T >();
			}
			refTarget_ = new System.WeakReference( implementation_ );
			Object.DontDestroyOnLoad( implementation_ );
			/*if ( typeof( T ) != typeof( ServiceLocatorRoot ) ) {
				implementation_.transform.parent = ServiceLocator< ServiceLocatorRoot >.Instance.transform;
			}*/
			isInitialized_g = true;
		}

		public static void clear(){
			if ( null != implementation_ ) {
				Object.Destroy( implementation_.gameObject );
			}
			isInitialized_g = false;
			implementation_ = null;
			refTarget_ = null;
		}

		public static bool Valid {
			get {
				return isInitialized_g;
			}
		}

		public static T Instance {
			get {
				if ( !isInitialized_g ) {
					set( null );
				}
				return ( refTarget_ != null ) ? ( T )refTarget_.Target : null;
			}
		}
	}
}
