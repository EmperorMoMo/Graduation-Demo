// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|custl-2393-OUT,alpha-1204-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31772,y:32347,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:True,tagnrm:False,tex:56dde5616cf640c4da865ca4f3f2d112,ntxv:2,isnm:False|UVIN-4491-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-4513-OUT,B-2053-RGB,C-797-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32022,y:32783,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32022,y:32960,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1204,x:32454,y:32958,varname:node_1204,prsc:2|A-2053-A,B-797-A,C-3000-OUT;n:type:ShaderForge.SFN_TexCoord,id:8292,x:29893,y:31825,varname:node_8292,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ObjectScale,id:468,x:29495,y:32503,varname:node_468,prsc:2,rcp:False;n:type:ShaderForge.SFN_FaceSign,id:440,x:32025,y:32626,varname:node_440,prsc:2,fstp:0;n:type:ShaderForge.SFN_Tex2d,id:5087,x:31783,y:32595,ptovrint:False,ptlb:node_5087,ptin:_node_5087,varname:_node_5087,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:True,tagnrm:False,tex:02da83e1e12b8d24f99bc636f64c0267,ntxv:0,isnm:False|UVIN-9676-OUT;n:type:ShaderForge.SFN_Lerp,id:4513,x:32032,y:32485,varname:node_4513,prsc:2|A-6074-RGB,B-5087-RGB,T-440-VFACE;n:type:ShaderForge.SFN_Vector1,id:789,x:29529,y:32421,varname:node_789,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Slider,id:6776,x:29338,y:32672,ptovrint:False,ptlb:MapXScale,ptin:_MapXScale,varname:_MapXScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.1,max:0.5;n:type:ShaderForge.SFN_Subtract,id:1138,x:30268,y:32409,varname:node_1138,prsc:2|A-4243-OUT,B-6203-OUT;n:type:ShaderForge.SFN_Multiply,id:3125,x:29746,y:32525,varname:node_3125,prsc:2|A-468-X,B-6776-OUT;n:type:ShaderForge.SFN_Add,id:4276,x:30262,y:32619,varname:node_4276,prsc:2|A-4243-OUT,B-6203-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:4824,x:30791,y:32334,varname:node_4824,prsc:2|IN-3396-OUT,IMIN-7731-OUT,IMAX-2906-OUT,OMIN-1138-OUT,OMAX-4276-OUT;n:type:ShaderForge.SFN_Vector1,id:7731,x:30501,y:32144,varname:node_7731,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:2906,x:30501,y:32242,varname:node_2906,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:9238,x:29746,y:32654,varname:node_9238,prsc:2|A-468-Y,B-2134-OUT;n:type:ShaderForge.SFN_Append,id:6203,x:29937,y:32587,varname:node_6203,prsc:2|A-3125-OUT,B-9238-OUT;n:type:ShaderForge.SFN_Slider,id:2134,x:29335,y:32812,ptovrint:False,ptlb:MapYScale,ptin:_MapYScale,varname:_MapYScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.1,max:0.5;n:type:ShaderForge.SFN_Set,id:4788,x:29773,y:32422,varname:node_4788,prsc:2|IN-789-OUT;n:type:ShaderForge.SFN_Get,id:4243,x:29926,y:32411,varname:node_4243,prsc:2|IN-4788-OUT;n:type:ShaderForge.SFN_Parallax,id:4491,x:31049,y:32387,varname:node_4491,prsc:2|UVIN-4824-OUT,HEI-6916-OUT;n:type:ShaderForge.SFN_Slider,id:6482,x:30520,y:32616,ptovrint:False,ptlb:parallax,ptin:_parallax,varname:_parallax,prsc:0,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.418803,max:10;n:type:ShaderForge.SFN_RemapRange,id:6916,x:30849,y:32616,varname:node_6916,prsc:2,frmn:0,frmx:10,tomn:-10,tomx:0|IN-6482-OUT;n:type:ShaderForge.SFN_ComponentMask,id:9515,x:31258,y:32476,varname:node_9515,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-4491-UVOUT;n:type:ShaderForge.SFN_Negate,id:6766,x:31443,y:32497,varname:node_6766,prsc:2|IN-9515-R;n:type:ShaderForge.SFN_Append,id:9676,x:31608,y:32498,varname:node_9676,prsc:2|A-6766-OUT,B-9515-G;n:type:ShaderForge.SFN_Tex2d,id:2085,x:30974,y:32850,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:_Mask,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5de0eafe0c281495b8272d9a1d7c3ea8,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3000,x:31402,y:32942,varname:node_3000,prsc:2|A-2085-A,B-7978-OUT,C-316-OUT;n:type:ShaderForge.SFN_Vector2,id:2963,x:29962,y:32847,varname:node_2963,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_TexCoord,id:320,x:29964,y:33015,varname:node_320,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Distance,id:5793,x:30168,y:32936,varname:node_5793,prsc:2|A-2963-OUT,B-320-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:5962,x:30338,y:32935,varname:node_5962,prsc:2|IN-5793-OUT;n:type:ShaderForge.SFN_Slider,id:316,x:30600,y:33441,ptovrint:False,ptlb:alphaScale,ptin:_alphaScale,varname:_alphaScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Power,id:4922,x:30525,y:32941,varname:node_4922,prsc:2|VAL-5962-OUT,EXP-7727-OUT;n:type:ShaderForge.SFN_Slider,id:7727,x:30154,y:33158,ptovrint:False,ptlb:Edge,ptin:_Edge,varname:_Edge,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Add,id:7978,x:30970,y:33068,varname:node_7978,prsc:2|A-4260-OUT,B-1813-OUT;n:type:ShaderForge.SFN_Slider,id:1813,x:30596,y:33247,ptovrint:False,ptlb:alphaOffset,ptin:_alphaOffset,varname:_alphaOffset,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Power,id:4260,x:30722,y:33028,varname:node_4260,prsc:2|VAL-4922-OUT,EXP-7727-OUT;n:type:ShaderForge.SFN_Tex2d,id:5321,x:28950,y:31778,ptovrint:False,ptlb:BoWenNormal,ptin:_BoWenNormal,varname:_BoWenNormal,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:88402de5ba91aa440ad58f9bbfd372ef,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:7099,x:29503,y:32269,ptovrint:False,ptlb:bowenScale,ptin:_bowenScale,varname:_bowenScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1452992,max:1;n:type:ShaderForge.SFN_Lerp,id:5817,x:29900,y:32058,varname:node_5817,prsc:2|A-5432-OUT,B-1579-UVOUT,T-7099-OUT;n:type:ShaderForge.SFN_Vector1,id:5432,x:29665,y:31977,varname:node_5432,prsc:2,v1:1;n:type:ShaderForge.SFN_ComponentMask,id:5964,x:29241,y:31882,varname:node_5964,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5321-RGB;n:type:ShaderForge.SFN_Add,id:2001,x:30123,y:31982,varname:node_2001,prsc:2|A-8292-UVOUT,B-5817-OUT;n:type:ShaderForge.SFN_Rotator,id:1579,x:29535,y:32057,varname:node_1579,prsc:2|UVIN-5964-OUT,SPD-5674-OUT;n:type:ShaderForge.SFN_Slider,id:5674,x:29096,y:32192,ptovrint:False,ptlb:bowenSpeed,ptin:_bowenSpeed,varname:_bowenSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.5606837,max:5;n:type:ShaderForge.SFN_SwitchProperty,id:3396,x:30480,y:31948,ptovrint:False,ptlb:BoWen,ptin:_BoWen,varname:_BoWen,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8292-UVOUT,B-2001-OUT;proporder:6074-797-5087-6776-2134-6482-2085-316-7727-1813-3396-5321-7099-5674;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        [NoScaleOffset]_MainTex ("MainTex", 2D) = "black" {}
        [HDR]_TintColor ("Color", Color) = (1,1,1,1)
        [NoScaleOffset]_node_5087 ("node_5087", 2D) = "white" {}
        _MapXScale ("MapXScale", Range(0.1, 0.5)) = 0.1
        _MapYScale ("MapYScale", Range(0.1, 0.5)) = 0.1
        _parallax ("parallax", Range(0, 10)) = 3.418803
        _Mask ("Mask", 2D) = "white" {}
        _alphaScale ("alphaScale", Range(0, 10)) = 1
        _Edge ("Edge", Range(0, 10)) = 1
        _alphaOffset ("alphaOffset", Range(-1, 1)) = 0
        [MaterialToggle] _BoWen ("BoWen", Float ) = 0
        _BoWenNormal ("BoWenNormal", 2D) = "bump" {}
        _bowenScale ("bowenScale", Range(0, 1)) = 0.1452992
        _bowenSpeed ("bowenSpeed", Range(0.1, 5)) = 0.5606837
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform sampler2D _MainTex;
            uniform float4 _TintColor;
            uniform sampler2D _node_5087;
            uniform float _MapXScale;
            uniform float _MapYScale;
            uniform fixed _parallax;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _alphaScale;
            uniform float _Edge;
            uniform float _alphaOffset;
            uniform sampler2D _BoWenNormal; uniform float4 _BoWenNormal_ST;
            uniform float _bowenScale;
            uniform float _bowenSpeed;
            uniform fixed _BoWen;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float node_5432 = 1.0;
                float4 node_9072 = _Time;
                float node_1579_ang = node_9072.g;
                float node_1579_spd = _bowenSpeed;
                float node_1579_cos = cos(node_1579_spd*node_1579_ang);
                float node_1579_sin = sin(node_1579_spd*node_1579_ang);
                float2 node_1579_piv = float2(0.5,0.5);
                float3 _BoWenNormal_var = UnpackNormal(tex2D(_BoWenNormal,TRANSFORM_TEX(i.uv0, _BoWenNormal)));
                float2 node_1579 = (mul(_BoWenNormal_var.rgb.rg-node_1579_piv,float2x2( node_1579_cos, -node_1579_sin, node_1579_sin, node_1579_cos))+node_1579_piv);
                float node_7731 = 0.0;
                float node_4788 = 0.5;
                float node_4243 = node_4788;
                float2 node_6203 = float2((objScale.r*_MapXScale),(objScale.g*_MapYScale));
                float2 node_1138 = (node_4243-node_6203);
                float2 node_4491 = (0.05*((_parallax*1.0+-10.0) - 0.5)*mul(tangentTransform, viewDirection).xy + (node_1138 + ( (lerp( i.uv0, (i.uv0+lerp(float2(node_5432,node_5432),node_1579,_bowenScale)), _BoWen ) - node_7731) * ((node_4243+node_6203) - node_1138) ) / (1.0 - node_7731)));
                float4 _MainTex_var = tex2D(_MainTex,node_4491.rg);
                float2 node_9515 = node_4491.rg.rg;
                float2 node_9676 = float2((-1*node_9515.r),node_9515.g);
                float4 _node_5087_var = tex2D(_node_5087,node_9676);
                float3 finalColor = (lerp(_MainTex_var.rgb,_node_5087_var.rgb,isFrontFace)*i.vertexColor.rgb*_TintColor.rgb);
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                return fixed4(finalColor,(i.vertexColor.a*_TintColor.a*(_Mask_var.a*(pow(pow((1.0 - distance(float2(0.5,0.5),i.uv0)),_Edge),_Edge)+_alphaOffset)*_alphaScale)));
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
