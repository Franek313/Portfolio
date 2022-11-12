//Shader prezentujący jak używać Backface Culling (renderowanie obu stron face'a)
Shader "Custom/AlphaBackfaceCulling" 
{
    Properties 
    {
		_MainTex ("Texture", 2D) = "black" {}
    }

    SubShader 
    {
        Tags {"Queue" = "Transparent"} 
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off //wyłączamy Culling czyli będziemy też renderować wewnętrzne strony face'a
                 //ta opcja najlepiej wyglada przy particlach, liściach itd
        Pass
        {
            SetTexture [_MainTex] {combine texture}
        }
    }
}