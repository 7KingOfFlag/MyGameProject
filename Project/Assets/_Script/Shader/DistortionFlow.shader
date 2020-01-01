Shader "Custom/DistortionFlow"
{
	Properties
	{
		_Color("颜色", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		[NoScaleOffset] _FlowMap("噪声流贴图", 2D) = "black"{}
		[NoScaleOffset] _DerivHeightMap("高度导数图", 2D) = "black" {}
		_UJump("U jump per phase", Range(-0.25, 0.25)) = 0.25
		_VJump("V jump per phase", Range(-0.25, 0.25)) = 0.25
		_Tiling("平铺",Float) = 1
		_Speed("速度", Float) = 1
		_FlowStrength("强度", Float) = 1
		_HeightScale("恒定高度",Float) = 0.25
		_HeightScaleModulated("高度阈值", Float) = 0.75
		_FlowOffSet("流开始位置", Float) = 0
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0
			#include"Flow.cginc"

			sampler2D _MainTex, _FlowMap,_DerivHeightMap;
			float _UJump,_VJump,_Tiling,_Speed,_FlowStrength,_FlowOffSet;
			float _HeightScale,_HeightScaleModulated;

			struct Input
			{
				float2 uv_MainTex;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			float3 UnpackDerivativeHeight(float4 textureData) {
				float3 dh = textureData.agb;
				dh.xy = dh.xy * 2 - 1;
				return dh;
			}

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
				float3 flow = tex2D(_FlowMap,IN.uv_MainTex).rgb;
				flow.xy = flow * 2 - 1;
				flow *= _FlowStrength;
				float noise = tex2D(_FlowMap,IN.uv_MainTex).a;
				float time = _Time.y * _Speed + noise;
				float jump = float2(_UJump,_VJump);

				float3 uvwA = FlowUVW(IN.uv_MainTex, flow.xy,
							jump,_FlowOffSet,_Tiling, time, false);
				float3 uvwB = FlowUVW(IN.uv_MainTex, flow.xy,
							jump,_FlowOffSet,_Tiling, time, true);

				float finalHeightScale =
					length(flow.z) * _HeightScaleModulated + _HeightScale;

				float3 dhA = UnpackDerivativeHeight(tex2D(_DerivHeightMap,uvwA.xy)) *
					(uvwA.z * finalHeightScale);
				float3 dhB = UnpackDerivativeHeight(tex2D(_DerivHeightMap,uvwB.xy)) *
					(uvwB.z * finalHeightScale);
				o.Normal = normalize(float3(-(dhA.xy + dhB.xy),1));

				float4 texA = tex2D(_MainTex,uvwA.xy) * uvwA.z;
				float4 texB = tex2D(_MainTex,uvwB.xy) * uvwB.z;

				fixed4 c = (texA + texB) * _Color;
				o.Albedo = c.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}

			ENDCG
		}
			FallBack "Diffuse"
}