Shader "Custom/UVTime"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Bright("Bright", Range(-1,1)) = 0.5
        _FlowSpeed("Flow Speed", Range(0, 5)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
                
        sampler2D _MainTex;
        float _FlowSpeed;
        float _Bright;
        int _IsBlinkOn;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D( _MainTex, IN.uv_MainTex);
            float4 time = _SinTime.w * _FlowSpeed;
            
            o.Emission = (c.rgb + _Bright) * time;
            o.Alpha = c.a ;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
