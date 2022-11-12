Shader "Custom/DotProduct"
{
    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

            struct Input
            {
                float3 viewDir;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = float3(0,1-dot(IN.viewDir, o.Normal),0);
			}

        ENDCG
    }
    FallBack "Diffuse"
}