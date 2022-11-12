Shader "Custom/PropsChallenge4"
{
    Properties
    {
		_myTex ("Example Texture", 2D) = "white" {}
        _myEmission ("Example Emission", 2D) = "white" {}
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

			sampler2D _myTex;
            sampler2D _myEmission;

            struct Input
            {
                float2 uv_myTex; //aby użyć jakiejś mapy uv na modelu i przypisać teksturę trzeba to zapisać w konwencji uvNazwaTekstury
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = tex2D(_myTex, IN.uv_myTex).rgb;
                o.Emission = tex2D(_myEmission, IN.uv_myTex).rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}