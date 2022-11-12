//Przykład shadera stosującego Blending
Shader "Custom/BlendTest" 
{
    Properties 
    {
		_MainTex ("Texture", 2D) = "black" {}
    }

    SubShader 
    {
        Tags{"Queue" = "Transparent"}

        Blend One One //Dodaje do siebie wartości kolorów modelu i renderowanej klatki

        Pass
        {
            SetTexture [_MainTex] {combine texture}    
        }
    }
}