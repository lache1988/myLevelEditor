Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			if (1>=IN.worldPos.y)
			{
				fixed4 s = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				s.g = 0.55;
				s.r = 0;
				s.b = 0;
				o.Albedo = s.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = s.a;
			}
			else if(2>=IN.worldPos.y )
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				c.r = 0.55;
				c.g = 0.31;
				c.b = 0;
				o.Albedo = c.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			else if (3>=IN.worldPos.y )
			{
				fixed4 a = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				a.r = 0.5;
				a.g = 0;
				a.b = 0;
				o.Albedo = a.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = a.a;
			}
			else if (4 >= IN.worldPos.y)
			{
				fixed4 a = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				a.r = 0.4;
				a.g = 0.85;
				a.b = 0.76;
				o.Albedo = a.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = a.a;
			}
			else if (5 >= IN.worldPos.y)
			{
				fixed4 a = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				a.r = 0.9;
				a.b = 0.9;
				a.g = 0.9;
				o.Albedo = a.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = a.a;
			}
        }
        ENDCG
    }
    FallBack "Diffuse"
}
