//==========================================================
/*    
    -   프레넬 공식적용..
        
        ----------
        -   개요..
        ----------
        
            ---------------
            -   기본 설정..
            ---------------

                -   [ Albedo ]에 검은색을 넣어
                    아무것도 보이지 않게 함..

                    ㄴ   프레넬 공식의 결과만 연출..
                    

                -   램버트 공식을 응용..

                    1)  램버트 = N dot L

                        -   노멀 벡터와 조명 벡터를 내적..

                        -   노멀 벡터와 조명 벡터의
                            각도와 결과..

                            -   각도가 같은 경우..
                                -   가장 밝음..

                            -   각도가 90도..
                                -   가장 어두워짐..


                    
                    2)  조명 벡터 대신 뷰 벡터로
                                    ( 시선 방향 )
                        노멀 벡터와 내적..

                        -   시선이 조명의 역할..

                        -   바라보는 방향이 밝아짐..
                            테두리는 어두워짐..



                    3)  2)의 연산을 반전하여 적용..

                        -   바라보는 방향이 어두워짐..
                            테두리는 밝아짐..


                    4)  테두리의 굵기를 조절..
                        -   3)을 제곱하여 조절..


                    5)  조명과 관계없이
                        일정한 색으로 연출..
                        
                        -   이미션으로 설정..
                            ( Emission )

*/                          
//==========================================================
Shader "Tutor/Fresnel"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM

        //  램버트 라이팅..
        //  -   라이팅 연산 적용..
        //#pragma surface surf Lambert
        #pragma surface surf Lambert noambient

        sampler2D _MainTex;
        float4 _Color;

        struct Input
        {
            float2 uv_MainTex;

            //  시선 벡터 추가..
            //  -   예약어..
            float3 viewDir;
        };


        //  램버트 라이팅..
        //  -   입력 구조체로 [ SurfaceOutput ] 사용..
        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = c.rgb;
            
            //  0)  알베도를 검은색으로..
            o.Albedo = 0;
            
            //  1)  시야 벡터로 내적한 값을 적용..
            //      -   시야 방향이 조명이 되는 셈..
            float actRim;
            float rim = dot(o.Normal, IN.viewDir);
            actRim = rim;

            //  2)  1)을 반전..
            actRim = 1-rim;

            //  3)  2)를 제곱..
            actRim = pow(actRim, 3);


            //  4)  자체색으로 처리..
            o.Emission = actRim;


            o.Alpha = c.a;

        }

    ENDCG
    }
            
    FallBack "Diffuse"

}// Shader "Tutor/VtxColor/Apply_Smooth_Partly"
//==========================================================
/*
    [ 참고 ]

        Unity Custom Light 제작 2강 - 자체 라이트 모델 쉐이더 제작
        https://darkcatgame.tistory.com/14?category=1034169

        표면 셰이더 작성
        https://docs.unity3d.com/kr/2020.3/Manual/SL-SurfaceShaders.html
*/
//==========================================================