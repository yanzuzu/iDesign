using UnityEngine;
using System.Collections;
using System;

public class ZuDebug {

#if UNITY_EDITOR
	static public Action< object > Log = Debug.Log;
	static public Action< object > LogWarning = Debug.LogWarning;
	static public Action< object > LogError = Debug.LogError;
#else
	public static void Log(string p_log)
	{
		#if DEBUG
		Debug.Log (p_log);
		#endif
	}

	public static void LogWarning(string p_log)
	{
		#if DEBUG
		Debug.LogWarning (p_log);
		#endif
	}

	public static void LogError(string p_log)
	{
		#if DEBUG
		Debug.LogError (p_log);
		#endif
	}
	
#endif 

}
