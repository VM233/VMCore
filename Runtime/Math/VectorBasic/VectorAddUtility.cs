using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class VectorAddUtility
    {
        #region Add 1

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            vector.x += x;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            vector.y += y;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AddX(this Vector2Int vector, int x)
        {
            vector.x += x;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AddY(this Vector2Int vector, int y)
        {
            vector.y += y;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddX(this Vector3 vector, float x)
        {
            vector.x += x;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            vector.z += z;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddX(this Vector3Int vector, int x)
        {
            vector.x += x;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddY(this Vector3Int vector, int y)
        {
            vector.y += y;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddZ(this Vector3Int vector, int z)
        {
            vector.z += z;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddX(this Vector4 vector, float x)
        {
            vector.x += x;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddY(this Vector4 vector, float y)
        {
            vector.y += y;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddZ(this Vector4 vector, float z)
        {
            vector.z += z;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddW(this Vector4 vector, float w)
        {
            vector.w += w;
            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddRed(this Color color, float red)
        {
            color.r += red;
            return color;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddGreen(this Color color, float green)
        {
            color.g += green;
            return color;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddBlue(this Color color, float blue)
        {
            color.b += blue;
            return color;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddAlpha(this Color color, float alpha)
        {
            color.a += alpha;
            return color;
        }

        #endregion

        #region Add 2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddXY(this Vector3 vector, Vector2 xy)
        {
            return new(vector.x + xy.x, vector.y + xy.y, vector.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddXZ(this Vector3 vector, Vector2 xz)
        {
            return new(vector.x + xz.x, vector.y, vector.z + xz.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddYZ(this Vector3 vector, Vector2 yz)
        {
            return new(vector.x, vector.y + yz.x, vector.z + yz.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddXY(this Vector3Int vector, Vector2Int xy)
        {
            return new(vector.x + xy.x, vector.y + xy.y, vector.z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddXZ(this Vector3Int vector, Vector2Int xz)
        {
            return new(vector.x + xz.x, vector.y, vector.z + xz.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddYZ(this Vector3Int vector, Vector2Int yz)
        {
            return new(vector.x, vector.y + yz.x, vector.z + yz.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddXY(this Vector4 vector, Vector2 xy)
        {
            return new(vector.x + xy.x, vector.y + xy.y, vector.z, vector.w);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddXZ(this Vector4 vector, Vector2 xz)
        {
            return new(vector.x + xz.x, vector.y, vector.z + xz.y, vector.w);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddYZ(this Vector4 vector, Vector2 yz)
        {
            return new(vector.x, vector.y + yz.x, vector.z + yz.y, vector.w);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddZW(this Vector4 vector, Vector2 zw)
        {
            return new(vector.x, vector.y, vector.z, vector.w + zw.x);
        }

        #endregion
    }
}