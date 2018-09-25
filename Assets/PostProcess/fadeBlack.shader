Shader "Hidden/Shader/fadeBlack"
{
	HLSLINCLUDE
		#include "PostProcessing/Shaders/StdLib.hlsl"
		
		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float _Blend;
		
		float4 Frag(VaryingsDefault i) : SV_Target
		{
			float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
			float black = 0;
			color.rgb = lerp(color.rgb, black.xxx, _Blend.xxx);
			return color;
		}
		
	ENDHLSL
	
	SubShader
	{
		Cull Off Zwrite Off ZTest Always
		
		Pass
		{
			HLSLPROGRAM
			
				#pragma vertex VertDefault
				#pragma fragment Frag
			
			ENDHLSL
		}
	}
}