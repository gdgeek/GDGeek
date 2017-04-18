
Shader "GDGeek/VoxelMesh" {
    Properties {
   		 _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Atlas Texture", 2D) = "white" {}
        _ShadowTex ("Shadow Texture", 2D) = "white" {}
        _ShadowAlpha ("Shadow Alpha", float) = 0
        _UpDown ("Up & Down", Vector) = (0,0,0,0)
        _LeftRight ("Left & Right", Vector) = (0,0,0,0)
        _FrontBack ("Front & Back", Vector) = (0,0,0,0)
        _LightPower ("Light Power", Range(0.5, 2)) = 1.4
       
    }

    // ======================================================== 
    // cg 
    // ======================================================== 

    SubShader {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
        }
        Cull Off 
        Lighting Off 
        Fog { Mode Off }

        Pass {
			CGPROGRAM 
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members shadow)
			//#pragma exclude_renderers d3d11 xbox360
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _ShadowTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float _ShadowAlpha;
			float _LightPower;
			struct appdata_t {
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1     : TEXCOORD1;
				fixed3 color     : COLOR;
				float3 normal :NORMAL;
			};

			struct v2f {
				float4 vertex        : POSITION;
				float2 texcoord      : TEXCOORD0;
				float2 texcoord1     : TEXCOORD1;
				fixed3 color     : COLOR; 
			};

			v2f vert ( appdata_t _in ) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, _in.vertex);
				o.texcoord = TRANSFORM_TEX(_in.texcoord, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(_in.texcoord1, _MainTex);
				
				fixed3  front= (_in.normal.z == 1)? fixed3(0.4,0.4,0.4):fixed3(0,0,0);
				fixed3 back = (_in.normal.z == -1)? fixed3(1, 1, 1):fixed3(0,0,0);
				fixed3 down = (_in.normal.y == -1)? fixed3(0.8, 0.8, 0.8):fixed3(0,0,0);
				fixed3 up = (_in.normal.y == 1)? fixed3(0.6,0.6,0.6):fixed3(0,0,0);
				fixed3 right = (_in.normal.x == -1)? fixed3(0.6,0.6,0.6):fixed3(0,0,0);
				fixed3 left = (_in.normal.x == 1)? fixed3(0.8,0.8, 0.8):fixed3(0,0,0);
				
				o.color = back + front + down + up + left+ right;
				o.color *= _in.color;
				return o;
			}

			fixed4 frag ( v2f _in ) : COLOR {
				fixed4 outColor = tex2D ( _MainTex, _in.texcoord );
				fixed4 shadowColor = tex2D ( _ShadowTex, _in.texcoord1);
				outColor *= shadowColor *_ShadowAlpha + (1-_ShadowAlpha);
				outColor *= _Color;
				outColor.rgb *= _in.color;
                return outColor; 
			}
			ENDCG
        }
    }

    // ======================================================== 
    // fallback 
    // ======================================================== 

    SubShader {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
        }
        Cull Off 
        Lighting Off 
        ZWrite Off 
        Fog { Color (0,0,0,0) }
        Blend SrcAlpha OneMinusSrcAlpha

        BindChannels {
            Bind "Color", color
            Bind "Vertex", vertex
            Bind "TexCoord", texcoord
        }

        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
