Shader "Custom/GhostShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _RimPow("RimLight Power", Range(1,10)) = 1
        _RimColor("RimColor", Color) = (1,1,1,1)
        _RimOn("IsRimOn", Range(0,1)) = 0
        
        _BlinkOn("IsBlinkOn", Range(0,1)) = 0
        _BlinkSpeed("BlinkSpeed", Range(0,7)) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf MyLight 

        sampler2D _MainTex;

        float _RimPow;
        float4 _RimColor;
        half _RimOn;
        half _BlinkOn;
        float _BlinkSpeed;

        struct Input
        {
            float2 uv_MainTex;

            float3 viewDir;
        };

        UNITY_INSTANCING_BUFFER_START(Props)

            UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
        
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            o.Albedo = c.rgb;

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1-rim, _RimPow);
            o.Emission = (rim * _RimOn) * saturate(_RimColor.rgb + sin(_Time.y * _BlinkSpeed * _BlinkOn));

            o.Alpha = c.a;
        }

        float4 LightingMyLight(SurfaceOutput s, float3 lightDir, float atten)
        {
            float ndot = saturate(dot(s.Normal, lightDir));
            float4 final;
            final.rgb = ndot * s.Albedo * _LightColor0.rgb * atten;
            final.a = s.Alpha;

            return final;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
