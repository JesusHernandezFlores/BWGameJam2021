Shader "Custom/Lighting Model/HD_ToonShader"
{
    Properties
    {
        [Header(Main)]
        _MainTex ("Texture", 2D) = "white" {}
        _PrimaryColor("Primary Color", Color) = (1,1,1,1)
        [HDR] _AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
        _DarkSideStrength("Dark Side Strength", Float) = 0.5

        //Specular
        [Header(Specularity)]
        [Toggle(SPEC_ON)] _SpecularToggle ("Specular", Float) = 0
        [HDR] _SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)
        _Glossiness ("Glossiness", Float) = 32

        //Rim
        [Header(Rim Lighting)]
        [Toggle(RIM_ON)] _RimToggle ("Rim", Float) = 0
        [HDR] _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount ("Rim Amoutn", Float) = 0.716
        _RimThreshold ("Rim Threshold", Float) = 0.1
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque" 
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ RIM_ON
            #pragma multi_compile __ SPEC_ON

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal: NORMAL;
                float3 viewDir : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST; 
            fixed4 _PrimaryColor;
            fixed4 _AmbientColor;
            fixed _DarkSideStrength;

            fixed4 _SpecularColor;
            fixed _Glossiness;

            fixed4 _RimColor;
            fixed _RimAmount;
            fixed _RimThreshold;


            v2f vert (appdata v)
            {
                v2f o;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                //Specularity
                o.viewDir = WorldSpaceViewDir(v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target 
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                fixed3 normal = normalize(i.worldNormal);
                fixed NdotL = dot(_WorldSpaceLightPos0, normal);
                fixed lightIntensity = NdotL > 0 ? 1 : (1 -_DarkSideStrength);
                //fixed lightIntensity = smoothstep(0, 0.01, NdotL);
                fixed4 light = lightIntensity * _LightColor0;

                half3 viewDir = normalize(i.viewDir);
                
                //Specularity
                half4 specular = 0;
                #if SPEC_ON
                    float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                    float NdotH = dot(normal, halfVector);
                    half specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                    half specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                    specular = specularIntensitySmooth * _SpecularColor;
                #endif

                // Rim Lighting
                fixed4 rim = 0;
                #if RIM_ON
                    half4 rimDot = 1 - dot(viewDir, normal);
                    half rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                    rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                    rim = rimIntensity * _RimColor;
                #endif

                // sample the texture
                float4 sample = tex2D(_MainTex, i.uv);
                
                return ((_AmbientColor + specular + rim) * _PrimaryColor * sample);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
