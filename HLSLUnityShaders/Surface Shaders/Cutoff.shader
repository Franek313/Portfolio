Shader "Custom/Cutoff"
{
    Properties
    {
		_RimColor ("Rim Color", Color) = (0,0.5,0.5,0)
        _MainTexture ("Main Texture", 2D) = "white" {}
        _RimPower ("RimPower", Range(0.0,1.0)) = 0.5
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert
            struct Input
            {
                float2 uv_MainTexture;
                float3 viewDir;
                float3 worldPos;
            };

            float4 _RimColor;
            float _RimPower;
            sampler2D _MainTexture;

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                half rim = 1-saturate(dot(normalize(IN.viewDir), o.Normal));
                //instrukcja warunkowa jeśli rim > 0.8 pomnóż przez rim, jeśli nie to pomnóż przez 0
                //o.Emission = _RimColor.rgb * rim > 0.6 ? rim: 0; //kolorowanie konturu, to jeszcze nie outline ale coś w ten deseń
                //o.Emission = rim > _RimPower ? (0.5*tex2D(_MainTexture, IN.uv_MainTexture)).rgb : tex2D(_MainTexture, IN.uv_MainTexture).rgb; //pierwszy względnie ładny toon shader
                //o.Emission = IN.worldPos.y >1 ? float3(0,1,0): float3(1,0,0); //ten shader koloruje w zależności od pozycji w osi y dla współrzędnych świata
                o.Emission = frac(IN.worldPos.y*10 * 0.5) > 0.4 ? //funkcja frac zwraca wartości po przecinku
                float3(0,1,0)*rim: float3(1,0,0)*rim;
            }   

        ENDCG
    }
    FallBack "Diffuse"
}