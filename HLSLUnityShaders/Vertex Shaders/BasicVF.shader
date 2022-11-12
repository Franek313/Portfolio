//Podstawowy shader operowania na wierzchołkach
Shader "CustomVertex/BasicVF"
{
    Properties 
    {
		_MainTex ("Texture",2D) = "white" {}
    }

    SubShader
    { 
		Tags{"RenderType" = "Opaque"}
		LOD 100

        Pass //Każdy kod musi być zawarty w Passie
        {
            CGPROGRAM
                #pragma vertex vert //definicja funkcji operującej na wierzchołkach
                #pragma fragment frag //definicja funkcji operującej na fragmentach (obliczany jest każdy pixel face'a)
                #pragma multi_compile_fog //funkcja obsługująca mgłę

                #include "UnityCG.cginc" //uwzględniamy bibliotekę zawierającą funkcje Unity dla shaderów w CG/HLSL

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				//struktura zawierająca dane wejściowe wierzchołków i zmienne do operowania na fragmentach
                struct v2f 
                {
                    float2 uv : TEXCOORD0; //przypisujemy mapę UV do zmiennej TEXCOORD0
                    UNITY_FOG_COORDS(1) // opisujemy koordynaty mgły
                    float4 vertex : SV_POSITION; //przypisujemy dane o wierzchołkach do zmiennej SV_POSITION
                };

                sampler2D _MainTex;
                float4 _MainTex_ST; //dane skalowania dla tekstury które zostają użyte w funkcji vert
									//bez końcówki _ST nie zadziała nam ta zmienna w tej funkcji

                v2f vert (appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag (v2f i) : SV_TARGET
                {
                    fixed4 col = tex2D(_MainTex, i.uv); //sampling tekstury
                    UNITY_APPLY_FOG(i.fogCoordm, col); //zatwierdź mgłę
                    return col;
                }
            ENDCG
        }
    }
}