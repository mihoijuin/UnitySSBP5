Shader "Unlit/Silhouette"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [PowerSlider(1)]_ScaleX ("Scale X", Range(0, 2)) = 1
        [PowerSlider(1)]_ScaleY ("Scale Y", Range(0, 2)) = 1
        [PowerSlider(1)]_RotateZ ("Rotate Z", Range(-180, 180)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScaleX;
            float _ScaleY;
            float _RotateZ;

            v2f vert (appdata v)
            {
                float4x4 sizeMat = float4x4
                (
                    float4(_ScaleX, 0, 0, 0),
                    float4(0, _ScaleY, 0, 0),
                    float4(0, 0, 1, 0),
                    float4(0, 0, 0, 1)
                );
                v.vertex = mul(sizeMat, v.vertex);

                float sinZ = sin(radians(_RotateZ));
                float cosZ = cos(radians(_RotateZ));
                float4x4 rotMat = float4x4
                (
                    float4(cosZ, -sinZ, 0, 0),
                    float4(sinZ, cosZ, 0, 0),
                    float4(0, 0, 1, 0),
                    float4(0, 0, 0, 1)
                );
                v.vertex = mul(rotMat, v.vertex);

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = float3(0,0,0);
                return col;
            }
            ENDCG
        }
    }
}
