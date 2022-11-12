//Shader prezentujący jak używać Alpha Channel
Shader "Custom/AlphaChannel" 
{
    Properties 
    {
		_MainTex ("MainTex", 2D) = "white" {}
    }

    SubShader 
    {
        Tags {"Queue" = "Transparent"} //kolejka renderowania ustawiona na Transparent czyli rzeczy z kanałem alpha renderowane sa na końcu
                                       //renderowanie zaczyna się od tyłu więc pierwszy zostanie wyrenderowany obiekt będący najdalej w kwestii warstw pikseli
        
        CGPROGRAM
            #pragma surface surf Lambert alpha:fade //alpha:fade uruchamia działanie o.Alpha

			sampler2D _MainTex;

            struct Input 
            {
                float2 uv_MainTex; 
            };

            void surf (Input IN, inout SurfaceOutput o) 
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
			}

        ENDCG
    }
    FallBack "Diffuse" 
}