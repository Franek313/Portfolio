Shader "Custom/BumpedEnvChallenge1"
{
    Properties
    {
		_myDiffuse ("Diffuse Texture", 2D) = "white" {}
        _myBump ("Bump Texture", 2D) = "bump" {}
        _myBumpSlider ("Bump Amount", Range(0,10)) = 1
        _myBright ("Brightness", Range(0,10)) = 1
        _myCube ("Cube Map", CUBE) = "white" {}
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

			sampler2D _myDiffuse;
            sampler2D _myBump;
            samplerCUBE _myCube;
            half _myBumpSlider;
            half _myBright;
            

            struct Input
            {
                float2 uv_myDiffuse;
                float2 uv_myBump;
                float3 worldRefl; INTERNAL_DATA //normalnie worldRefl jest ściśle związany z Normal Mapami aby rozdzielić te dane w celu użycia ich w linijce 37 należy dopisać INTERNAL_DATA
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = texCUBE(_myCube, IN.worldRefl).rgb;
                o.Normal = UnpackNormal(tex2D(_myBump, IN.uv_myBump)) * 0.3;
                o.Normal *= float3(_myBumpSlider, _myBumpSlider,1); 
                o.Emission = texCUBE(_myCube, WorldReflectionVector(IN,o.Normal)).rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}