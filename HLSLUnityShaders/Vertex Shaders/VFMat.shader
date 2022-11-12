//Podstawowy shader operowania na wierzchołkach
Shader "CustomVertex/VFMat"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _ScaleUVX("Scale X", Range(1,10)) = 1
        _ScaleUVY("Scale Y", Range(1,10)) = 1
    }
    SubShader
    { 
        Tags{"Queue" = "Transparent"}
        GrabPass //pass umożliwiający przechwycenie obrazu z kamery
        {    }

        Pass
        {
            CGPROGRAM
                #pragma vertex vert 
                #pragma fragment frag 

                #include "UnityCG.cginc" 

				struct appdata
				{
					float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
				};

                struct v2f 
                {
                    float2 uv : TEXCOORD0; 
                    float4 vertex : SV_POSITION; 
                };

                sampler2D _GrabTexture; //_GrabTexture nazwa kluczowa dla tekstury przechwytującej widok z kamery
                sampler2D _MainTex;
                float4 _MainTex_ST;
                half _ScaleUVX;
                half _ScaleUVY;

                v2f vert (appdata v)
                {                   
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.uv.x = sin(o.uv.x * _ScaleUVX); //modyfikujemy uv w osi x używając sinusa
                    o.uv.y = sin(o.uv.y * _ScaleUVY); //modyfikujemy uv w osi y używając cosinusa
                    return o;
                }

                fixed4 frag (v2f i) : SV_TARGET //a to wykonuje się dla każdego piksela
                {
                    fixed4 col = tex2D(_GrabTexture, i.uv);
                    return col;
                }
            ENDCG
        }
    }
}