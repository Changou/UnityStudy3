Shader "Custom/Basic Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Bright ("Bright", Range(-1,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        //LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float4 color : COLOR;
        };

        fixed4 _Color;
        float _Bright;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color + _Bright;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
