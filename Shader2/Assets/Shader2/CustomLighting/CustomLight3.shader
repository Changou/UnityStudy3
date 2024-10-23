Shader "Custom/CustomLight3"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf MyLight noambient


        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        float4 LightingMyLight(SurfaceOutput s, float3 lightDir, float atten)
        {
            float4 ndotl = saturate( dot( s.Normal, lightDir ) );
            //float4 ndotl = max( 0, dot( s.Normal, lightDir ) );
            ndotl.rgb *= s.Albedo.rgb;

            return ndotl;
            //return ndotl + 0.5;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
