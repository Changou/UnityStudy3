Shader "Custom/SimpleBlending"
{
    Properties
    {
        _MainTex("Albedo (RGB) 1", 2D) = "white" {}

        //  추가 이미지..
        _MainTex2("Albedo (RGB) 2", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

       
        sampler2D _MainTex;

        sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex;

            float2 uv_MainTex2;
        };

       

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 d = tex2D(_MainTex2, IN.uv_MainTex2);
            
            o.Albedo = lerp( c.rgb, d.rgb, c.a );
             
            //  반전..
            //  방법 - 1)
            //o.Albedo = lerp(d.rgb, c.rgb, c.a );
            //  방법 - 2)
            //o.Albedo = lerp(c.rgb, d.rgb, 1-c.a );
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
