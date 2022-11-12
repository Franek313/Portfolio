//Shader pokazujący działanie oświetlenia metodą Blinn'a-Phong'a
Shader "Custom/BasicBlinn"
{
    Properties
    {
		_Colour ("Colour", Color) = (1,1,1,1)
        _SpecColor ("Colour", Color) = (1,1,1,1) //ta zmienna nie musi być definiowana bo Unity ma ją zdefiniowaną z automatu
        _Spec ("Specular", Range(0,1)) = 0.5     //slider
        _Gloss ("Gloss", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags {"Queue" = "Geometry"}
        
        CGPROGRAM
            #pragma surface surf BlinnPhong //metodę oświetlenia ustawiamy na metodę Blinn'a-Phong'a

			float4 _Colour; //kolor
            half _Spec;     //wartość specular
            fixed _Gloss;   //wartość gloss

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = _Colour.rgb; //albedo ustawiamy na _Color
                o.Specular = _Spec;     //specular na _Spec
                o.Gloss = _Gloss;       //gloss na _Gloss
			}

        ENDCG
    }
    FallBack "Diffuse"
}