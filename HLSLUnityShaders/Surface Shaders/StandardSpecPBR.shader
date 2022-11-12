Shader "Custom/StandardSpecPBR"
{
    Properties
    {
		_Color ("Color", Color) = (1,1,1,1)
        _MetallicTex ("Metallic (R)", 2D) = "white" {}
        _Metallic ("Metallic", Range(0,1)) = 0.5
        _SpecColor ("Specular", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags {"Queue" = "Geometry"}
        
        CGPROGRAM
            #pragma surface surf StandardSpecular //Standardowy system oświetlenia

			sampler2D _MetallicTex;
            fixed4 _Color;

            struct Input
            {
                float2 uv_MetallicTex;
            };

            void surf (Input IN, inout SurfaceOutputStandardSpecular o) //SurfaceOutput musieliśmy zmienić na Surface Standard Output
            {
                o.Albedo = _Color.rgb;
                o.Smoothness = tex2D(_MetallicTex, IN.uv_MetallicTex).r;
                o.Specular = _SpecColor.rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}