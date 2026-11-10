#ifndef GDGEEK_VOXEL_SURFACE_INCLUDED
#define GDGEEK_VOXEL_SURFACE_INCLUDED

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ParallaxMapping.hlsl"

float4 GDGeekVoxelSample(UnityTexture2D map, float2 uv)
{
    float4 value = SAMPLE_TEXTURE2D(map.tex, map.samplerstate, map.GetTransformedUV(uv));
    if (map.hdrDecode.x > 0)
        value = DecodeHDRSample(value, map.hdrDecode);
    return value;
}

void VoxelParallax_float(float height, UnityTexture2D map, float2 meshUV, float3 viewDirectionTS, out float2 uv)
{
    // Preserve the original subgraph's half-precision mesh UV and centimetre conversion.
    float2 baseUV = (half2)meshUV;
    float heightCentimetres = (GDGeekVoxelSample(map, baseUV).r * height) * 100.0;
    float2 mapUV = map.GetTransformedUV(baseUV);
    uv = mapUV + ParallaxMappingChannel(TEXTURE2D_ARGS(map.tex, map.samplerstate),
        viewDirectionTS, heightCentimetres * 0.01, mapUV, 1);
}

void VoxelBaseTexture_float(float4 tint, UnityTexture2D map, float2 uv, out float3 color, out float alpha)
{
    float4 value = tint * GDGeekVoxelSample(map, uv);
    color = (half3)(value.rgb * value.a);
    alpha = (half)value.a;
}

void VoxelNormalMap_float(float scale, UnityTexture2D map, float2 uv, out float3 normalTS)
{
    float3 normal = UnpackNormal(GDGeekVoxelSample(map, uv));
    float strength = abs(scale);
    normalTS = float3(normal.xy * strength, lerp(1.0, normal.z, saturate(strength)));
}

void VoxelEmission_float(float4 tint, UnityTexture2D map, float2 uv, out float3 emission)
{
    half3 tintRGB = (half3)(tint.rgb * tint.a);
    emission = tintRGB * GDGeekVoxelSample(map, uv).rgb;
}

void VoxelOcclusion_float(float strength, UnityTexture2D map, float2 uv, out float occlusion)
{
    occlusion = (1.0 - strength) + (strength * GDGeekVoxelSample(map, uv).g);
}

void VoxelMetallicMap_float(float strength, UnityTexture2D map, float2 uv, out float metallic, out float alpha)
{
    float4 value = GDGeekVoxelSample(map, uv);
    metallic = strength * value.r;
    alpha = value.a;
}

void VoxelSpecularMap_float(float4 tint, UnityTexture2D map, float2 uv,
    out float3 specular, out float alpha, out float smoothness)
{
    float4 value = tint * GDGeekVoxelSample(map, uv);
    specular = value.rgb;
    alpha = value.a;
    smoothness = value.r;
}

void VoxelWorkflow_float(float metallic, float metallicAlpha, float3 specular, float specularAlpha, float specularRed,
    out float alpha, out float workflow, out float3 specularOutput)
{
#if defined(_SPECULAR_SETUP)
    alpha = specularAlpha;
    workflow = specularRed;
#else
    alpha = metallicAlpha;
    workflow = metallic;
#endif
    // The Voxel graph's specular output is constant in both workflows.
    specularOutput = float3(0.0, 0.0, 0.0);
}

#endif
