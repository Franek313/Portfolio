Shader "Custom/MyFirstShader"
{
    Properties
    {
        _myColour ("Example Colour", Color) = (1,1,1,1)
        _myEmission ("Example Emission", Color) = (1,1,1,1)
        _myNormal ("Example Normal", Color) = (1,1,1,1)
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert //working on surface using function called surf using Lambert lighting mode

            struct Input
            {
                float2 uvMainTex;
            };

            fixed4 _myColour;  //properties we want to be available to our shader funcion
            fixed4 _myEmission;
            fixed4 _myNormal;

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = _myColour.rgb;
                o.Emission = _myEmission.rgb;
                o.Normal = _myNormal.rgb;
            }

        ENDCG
    }
    FallBack "Diffuse"
}