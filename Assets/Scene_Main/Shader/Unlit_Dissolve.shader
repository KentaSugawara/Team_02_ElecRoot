Shader "MyShader/Enemy/Unlit_Dissolve"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DissolveTex("DissolveTex", 2D) = "white" {}
		_Threshold("Threshold", Range(-1.0, 2.0)) = 0.0
		_Saturation("Saturation", Range(0, 1)) = 0.0
		_Width("Width", Range(0, 1)) = 0.0
		[MaterialToggle] _Inverse("Inverse", Float) = 0 // 0 is false, 1 is true
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent"
		"RenderType" = "Transparent" }
		LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
	};

	sampler2D _MainTex;
	sampler2D _DissolveTex;
	float4 _MainTex_ST;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed _Saturation;
	fixed _Threshold;
	fixed _Width;
	float _Inverse;

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed3 col = tex2D(_MainTex, i.uv);
		fixed alpha = 1.0;

		fixed d;
		if (_Inverse)
			d = tex2D(_DissolveTex, i.uv).r;
		else
			d = 1.0 - tex2D(_DissolveTex, i.uv).r;

		if (d <= _Threshold) {
			fixed f = (_Threshold - d);
			if (f < _Width)
				alpha = clamp(1.0 - f / _Width, -0.1, 1.1);
			else
				discard;
		}

		
		// apply fog
		UNITY_APPLY_FOG(i.fogCoord, col);
		return fixed4(col * _Saturation, alpha);
		}
			ENDCG
		}
	}
}
