// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// ======================================================================================
// File         : SpriteBlendClipping.shader
// Author       : Wu Jie 
// Last Change  : 03/05/2012 | 15:19:25 PM | Monday,March
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
//
///////////////////////////////////////////////////////////////////////////////



Shader "GDGeek/Voxel" {
    Properties {
   		 _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Atlas Texture", 2D) = "white" {}
      //  _Alpha ("Alpha", float) = 0
       
    }


    SubShader {
        Tags { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
        }
        
        Blend SrcAlpha OneMinusSrcAlpha 
       
        Lighting Off 
        Fog { Mode Off }

        Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			struct vertexInput {
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal :NORMAL;
			};

			struct vertexOutput {
				float4 vertex        : POSITION;
				float2 texcoord      : TEXCOORD0;
				fixed3 color : COLOR;
			};

			vertexOutput vert ( vertexInput input ) {
				vertexOutput output;
				output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texcoord = TRANSFORM_TEX(input.texcoord, _MainTex);
				
				float3 viewDir = normalize(WorldSpaceViewDir(input.vertex));
				float3 normalDir =  normalize(mul(float4(input.normal, 0.0), unity_WorldToObject).xyz);
				output.color = float3(1,1,1) * min(1.0, max(0.3, dot(normalDir, viewDir)) *1.5);
				return output;
			}

			fixed4 frag ( vertexOutput _in ) : COLOR {
				fixed4 outColor =  tex2D ( _MainTex, _in.texcoord );
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
