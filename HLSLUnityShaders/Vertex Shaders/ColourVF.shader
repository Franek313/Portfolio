//Podstawowy shader operowania na wierzchołkach
Shader "CustomVertex/ColourVF"
{
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
				};

                struct v2f 
                {
                    float4 vertex : SV_POSITION; 
                    float4 color : COLOR; 
                };

                v2f vert (appdata v) //lepiej manipulować kolorami wierzchołków tutaj bo to wykonuje się dla
                {                    //każdego wierzchołka
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    //o.color.r = (v.vertex.x+10)/10;
                    //o.color.g = (v.vertex.z+10)/10;
                    return o;
                }

                fixed4 frag (v2f i) : SV_TARGET //a to wykonuje się dla każdego piksela
                {
                    fixed4 col = i.color;
                    col.r = i.vertex.x/1000; //operuje na dla pozycji pikseli w świecie czyli
                    col.g = i.vertex.y/1000; //zmieniając koordynaty modelu zmieniam kolor modelu
                    return col;
                }
            ENDCG
        }
    }
}