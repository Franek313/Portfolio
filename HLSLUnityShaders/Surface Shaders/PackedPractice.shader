Shader "Custom/PackedPractice"
{
    Properties
    {
        _myColour ("Example Colour", Color) = (1,1,1,1)
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

            struct Input
            {
                float2 uvMainTex;
            };

            fixed4 _myColour;  //properties we want to be available to our shader funcion

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = _myColour.rgb;
            }

        ENDCG
    }
    FallBack "Diffuse"
}