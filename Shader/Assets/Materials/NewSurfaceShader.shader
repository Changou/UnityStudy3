Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _ColorR("Red",Range(0,1)) = 0
        _ColorG("Green",Range(0,1)) = 0
        _ColorB("Blue",Range(0,1)) = 0

        _Bright("Brightness",Range(-1,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _ColorR;
        half _ColorG;
        half _ColorB;
        float _Bright;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 color = float3(_ColorR, _ColorG, _ColorB);
            o.Albedo = color + _Bright;
            o.Alpha = 1;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
