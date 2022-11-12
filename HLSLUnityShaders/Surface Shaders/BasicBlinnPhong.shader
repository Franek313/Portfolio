//Shader gdzie sami odtworzyliśmy cieniowanie metodą Blinn'a-Phong'a
Shader "Custom/BasicBlinnPhong"
{
    Properties
    {
		_Colour ("Colour", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags {"Queue" = "Geometry"}
        
        CGPROGRAM
            #pragma surface surf BasicBlinn 

            //funkcja obliczająca cieniowanie modelu metodą Blinn'a-Phong'a
            half4 LightingBasicBlinn (SurfaceOutput s,
                                        half3 lightDir, //kierunek światła
                                        half3 viewDir,  //kierunek patrzenia widza
                                         half atten)    //intensywność
            {
                half h = normalize(lightDir + viewDir); //normalize, czyli normalizacja wektora czyli utworzenie wektora o tym samym kierunku ale wartości 1
                half diff = max (0,dot(s.Normal, lightDir)); //dot(s.Normal, lightDir) iloczyn skalarny wektorów
                float nh = max (0,dot (s.Normal, lightDir)); //funkcja max pobierająca maksimum
                float spec = pow(nh, 48.0); //pow funkcja potęgi
                half4 c;
                //c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
                c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten * _SinTime; //animowany shader _SinTime przechodzi po sinusoidzie w czasie zmieniając kolory
                c.a = s.Alpha;
                return c;
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