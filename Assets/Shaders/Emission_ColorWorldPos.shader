Shader "Custom/Emission_ColorWorldPos" {
	Properties {
		_AlbedoTex ("Albedo", 2D) = "white" {}
		_AlbedoColor ("Albedo Color", Color) = (1,1,1,1)
		_EmissionTex ("Emission", 2D) = "white" {}
		_Color1 ("Emission 1", Color) = (1,1,1,1)
		_Color2 ("Emission 2", Color) = (1,0,0,1)
		_Color3 ("Emission 3", Color) = (0,1,0,1)
		_Color4 ("Emission 4", Color) = (0,0,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float4 vertex : POSITION;
			float2 uv_AlbedoTex;
			float2 uv_EmissionTex;
			float3 worldPos;
		};

		sampler2D _AlbedoTex;
		fixed3 _AlbedoColor;
		sampler2D _EmissionTex;
		fixed4 _Color1;
		fixed4 _Color2;
		fixed4 _Color3;
		fixed4 _Color4;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//o.Emission = float4(0,0,0,1);
			if(IN.worldPos.y != 0 && IN.worldPos.z != 0){
				float angle  = atan2(IN.worldPos.y, IN.worldPos.z);
				if(angle < 0){
					o.Emission = lerp(_Color1, _Color2, saturate(angle * -1));
				}
				else{
					o.Emission = lerp(_Color3, _Color4, saturate(angle));
				}
			}
			o.Albedo =  tex2D (_AlbedoTex, IN.uv_AlbedoTex).rgb * _AlbedoColor;
			o.Emission *= tex2D (_EmissionTex, IN.uv_EmissionTex).rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
