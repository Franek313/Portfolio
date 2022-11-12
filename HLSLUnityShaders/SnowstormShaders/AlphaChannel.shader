//Prosty shader Alpha Channel do spriteów
Shader "HolyArts/AlphaChannel" 
{
    Properties 
    {
		_MainTex ("MainTex", 2D) = "white" {}
        _Opacity ("Opacity", Range(0.0,1.0)) = 0.5
    }

    SubShader 
    {
        Tags {"Queue" = "Transparent"} //kolejka renderowania ustawiona na Transparent czyli rzeczy z kanałem alpha renderowane sa na końcu
                                       //renderowanie zaczyna się od tyłu więc pierwszy zostanie wyrenderowany obiekt będący najdalej w kwestii warstw pikseli
        
        CGPROGRAM
            #pragma surface surf Lambert alpha:fade //alpha:fade uruchamia działanie o.Alpha

			sampler2D _MainTex;
            half _Opacity;

            struct Input 
            {
                float2 uv_MainTex; 
            };

            void surf (Input IN, inout SurfaceOutput o) 
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a*_Opacity;
			}

        ENDCG
    }
    FallBack "Diffuse" 
}