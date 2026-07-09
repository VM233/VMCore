using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ColliderMeshCache
    {
        private static readonly Dictionary<uint, Mesh> colliderMeshMap = new();
        
        public static Mesh GetMesh(Collider2D collider)
        {
            uint hash = collider.GetShapeHash();
            if (colliderMeshMap.TryGetValue(hash, out var mesh) == false)
            {
                mesh = collider.CreateMesh(false, false);
                colliderMeshMap.Add(hash, mesh);
            }
            
            return mesh;
        }
    }
}