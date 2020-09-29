Shader "Hidden/BloodLineEffect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_RampTex ("Base (RGB)", 2D) = "grayscaleRamp" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform half _bloodLine;
uniform float4 _color;

fixed4 frag (v2f_img i) : COLOR
{
	fixed4 original = tex2D(_MainTex, i.uv);
	float height = i.uv.y;
	
	if(height > _bloodLine)
	{
		original.r*=_color.r;
		original.g	=0;		
		original.b*=_color.b;
		
	}
	
	return original;
}
ENDCG

	}
}

Fallback off

}