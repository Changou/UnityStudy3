Shader "Custom/RimLight"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)

        //  테두리 밀도..
        _RimDensity("Rim Density", Range(1, 10)) = 3

        //  테두리 색..
        _RimColor("Rim Color", Color) = (1,1,1,1)

        _BumpMap("Normal Map", 2D) = "bump"{}

        _BumpRate("Normal Rate", Range(0.2, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf MyLight noambient

        sampler2D _MainTex;
        float4 _Color;

        half _RimDensity;
        float4 _RimColor;

        sampler2D _BumpMap;
        float _BumpRate;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;

            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            
            float3 nor = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

            nor = float3(nor.r * _BumpRate, nor.g * _BumpRate, nor.b);
            o.Normal = nor;

            float actRim;
            float rim = dot(o.Normal, IN.viewDir);            

            actRim = pow(1 - rim, _RimDensity);

            //  4)  색 적용..
            o.Emission = actRim * _RimColor.rgb;

            o.Alpha = c.a;
        }

        float4 LightingMyLight(SurfaceOutput s, float3 lightDir, float atten)
        {
            float4 ndotl = saturate(dot(s.Normal, lightDir));

            float4 final;

            final.rgb = ndotl * s.Albedo * _LightColor0.rgb * atten;

            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
