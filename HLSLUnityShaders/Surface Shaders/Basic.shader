//Podstawowy shader
Shader "Custom/Basic" //ścieżka i nazwa shadera
{
    Properties //Tutaj definiujemy pola które pojawią się w Material
    {
		_Color ("Color", Color) = (1,1,1,1) //definiujemy pole _Color
    }

    SubShader //ciało shadera
    {
        Tags {"Queue" = "Geometry"} //kolejka renderowania Geometry = 2000
        
        CGPROGRAM
            #pragma surface surf Lambert //dla powierzchni definiujemy funkcję surf 
                                         //wykonująco operacje na wyglądzie powierzchni modelu
                                         //stosujemy model oświetlenia metodą Lamberta

			float4 _Color; //definiujemy zmienną _Color

            struct Input //dane wejściowe
            {
                float2 uv_MainTex; //określamy UV mapę dla tekstury _MainTex
            };

            void surf (Input IN, inout SurfaceOutput o) //funkcja surf
            {
                o.Albedo = _Color.rgb; //przypisujemy do Albedo modelu kolor z pola _Color
			}

        ENDCG
    }
    FallBack "Diffuse" //Jeśli nasz shader nie zadziała zaaplikuj ten
}