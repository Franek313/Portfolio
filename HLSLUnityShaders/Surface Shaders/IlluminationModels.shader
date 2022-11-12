Shader "Custom/IlluminationModels"
{
    Properties
    {
		_myDiffuse ("Diffuse Texture", 2D) = "white" {}
        _myBump ("Bump Texture", 2D) = "bump" {}
        _myBumpSlider ("Bump Amount", Range(0,10)) = 1
        _myScale ("Material Scale", Range(0,10)) = 1
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

			sampler2D _myDiffuse;
            sampler2D _myBump;
            half _myBumpSlider;
            half _myScale;
            

            struct Input
            {
                float2 uv_myDiffuse;
                float2 uv_myBump;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                
                o.Normal = UnpackNormal(tex2D(_myBump,_myScale * IN.uv_myBump));
                o.Normal *= float3(_myBumpSlider, _myBumpSlider,1); 
                o.Albedo = o.Normal;
			}

        ENDCG
    }
    FallBack "Diffuse"
}