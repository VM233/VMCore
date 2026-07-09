using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Editor
{
    public static class PrefabEditorUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SaveComponentAsPrefab(this Type componentType, string assetPath)
        {
            var gameObject = new GameObject();
            var component = gameObject.AddComponent(componentType);

            if (component == null)
            {
                return;
            }
            
            PrefabUtility.SaveAsPrefabAsset(gameObject, assetPath);
            Object.DestroyImmediate(gameObject);
        }
    }
}