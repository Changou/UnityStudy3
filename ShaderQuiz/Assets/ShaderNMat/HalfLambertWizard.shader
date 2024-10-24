Shader "Wizard/HalfLambertWizard"
{
    Properties
    {
        _Color("Color", Color) = (0,0,0,0)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimDensity("Rim Density", Range(1,10)) = 3
        _IsRimOn("Rim On", Range(0,1)) = 0

        _Speed("Speed", Range(0,3)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+0"}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf MyLight

        sampler2D _MainTex;
        
        //블링크
        float _Speed;

        //림라이트
        half _RimDensity;
        float4 _RimColor;
        half _IsRimOn;

        struct Input
        {
            float2 uv_MainTex;

            float3 viewDir;
        };

        UNITY_INSTANCING_BUFFER_START(Props)

            UNITY_DEFINE_INSTANCED_PROP(float4, _Color)

        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            o.Albedo = c.rgb;

            float actRim;
            float rim = dot(o.Normal, IN.viewDir);

            actRim = pow(1- rim, _RimDensity);

            o.Emission = _IsRimOn * actRim * (_RimColor.rgb + sin(_Time.y * _Speed));

			o.Alpha = c.a;
        }

        float4 LightingMyLight( SurfaceOutput s, float3 lightDir, float atten)
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
