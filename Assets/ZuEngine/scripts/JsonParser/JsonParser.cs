using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;

namespace ZuEngine.Data
{
	public class JsonParser {

		public T Parse<T>( string pJsonStr )
		{
			return JsonReader.Deserialize<T>(pJsonStr);
		}
	}
}
