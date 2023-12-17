Shader "Billboard/1Directional"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _SecondTex("SecondTexture", 2D) = "white" {}
    }
        SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "DisableBatching" = "true"
        }
        LOD 100
        Cull off

        //����
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog 

            #include "UnityCG.cginc"
            #include "DoomBilboard.cginc"//�������������� ���������� (������� ��� ���������)
            //�� ��� �������� ��������������� ����������
            struct appdata
            {
                float4 vertex : POSITION; //������ ��������� �� ������� float 
                float2 uv : TEXCOORD0; //������ ��������� �� ���� float
                float3 normal: NORMAL;
            };
    //vector to float
    struct v2f
    {
        float2 uv : TEXCOORD0;
        UNITY_FOG_COORDS(1)
        float4 vertex : SV_POSITION;
        float3 normal: NORMAL;
        float angle : TEXCOORD1;

    };

    sampler2D _MainTex;
    float4 _MainTex_ST;

    //https://docs.unity3d.com/Packages/com.unity.shadergraph@16.0/manual/Rotate-About-Axis-Node.html 
   


    v2f vert(appdata v)
    {
        v2f o;

        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        UNITY_TRANSFER_FOG(o,o.vertex);
        o.normal = v.normal;

        //https://docs.unity3d.com/Packages/com.unity.shadergraph@16.0/manual/Camera-Node.html 
        float3 cameraDir = -1 * mul(UNITY_MATRIX_M, transpose(mul(unity_WorldToObject, UNITY_MATRIX_I_V))[2].xyz);
        cameraDir.y = 0;
        float2 cameraDir2D = normalize(cameraDir.xz);



        float2 vectorForward2D = mul(UNITY_MATRIX_M, float4(0, 0, 1, 0)).xz;

        float angle = dot(vectorForward2D, cameraDir2D);

        float angleRad = acos(angle);

        float3 crossProduct = cross(float3(vectorForward2D.x, 0, vectorForward2D.y),
            float3(cameraDir.x, 0, cameraDir.y));

        if (dot(crossProduct, float3(0, 1, 0)) < 0) {
            angleRad = -angleRad;
        }

        float angleNormalized = angleRad / 3.1415;


        float finalAngle = (angleNormalized + 1) / 2;

        o.angle = finalAngle;

        float3 newVertex;

        Unity_RotateAboutAxis_Radians_float(v.vertex, float3(0, 1, 0), angleRad, newVertex);

        o.vertex = UnityObjectToClipPos(newVertex);

        return o;
    }

    fixed4 frag(v2f i) : SV_Target
    {



       
        fixed4 color = tex2D(_MainTex, i.uv); 

        if (color.a < 0.001)
            discard;

        return color;
    
    }
    ENDCG
}
    }
}
