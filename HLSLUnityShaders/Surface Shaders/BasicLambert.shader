//Nasza własna definicja modelu oświetlenia typu Lambert
Shader "Custom/BasicLambert"
{
    Properties
    {
		_Colour ("Colour", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags {"Queue" = "Geometry"}
        
        CGPROGRAM
            #pragma surface surf BasicLambert 
            
            //Funkcja oświetlająca model metodą Lamberta
            half4 LightingBasicLambert (SurfaceOutput s, //dane powierzchni modelu
                                        half3 lightDir, //kierunek światła
                                         half atten) //intensywność światła
            {
                half NdotL = dot (s.Normal, lightDir);
                half4 c;
                c.rgb = s.Albedo * _LightColor0.rgb //_LightColor0 wbudowana zmienna unity przechowująca kolor wszystkich świateł padających na model
                        * (NdotL * atten);
                c.a = s.Alpha;
                return c; //wygenerowany kolor piksela
            }
			float4 _Colour;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = _Colour.rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}