Shader "Unlit/Wave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveWidth ("Wave width", Float) = 1
        _WaveSpeed ("Wave Speed", Float) = 1
        _WaveDirection ("Wave Direction", Vector) = (1,1,1,1)
        _WaveAmplitudeOffset ("_WaveAmplitudeOffset", Float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" }

        Pass
        {
            Blend One OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _WaveDirection;
            float _WaveWidth;
            float _WaveSpeed;
            float _WaveAmplitudeOffset;

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


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed dir = dot(_WaveDirection.xy, i.uv.xy);

                fixed4 white = fixed4(1,1,1,1);
                fixed4 black = fixed4(0,0,0,0);

                float sineWave = saturate(frac(dir/_WaveWidth- _WaveSpeed*_Time.y)  + _WaveAmplitudeOffset);  

                float4 value = lerp(sineWave,white,black); 
                
                return fixed4(value);
            }
            ENDCG
        }
    }
}
