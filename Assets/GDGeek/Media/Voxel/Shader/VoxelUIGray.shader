// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Voxel/UIGray" {
Properties {
	_Color ("Main Color", Color) = (.5,.5,.5,1)
}
SubShader {

	//Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="MKGlow"}
	Tags { "Queue"="Transparent" } 
	LOD 150

	Blend SrcAlpha OneMinusSrcAlpha  
CGPROGRAM
#pragma surface surf Lambert noambient

float4 _Color;
struct Input {
	fixed3 color : COLOR;
	float4 vertex : POSITION; 

};

void surf (Input IN, inout SurfaceOutput o) {


	fixed3 clr = IN.color *_Color;
	float f = (clr.r + clr.g+clr.b)/3;
	o.Albedo =  fixed3(f,f,f);
	o.Alpha = 0.1;

}
ENDCG
}

Fallback "Mobile/VertexLit"
}
