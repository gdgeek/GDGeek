// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Voxel/Cube" {
Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd



fixed4 _Color;
struct Input {
	float2 uv_MainTex;
	float4 vertex : POSITION; 
};

void surf (Input IN, inout SurfaceOutput o) {
	
	o.Alpha = _Color.a;
	o.Albedo = _Color.rgb;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
