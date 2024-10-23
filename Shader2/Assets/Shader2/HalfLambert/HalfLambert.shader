Shader "Custom/HalfLambert"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalPam",2D) = "bump"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf MyLight

        sampler2D _MainTex;
        sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };
        float4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Alpha = c.a;
        }

        float4 LightingMyLight( SurfaceOutput s, float3 lightDir, float atten)
        {
            float4 ndotl = saturate(dot(s.Normal, lightDir));

            float4 final;

            //  램버트 * 이미지..
            //final.rgb = ndotl * s.Albedo;
            
            //  램버트 * 이미지 * 조명 색 & 강도..
            //final.rgb = ndotl * s.Albedo * _LightColor0.rgb;
            
            //  램버트 * 이미지 * 조명 색 & 강도 * 감쇠..
            //final.rgb = ndotl * s.Albedo * atten;
            final.rgb = ndotl * s.Albedo * _LightColor0.rgb * atten;
            
           
            final.a = s.Alpha;

            return final;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
