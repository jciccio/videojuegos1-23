Shader "CI0162/Simple Shader"
{
    Properties
    {
        _GreenAmount("Green Amount", Float) = 1
        _MainColor("Main Color", Color) = (1,1,1,1)
    }

    SubShader{
        Pass {
            CGPROGRAM
            
            #pragma vertex vertices
            #pragma fragment fragments

            float _GreenAmount;
            float4 _MainColor;

            struct data{
                float4 vertex : POSITION;
            };

            struct FromVertToFrag{
                float4 vertex : SV_POSITION;
            };

            FromVertToFrag vertices(data v){
                FromVertToFrag o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 fragments(FromVertToFrag i) : SV_TARGET {
                
                //return float4(0,_GreenAmount,0,1);
                return _MainColor;
            }


            ENDCG
        }
    }
}