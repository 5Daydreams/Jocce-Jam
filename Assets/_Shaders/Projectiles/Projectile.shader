Shader "ShaderKiyoshi/Projectile" 
{
	Properties {
		_MainTex ("Main Tex", 2D) = "white" {}
		_CellSize ("Cell Size", Range(0, 5)) = 1
		_Color1 ("Color 1", Color) = (1,1,1,1)
		_Color0 ("Color 0", Color) = (1,1,1,1)
		_Speed ("Speed ", Float) = 1
		_Radius ("Radius ", Range(0.1,1.0)) = 1
	}
	SubShader {
		Blend One OneMinusSrcAlpha

		Tags{ "RenderType"="Transparent" "Queue"="Geometry"}
		
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows alpha:blend

		#include "Assets/_Shaders/Random.cginc"
		
		float _CellSize;
		float _Jitter;
		float _Speed;
		float _Radius;
		float4 _Color0;
		float4 _Color1;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		float easeIn(float interpolator){
			return interpolator * interpolator;
		}

		float easeOut(float interpolator){
			return 1 - easeIn(1 - interpolator);
		}

		float easeInOut(float interpolator){
			float easeInValue = easeIn(interpolator);
			float easeOutValue = easeOut(interpolator);
			return lerp(easeInValue, easeOutValue, interpolator);
		}

		float perlinNoise(float3 value){
			float3 fraction = frac(value);

			float interpolatorX = easeInOut(fraction.x);
			float interpolatorY = easeInOut(fraction.y);
			float interpolatorZ = easeInOut(fraction.z);

			float cellNoiseZ[2];
			[unroll]
			for(int z=0;z<=1;z++){
				float cellNoiseY[2];
				[unroll]
				for(int y=0;y<=1;y++){
					float cellNoiseX[2];
					[unroll]
					for(int x=0;x<=1;x++){
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

		void surf (Input i, inout SurfaceOutputStandard o)
		{
			float2 center = float2(0.5,0.5);
			float distToCenter = length(i.uv_MainTex - center);
			float cutoff = step(distToCenter,_Radius);

			float3 value = (i.worldPos) / _CellSize;
			float noise = (perlinNoise(value-_Speed *_Time.xxz)+1)/2;

			float4 finalValue = lerp(_Color0,_Color1,noise) * cutoff;
			
			o.Albedo = finalValue;
			o.Alpha = cutoff;
		}
		ENDCG
	}
	FallBack "Standard"
}