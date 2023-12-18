#ifndef DOOM_BILLBOARD
#define DOOM_BILLBOARD

void Unity_RotateAboutAxis_Radians_float(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;

                Axis = normalize(Axis);
                float3x3 rot_mat =
                {   one_minus_c * Axis.x * Axis.x + c, one_minus_c * Axis.x * Axis.y - Axis.z * s, one_minus_c * Axis.z * Axis.x + Axis.y * s,
                    one_minus_c * Axis.x * Axis.y + Axis.z * s, one_minus_c * Axis.y * Axis.y + c, one_minus_c * Axis.y * Axis.z - Axis.x * s,
                    one_minus_c * Axis.z * Axis.x - Axis.y * s, one_minus_c * Axis.y * Axis.z + Axis.x * s, one_minus_c * Axis.z * Axis.z + c
                };
                Out = mul(rot_mat,  In);
            }

            // https://docs.unity3d.com/Packages/com.unity.shadergraph@16.0/manual/Flipbook-Node.html 
            void Unity_Flipbook_float(float2 UV, float Width, float Height, float Tile, float2 Invert, out float2 Out)
            {
                Tile = floor(fmod(Tile + float(0.00001), Width*Height));
                float2 tileCount = float2(1.0, 1.0) / float2(Width, Height);
                float base = floor((Tile + float(0.5)) * tileCount.x);
                float tileX = (Tile - Width * base);
                float tileY = (Invert.y * Height - (base + Invert.y * 1));
                Out = (UV + float2(tileX, tileY)) * tileCount;
            }

#endif