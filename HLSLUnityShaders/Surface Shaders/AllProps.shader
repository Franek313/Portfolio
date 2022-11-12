//Ten shader prezentuje implementację większości funkcji shaderów Unity
Shader "Custom/AllProps"
{
    Properties
    {
        _myColor ("Example Color", Color) = (1,1,1,1)
		_myRange ("Example Range", Range(0,5)) = 1
		_myTex ("Example Texture", 2D) = "white" {}
		_myCube ("Example Cube", CUBE) = "" {}
		_myFloat ("Example Float", Float) = 0.5
		_myVector ("Example Vector", Vector) = (0.5,1,1,1)
    }
    

    SubShader
    {
        CGPROGRAM
            #pragma surface surf Lambert

			fixed4 _myColor; //zmienne z Properies muszą mieć takie same nazwy jak zmienne zdefiniowane w CGPROGRAM
			half _myRange;
			sampler2D _myTex;
			samplerCUBE _myCube;
			float _myFloat;
			float4 _myVector;

            struct Input
            {
                float2 uv_myTex; //aby użyć jakiejś mapy uv na modelu i przypisać teksturę trzeba to zapisać w konwencji uvNazwaTekstury
				float3 worldRefl;
            };

            void surf (Input IN, inout SurfaceOutput o) //shader function
            {
                o.Albedo = (_myColor * _myRange * tex2D(_myTex, IN.uv_myTex)).rgb;
				o.Albedo.g = 1; //do albedo przypisujemy teksturę 2D (zawartą w zmiennej _myTex) określoną UV-mapą z obiektu IN następnie pobierane są kolory poprzez odwołanie do zmiennej rgb
				o.Emission = texCUBE(_myCube, IN.worldRefl).rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}