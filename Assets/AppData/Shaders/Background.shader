Shader "Unlit/Background"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [PowerSlider(0.5)]_EllipseX("EllipseX", Range(0, 1)) = 0
        [PowerSlider(0.5)]_EllipseY("EllipseY", Range(0, 1)) = 0
        [PowerSlider(0.5)]_Width("Ellipse Width", Range(0, 0.1)) = 0
        _STX("Center X", Float) = 0.5
        _STY("Center Y", Float) = 0.5
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
            float _Width;
            float _STX;
            float _STY;

            fixed4 frag (v2f_img i) : SV_Target
            {
                float2 st = float2(_STX, _STY);
                float r1 = pow(((i.uv.x-st.x)/_EllipseX), 2) + pow(((i.uv.y-st.y)/_EllipseY), 2);
                float r2 = pow(((i.uv.x-st.x)/(_EllipseX-_Width)), 2) + pow(((i.uv.y-st.y)/(_EllipseY-_Width)), 2);
                if(r1 < 1 && r2 > 1)
                {
                    return 0;
                }
                return _Color;
            }
            ENDCG
        }
    }
}
