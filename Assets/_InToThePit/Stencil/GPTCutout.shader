// Shader "Custom/StencilCutout"
// {
// Properties
//     {
//         _BaseColor("Base Color", Color) = (1,1,1,1)
//     }
//     SubShader
//     {
//         Tags { "Queue" = "AlphaTest" }
//         Stencil
//         {
//             Ref 1
//             Comp NotEqual // Render only where stencil is NOT set
//             Pass Keep
//         }
//         ZWrite On // Ensures proper depth sorting
//         AlphaTest Greater .5 // Forces clipping
//         Pass
//         {
//             HLSLPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

//             struct Attributes
//             {
//                 float4 positionOS : POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             struct Varyings
//             {
//                 float4 positionHCS : SV_POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             Varyings vert(Attributes IN)
//             {
//                 Varyings OUT;
//                 OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
//                 OUT.uv = IN.uv;
//                 return OUT;
//             }

//             float4 _BaseColor;
            
//             float4 frag(Varyings IN) : SV_Target
//             {
//                 if (_BaseColor.a < 0.5) discard; // Ensures proper cutout
//                 return _BaseColor;
//             }

//             ENDHLSL
//         }
//     }
// }

Shader "Custom/StencilCutoutLit"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {} // Optional texture support
    }
    SubShader
    {
        Tags { "Queue" = "AlphaTest" "RenderType" = "Opaque" }
        Stencil
        {
            Ref 1
            Comp NotEqual // Render only where stencil is NOT set
            Pass Keep
        }
        Pass
        {
            Tags { "LightMode" = "UniversalForward" } // URP lighting support
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normalWS : NORMAL;
                float3 worldPos : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _BaseColor;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                OUT.normalWS = TransformObjectToWorldNormal(IN.normal);
                OUT.worldPos = TransformObjectToWorld(IN.positionOS.xyz);
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                // Sample texture
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                float4 baseColor = _BaseColor * texColor;

                // Compute lighting
                Light mainLight = GetMainLight();
                float3 normal = normalize(IN.normalWS);
                float3 lightDir = normalize(mainLight.direction);
                float NdotL = max(dot(normal, lightDir), 0.0);

                float3 litColor = baseColor.rgb * (mainLight.color.rgb * NdotL);
                return float4(litColor, baseColor.a);
            }

            ENDHLSL
        }
    }
}
