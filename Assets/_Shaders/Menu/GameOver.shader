Shader "ShaderKiyoshi/GameOver"
{
    Properties
    {
        _MainTex("MainTex", 2D ) = "white"{}
        _CellSize ("Cell Size", Range(0, 5)) = 1
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color0 ("Color 0", Color) = (1,1,1,1)
        _Speed ("Speed ", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "Queue"="Transparent"
        }
        Pass
        {

            Blend One OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Assets/_Shaders/Random.cginc"

            float _CellSize;
            float _Speed;
            float4 _Color0;
            float4 _Color1;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float easeIn(float interpolator)
            {
                return interpolator * interpolator;
            }

            float easeOut(float interpolator)
            {
                return 1 - easeIn(1 - interpolator);
            }

            float easeInOut(float interpolator)
            {
                float easeInValue = easeIn(interpolator);
                float easeOutValue = easeOut(interpolator);
                return lerp(easeInValue, easeOutValue, interpolator);
            }

            float perlinNoise(float3 value)
            {
                float3 fraction = frac(value);

                float interpolatorX = easeInOut(fraction.x);
                float interpolatorY = easeInOut(fraction.y);
                float interpolatorZ = easeInOut(fraction.z);

                float cellNoiseZ[2];
                [unroll]
                for (int z = 0; z <= 1; z++)
                {
                    float cellNoiseY[2];
                    [unroll]
                    for (int y = 0; y <= 1; y++)
                    {
                        float cellNoiseX[2];
                        [unroll]
                        for (int x = 0; x <= 1; x++)
                        {
                            float3 cell = floor(value) + float3(x, y, z);
                            float3 cellDirection = rand3dTo3d(cell) * 2 - 1;
                            float3 compareVector = fraction - float3(x, y, z);
                            cellNoiseX[x] = dot(cellDirection, compareVector);
                        }
                        cellNoiseY[y] = lerp(cellNoiseX[0], cellNoiseX[1], interpolatorX);
                    }
                    cellNoiseZ[z] = lerp(cellNoiseY[0], cellNoiseY[1], interpolatorY);
                }
                float noise = lerp(cellNoiseZ[0], cellNoiseZ[1], interpolatorZ);
                return noise;
            }

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float3 value = (i.uv.xyx) / _CellSize;
                float noise = (perlinNoise(value - _Speed * _Time.xxw) + 1) / 3;

                float4 finalValue = lerp(_Color0, _Color1, noise);

finalValue = float4(1,1,1,1);
                
                return finalValue;
            }
            ENDCG
        }

    }
}