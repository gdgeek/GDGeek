// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Voxel/Glow" {
Properties {


	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_MKGlowPower ("Glow Power", Range(0.0,1.0)) = 1.0
	_MKGlowOffSet ("Glow Width ", Range(0.0,0.0755)) = 0.0
}
SubShader {

	//Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="MKGlow"}
	Tags {"RenderType"="MKGlow"}
	LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd


uniform half _MKGlowPower;

struct Input {
	float2 uv_MainTex;
	fixed3 color : COLOR;
	float4 vertex : POSITION; 

};

void surf (Input IN, inout SurfaceOutput o) {



	o.Albedo = IN.color ;// *  a;
	o.Emission = IN.color * IN.uv_MainTex.x * _MKGlowPower;
	o.Alpha = 1;

}
ENDCG
}

Fallback "Mobile/VertexLit"
}
