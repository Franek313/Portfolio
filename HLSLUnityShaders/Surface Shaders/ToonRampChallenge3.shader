Shader "Custom/ToonRampChallenge3"
{
    Properties
    {
		_Colour ("Colour", Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _RampTex ("Ramp Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags {"Queue" = "Geometry"}
        
        CGPROGRAM
            #pragma surface surf ToonRamp

            float4 _Colour;
            sampler2D _RampTex;
            sampler2D _MainTex;

            half4 LightingToonRamp (SurfaceOutput s,
                                        fixed3 lightDir, 
                                         fixed atten) 
            {
                float diff = dot(s.Normal, lightDir);
                float h = diff * 0.5 + 0.5;
                float2 rh = h;
                float3 ramp = tex2D(_RampTex, rh).rgb;

                float4 c;
                c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
                c.a = s.Alpha;
                return c;
            }
			

            struct Input
            {
                float2 uv_MainTex;
                float3 viewDir;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                float diff = dot (o.Normal, IN.viewDir);
			    float h = diff * 0.5 + 0.5;
			    float2 rh = h;
			    o.Albedo = tex2D(_RampTex, rh).rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}