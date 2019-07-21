Shader "Unlit/Background"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [PowerSlider(0.5)]_EllipseX("EllipseX", Range(0, 1)) = 0
        [PowerSlider(0.5)]_EllipseY("EllipseY", Range(0, 1)) = 0
        [PowerSlider(0.5)]_EllipseWidth("Ellipse Width", Range(0, 0.1)) = 0
        _STY("Center Y", Float) = 0.5

        [PowerSlider(0.5)]_RemainWidth("Remain Width", Range(0, 100)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;
            float _EllipseX;
            float _EllipseY;
            float _EllipseWidth;
            float _STY;
            float _RemainWidth;

            fixed4 frag (v2f_img i) : SV_Target
            {
                float2 st = float2(0.5, _STY);
                float r1 = pow(((i.uv.x-st.x)/_EllipseX), 2) + pow(((i.uv.y-st.y)/_EllipseY), 2);
                float r2 = pow(((i.uv.x-st.x)/(_EllipseX-_EllipseWidth)), 2) + pow(((i.uv.y-st.y)/(_EllipseY-_EllipseWidth)), 2);
                if(r1 < 1 && r2 > 1)
                {
                    return 0;
                }

                float ly = _STY + 0.1;
                // float w;

                float w = (_RemainWidth - i.uv.x) * 0.03;

                // if(_RemainWidth < 0.95)
                // {
                //     w = (_RemainWidth - i.uv.x) * 0.04;
                // } else
                // {
                //     w = (_RemainWidth - sqrt(i.uv.x+0.05)) * (0.15 + i.uv.x*0.01) + 0.01;
                // }

                float s1 = ly + w;
                float s2 = ly - w;
                if(i.uv.y < s1 && i.uv.y > s2)
                {
                    return 0;
                }

                return _Color;
            }
            ENDCG
        }
    }
}
