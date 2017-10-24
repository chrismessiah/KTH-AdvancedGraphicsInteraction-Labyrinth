// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Jockes/Blackhole"
{
	Properties
	{
		_Scale("Scale", Float) = 1.0
		_GravitationalLensing("Gravitational Lensing", Float) = 1.0
		_EventHorizon("Event Horizon", Float) = 1.0
	}
	SubShader
	{

		Tags { "Queue"="Transparent" }

		GrabPass {
			Name "BASE"
            Tags { "LightMode" = "Always" }
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				half2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float R : COLOR0;
				float2 uv : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
				float4 normal : TEXCOORD2;
				UNITY_FOG_COORDS(1)
			};

			uniform float _Scale;
			uniform float _GravitationalLensing;
			uniform float _EventHorizon;
			sampler2D _GrabTexture;

			v2f vert (vertexInput v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				// World space vertices and normals
				float3 posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 normWorld = normalize(mul( float4( v.normal, 0.0 ), unity_WorldToObject ).xyz);

				// R value is the fresnel coefficient
				float3 I = normalize(posWorld - _WorldSpaceCameraPos);
				o.R = pow(1 + dot(I, normWorld), _GravitationalLensing);

				float mask = 1 - o.R;

				o.normal = UnityObjectToClipPos(v.normal * o.R);

				// Compute where the background should be
				float4 real = ComputeGrabScreenPos(o.vertex);
				float4 distorted = ComputeGrabScreenPos(o.normal);

				o.grabPos.xy = lerp(real.xy, distorted.xy, mask);
				o.grabPos.zw = real.zw;

				o.R = clamp(_Scale * pow(1 + dot(I, normWorld), _EventHorizon), 0, 1);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//half4 bgcolor = i.R;
				half4 bgcolor = tex2Dproj(_GrabTexture, i.grabPos);

				bgcolor = lerp(half4(0,0,0,0), bgcolor, i.R);

				//bgcolor += noise(1);

				// Lerp the object color with the fresnel color
				//bgcolor = lerp(bgcolor, _Fresnel, i.R);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, bgcolor);
				return bgcolor;
			}
			ENDCG
		}
	}
}
