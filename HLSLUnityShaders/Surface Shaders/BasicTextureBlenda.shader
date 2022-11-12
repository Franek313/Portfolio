//Podstawowy shader blendujący dwie tekstury ze sobą
Shader "Custom/BasicTextureBlend" 
{
    Properties 
    {
		_MainTex ("Main Texture", 2D) = "white" {}
        _DecalTex ("Decal Texture", 2D) = "white" {}
        [Toggle] _ShowDecal ("Show Decal?", Float) = 0 
        //zmienna float zastępująca w shaderach bool (bool nie ma)
        //w oknie materiału wyświetli się standardowe pole true/false
        //polami tymi można sterować ze skryptu C# co prezentuje
        //dołączony w plikach skrypt DecalOnOff.cs
    }   

    SubShader 
    {
        Tags {"Queue" = "Geometry"} 
        
        CGPROGRAM
            #pragma surface surf Lambert 

			sampler2D _MainTex;
            sampler2D _DecalTex;
            float _ShowDecal;

            struct Input 
            {
                float2 uv_MainTex;
            };

            void surf (Input IN, inout SurfaceOutput o)
            {
                fixed4 a = tex2D(_MainTex, IN.uv_MainTex);
                fixed4 b = tex2D(_DecalTex, IN.uv_MainTex) * _ShowDecal;
                //o.Albedo = a.rgb * b.rgb;
                //o.Albedo = a.rgb + b.rgb;
                o.Albedo = b.r > 0.9 ? b.rgb : a.rgb;
			}

        ENDCG
    }
    FallBack "Diffuse"
}