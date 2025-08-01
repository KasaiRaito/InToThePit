Shader "Custom/StencilMask"
{
SubShader
    {
        Tags { "Queue"="Geometry+1" }
        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }
        ColorMask 0 // Do not render any color
        ZWrite Off  // Prevents depth conflicts
        ZTest Always // Ensures it's always applied
        Pass {}
    }
}
