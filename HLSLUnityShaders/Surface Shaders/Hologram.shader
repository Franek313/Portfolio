Shader "Custom/Hologram"
{
    Properties
    {
		_RimColor ("Rim Color", Color) = (0,0.5,0.5,0)
        _RimPower ("RimPower", Range(0.5,8)) = 3.0
    }
    
    SubShader
    {
        Tags{"Queue" = "Transparent"}

        Pass //Pass, czyli warstwa shadera
        {
            ZWrite On   //zmuszamy shader aby przypisał dane do buforu osi Z
            //ColorMask RGB //wyrenderuj kolor RGB (biel)
            ColorMask 0 //nie renderuj żadnych pikseli w tej warstwie
        }
        
        CGPROGRAM 
            #pragma surface surf Lambert alpha:fade
            struct Input
            {
                float3 viewDir;
            };

            float4 _RimColor;
            float _RimPower;

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                half rim = 1-saturate(dot(normalize(IN.viewDir), o.Normal));
                o.Emission = _RimColor.rgb * pow(rim,_RimPower) * 10; //podnoszenie do potęgi może zaokrąglić wykres efektu 
                o.Alpha = pow (rim, _RimPower);
            }

        ENDCG
    }
    FallBack "Diffuse"
}