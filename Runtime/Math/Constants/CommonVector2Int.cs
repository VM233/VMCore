using UnityEngine;

namespace VMFramework.Core
{
    public static class CommonVector2Int
    {
        public static readonly Vector2Int maxValue = new(int.MaxValue, int.MaxValue);
        
        public static readonly Vector2Int minValue = new(int.MinValue, int.MinValue);
        
        /// <summary>
        /// Shorthand for Vector2Int(2, 2)
        /// </summary>
        public static readonly Vector2Int two = new(2, 2);
        
        /// <summary>
        /// Shorthand for Vector2Int(-1, 1)
        /// </summary>
        public static readonly Vector2Int leftUp = new(-1, 1);
        
        /// <summary>
        /// Shorthand for Vector2Int(1, 1)
        /// </summary>
        public static readonly Vector2Int rightUp = new(1, 1);
        
        /// <summary>
        /// Shorthand for Vector2Int(-1, -1)
        /// </summary>
        public static readonly Vector2Int leftDown = new(-1, -1);
        
        /// <summary>
        /// Shorthand for Vector2Int(1, -1)
        /// </summary>
        public static readonly Vector2Int rightDown = new(1, -1);
    }
}