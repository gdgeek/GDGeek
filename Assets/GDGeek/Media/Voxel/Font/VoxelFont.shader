Shader "GDGeek/VoxelFont" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
        _FontUV ("Font Rect", Vector) = (0,0,0,0)
        _FontUV2 ("Font Rect2", Vector) = (0,0,0,0)
        _Size ("Size", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 customColor; 
			float opt; 
		};


		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float4 _FontUV;
		float4 _FontUV2;
		fixed4 _Size;
		
		
		void vert (inout appdata_full v, out Input o) {
        	UNITY_INITIALIZE_OUTPUT(Input,o);
			o.opt = v.normal.z;
        }
      
      
		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			
			
			fixed4 size = _Size;
			float2 uv0 = IN.uv_MainTex;
			float4 uv1 = _FontUV;
			float4 uv2 = _FontUV2;
			
			int w = _Size.x - _Size.y;
			int h = _Size.z - _Size.w;
			
			float x1 = (uv0.x-1) * (8.0/w) + 1;
			float y1 = uv0.y * (8.0/h);
			
			float x = 1-(floor(w*x1) +0.5)/w;
			float y = (floor(h*y1)+ 0.5)/h;
			
			float2 top = float2( uv1.x * (1 - x) + uv1.z* x, uv1.y * (1 - x) + uv1.w* x);
			float2 bottom = float2( uv2.x * (1 - x) + uv2.z* x, uv2.y * (1 - x) + uv2.w* x);
			
			float2 uv = top * (1-y) + bottom *y; 
			
			fixed4 c = tex2D (_MainTex, uv) * _Color;
			
			fixed4 color = fixed4(1-c.a, 1-c.a, 1-c.a, 1);
			
			o.Albedo = _Color.rgb;
			
			if(IN.opt < 0){
				o.Albedo = color.rgb;
			}
			
			o.Metallic = _Metallic;
			
			o.Smoothness = _Glossiness;
			
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
