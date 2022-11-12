Shader "Custom/PropsChallenge3"
{
    Properties
    {
		_myTex ("Example Texture", 2D) = "white" {}
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

			sampler2D _myTex;

            struct Input
            {
                float2 uv_myTex; //aby użyć jakiejś mapy uv na modelu i przypisać teksturę trzeba to zapisać w konwencji uvNazwaTekstury
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                float4 green = float4(0,1,0,1); 
                o.Albedo = (tex2D(_myTex, IN.uv_myTex)*green).rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}