﻿//Podstawowy shader operowania na wierzchołkach
Shader "CustomVertex/UVChallenge1"
{
    Properties 
    {
		_MainTex ("Texture",2D) = "white" {}
    }

    SubShader
    { 
        Pass
        {
            CGPROGRAM
                #pragma vertex vert 
                #pragma fragment frag 

                #include "UnityCG.cginc" 

				struct appdata
				{
					float4 vertex : POSITION;
                    float2 uv : TEXCOORD;
				};

                struct v2f 
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION; 
                    float4 color : COLOR; 
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert (appdata v) //lepiej manipulować kolorami wierzchołków tutaj bo to wykonuje się dla
                {                    //każdego wierzchołka
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.color.r = v.uv.x;
                    return o;
                }

                fixed4 frag (v2f i) : SV_TARGET //a to wykonuje się dla każdego piksela
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col * i.color;
                }
            ENDCG
        }
    }
}