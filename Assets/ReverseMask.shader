Shader "UI/ReverseMask"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent"}
        Pass
        {
            ZWrite Off
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 tex = tex2D(_MainTex, i.texcoord);
                // Invert alpha to make circle area transparent
                tex.a = 1 - tex.a;
                return tex;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
