Shader "Custom/MagicGlow"
{
    Properties
    {
		_RimColor ("Magic Glow Color", Color) = (0,0.5,0.5,0)
        _RimPower ("Thickness", Range(0.5,8)) = 3.0
        _MainTex ("Main Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags{"Queue" = "Transparent"}

        CGPROGRAM
        #pragma surface surf Lambert
        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutput o) //shader function
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
        }
        ENDCG
        
        
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