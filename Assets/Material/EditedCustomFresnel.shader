Shader "Custom/FresnelShaderEdited1"
{
	Properties
	{
		_obj_opacity ("_obj_opacity", Range(0, 1)) = 1

		_Color ("Base Color", Color) = (1,1,0,1)
		
		_Power ("Power" , Float) = 1
		
		_RimGlow ("Rim Glow" , Float) = 1
	}
    SubShader
    {

		Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			//#include "Assets/Shaders/EditedStdLib.hlsl"
			
			uniform float _Power;
			uniform float4 _Color;
			uniform float _RimGlow;

			float _obj_opacity;
			
			struct appdata
			 {
				 float4 vertex : POSITION;
				 fixed4 color : COLOR;
				 float3 normal : NORMAL;
			 };

			 struct v2f {
				 float4 pos : SV_POSITION;
				 float3 normal : NORMAL;
				 float4 posWorld : TEXCOORD0;
				 fixed4 color : COLOR;
			 };

			 v2f vert(appdata v)
			 {
				 v2f o;

				 o.pos = UnityObjectToClipPos(v.vertex);
			 
				 o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				 o.normal = normalize( mul ( float4(v.normal, 0.0), unity_WorldToObject).xyz);

				 o.color = v.color;
				 UNITY_TRANSFER_FOG(o, o.pos);

				 return o;
			 }

			 half4 frag(v2f i) : COLOR
			 {
				 float3 normalDir = i.normal;
				 float3 viewDir = normalize( _WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				 
				 float rim = 1 - saturate ( dot(viewDir, normalDir) );

				 float3 rimLight = (pow(rim, _Power) * _Color) * _RimGlow;

				 //return i.color;
					
				 return float4( _Color + rimLight, _obj_opacity);
			 }

			//FallBack "Diffuse"
			ENDCG
		}
	}
}
