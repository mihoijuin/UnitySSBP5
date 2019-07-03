Shader "Unlit/SSBP5OP"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _HLWidth;
            float _VLWidth;

            fixed4 frag (v2f_img i) : COLOR
            {
                float vd = 0.28;
                float hd = 0.35;
                float minV = vd - _VLWidth;
                float maxV = vd + _VLWidth;
                float minH = hd - _HLWidth*0.8;
                float maxH = hd + _HLWidth*0.8;
                if((i.uv.x > minV && i.uv.x < maxV) || (i.uv.y > minH && i.uv.y < maxH))
                {
                    return fixed4(1,1,1,1);
                } else
                {
                    return fixed4(0,0,0,1);
                }
            }
            ENDCG
        }
    }
}
