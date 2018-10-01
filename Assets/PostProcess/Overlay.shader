Shader "FX/Overlay"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_GlitchTex ("Glitch Texture", 2D) = "white" {}
		_Mix ("Mix", Range(0, 1)) = 1
		_Displace ("Displacement", Float) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _GlitchTex;
			float _Mix;
			float _Displace;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 glitchcol = tex2D(_GlitchTex, i.uv);

				//fixed2 offset = (0, (1 - (glitchcol.r * 2)) * _Displace);
				fixed2 offset = (1, 0);

				fixed4 col = tex2D(_MainTex, i.uv + fixed2((glitchcol.r + glitchcol.g + glitchcol.b) / 3 * _Displace, 0));
				// just invert the colors
				//col.rgb = 1 - col.rgb;
				fixed4 res = col + (glitchcol * _Mix);
				return res;
			}
			ENDCG
		}
	}
}
