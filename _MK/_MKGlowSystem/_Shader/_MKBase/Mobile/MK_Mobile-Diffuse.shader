﻿Shader "MK/MKGlow/Mobile/Diffuse" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
		_MKGlowColor ("Glow Color", Color) = (1,1,1,1)
		_MKGlowPower ("Glow Power", Range(0.0,5.0)) = 2.5
		_MKGlowTex ("Glow Texture", 2D) = "black" {}
		_MKGlowTexColor ("Glow Texture Color", Color) = (1,1,1,1)
		_MKGlowTexStrength ("Glow Texture Strength ", Range(0.0,1.0)) = 1.0
		_MKGlowOffSet ("Glow Width ", Range(0.0,0.15)) = 0.0
	}
	SubShader 
	{
		Tags { "RenderType"="MKGlow" }
		LOD 150

		CGPROGRAM
		#pragma surface surf Lambert noforwardadd

		sampler2D _MainTex;
		
		sampler2D _MKGlowTex;
		half _MKGlowTexStrength;
		fixed4 _MKGlowTexColor;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_MKGlowTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D(_MKGlowTex, IN.uv_MKGlowTex) * _MKGlowTexColor;
			c += (d * _MKGlowTexStrength);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Mobile/VertexLit"
}
