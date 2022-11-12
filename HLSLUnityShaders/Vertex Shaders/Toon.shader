Shader "Custom/Toon"
{
    Properties
    {
		
        _MainTex ("Main Texture", 2D) = "white" {}
        _Colour ("Color", Color) = (1,1,1,1)
        _EmissionTex ("Emission Texture", 2D) = "black" {}
        _EmissionIntensity ("Emission Intensity", Range(0,1)) = 0.5
        _Outline ("Outline Thickness", Range(-0.1,0.5)) = .005
        _OutlineColour ("Outline Colour", Color) = (1,1,1,1)
        _RampTex ("Ramp Texture", 2D) = "white" {}
        

    }

    SubShader
    {
        CGPROGRAM
            #pragma surface surf ToonRamp
            
            float4 _Colour;
            half _Atten;
            sampler2D _RampTex;
            sampler2D _MainTex;
            sampler2D _EmissionTex;
            half _EmissionIntensity;

            half4 LightingToonRamp (SurfaceOutput s,
                                        half3 lightDir, 
                                         half atten) 
            {
                float diff = dot(s.Normal, lightDir);
                float h = diff * 0.5 + 0.5;
                float2 rh = h;
                float3 ramp = tex2D(_RampTex, rh).rgb;
                float3 emission = tex2D(_EmissionTex, rh).rgb;

                float4 c;
                c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
                c.a = s.Alpha;
                return c;
            }
			

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Colour;
                o.Emission = tex2D(_EmissionTex, IN.uv_MainTex).rgb * _EmissionIntensity;
			}

        ENDCG
        
        Pass //zastosowanie tylko tego Pass'a powoduje fajny efekt renderowania samego cienia modelu
        {
                Tags {"LightMode" = "ShadowCaster"}

                CGPROGRAM
                #pragma vertex vert 
                #pragma fragment frag 
                #pragma multi_compile_shadowcaster

                #include "UnityCG.cginc" 

                struct appdata
				{
					float4 vertex : POSITION; //to wszystko zostanie przeliczone na rzucany cień
                    float3 normal : NORMAL;
                    float4 texcoord : TEXCOORD0;
				};

                struct v2f 
                {
                    V2F_SHADOW_CASTER; //output to będzie cień
                };

                v2f vert (appdata v)
                {                   
                    v2f o;
                    TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);
                    return o;
                }

                fixed4 frag (v2f i) : SV_Target 
                {
                    SHADOW_CASTER_FRAGMENT(i);
                }
            ENDCG
        }
        Pass //Outline Pass
        {
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };

            float _Outline;
            float4 _OutlineColour;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                float2 offset = TransformViewToProjection(norm.xy);

                o.pos.xy += offset * o.pos.z * _Outline;
                o.color = _OutlineColour;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}