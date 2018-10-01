Shader "Custom/FresnelShaderEdit" {
	Properties {
	 	_Shininess ("Shininess", Range (0.01, 3)) = 1

	 	//_MyColor ("Shine Color", Color) = (1,1,1,1)

		_MainTex ("Base Color", Color) = (1,1,1,1)

		_Bump ("Bump", 2D) = "bump" {}

		_Glow ("Glow", Range(0, 10)) = 1

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		fixed4 _MainTex;
		sampler2D _Bump;
		float _Shininess;
		fixed4 _MyColor;

		float _Glow;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bump;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			//half4 c = tex2D (_MainTex, IN.uv_MainTex);

			half4 c = _MainTex;

			_MyColor.rgb = c.rgb * (_Glow +1);

			o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
			half factor = dot(normalize(IN.viewDir),o.Normal);
			o.Albedo = (c.rgb+_MyColor*(_Shininess-factor*_Shininess));
			o.Emission.rgb = _MyColor*(_Shininess-factor*_Shininess);
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}