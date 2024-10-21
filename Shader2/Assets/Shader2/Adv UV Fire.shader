Shader "Custom/Adv UV Fire"
{
    Properties
    {

        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo (RGB)", 2D) = "black" {}
    }
    SubShader
    {
         Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

       sampler2D _MainTex;
       sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 d = tex2D(
                _MainTex2,
                float2(
                    IN.uv_MainTex2.x,
                    IN.uv_MainTex2.y - _Time.y )
                );

            fixed4 c = tex2D(
                _MainTex,
                float2( 
                    IN.uv_MainTex + d.r )
                );

            //  문제 발생..
            // 
            //  1)  불이 구겨지면서 흐르는 바람에
            //      위에서 아래로 다시
            //      이미지가 오버플로우 됨..
            // 
            //  2)  미묘하게 왼쪽 아래로
            //      치우침..

            o.Emission = c.rgb;     //  자체 발광..
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
