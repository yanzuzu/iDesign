Shader "Custom/outline" {
	Properties
	{
		_MainColor("MainColor", Color ) = (1,1,1,1)
		_MainTex("MainTex", 2D ) = "white"{}
		_OutlineColor("OutlineColor", Color ) = (1,1,1,1)
		_OutlineWidth("OutlineWidth", Range(0,0.1) ) = 0.05
	}
	
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Pass
		{
			Cull Front
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag
			
			struct v2f
			{
				float4 pos: POSITION;
				float4 color: COLOR;
			};
			
			float _OutlineWidth;
			float4 _OutlineColor;
			
			v2f vert( appdata_base v )
			{
				v2f o;
				v.vertex.xyz += v.normal * _OutlineWidth;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
//				float3 norm =  mul((float3x3)UNITY_MATRIX_IT_MV, v.normal );
//				float2 offset = TransformViewToProjection(norm.xy);
//				o.pos.xy += offset * o.pos.z * _OutlineWidth;
			
				o.color = _OutlineColor;
				return o;
			}
			
			half4 frag( v2f i ):COLOR
			{
				return i.color;
			}
			
			ENDCG
		}
		
//		Pass
//		{
//			CGPROGRAM
//			#include "UnityCG.cginc"
//			#pragma vertex vert
//			#pragma fragment frag
//			
//			struct v2f
//			{
//				float4 pos:POSITION;
//				float2 uv:TEXCOORD0;
//			};
//			
//			sampler2D _MainTex;
//			
//			v2f vert( appdata_base v )
//			{
//				v2f o;
//				o.pos = mul( UNITY_MATRIX_MVP , v.vertex );
//				o.uv = v.texcoord;
//				return o;
//			}
//			
//			half4 frag( v2f i ):COLOR
//			{
//				return tex2D(_MainTex, i.uv );
//			}
//			
//			ENDCG
//		}
	
	}
	
	FallBack "Diffuse"
}
