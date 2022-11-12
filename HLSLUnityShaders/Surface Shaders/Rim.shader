Shader "Custom/Rim"
{
    Properties
    {
		_RimColor ("Rim Color", Color) = (0,0.5,0.5,0)
        _RimPower ("RimPower", Range(0.5,8)) = 3.0
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert
            struct Input
            {
                float3 viewDir;
            };

            float4 _RimColor;
            float _RimPower;

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                half rim = 1-saturate(dot(normalize(IN.viewDir), o.Normal));
                o.Emission = _RimColor.rgb * pow(rim,_RimPower); //podnoszenie do potęgi może zaokrąglić wykres efektu 
			}

        ENDCG
    }
    FallBack "Diffuse"
}