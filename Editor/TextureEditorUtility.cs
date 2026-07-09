using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Editor
{
    public static class TextureEditorUtility
    {
        public static Texture GetAssetPreview(this Object obj)
        {
            return AssetPreview.GetAssetPreview(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Sprite> GetSpritesFromTexture(this Texture2D texture)
        {
            if (texture == null)
                return Array.Empty<Sprite>();

            string path = AssetDatabase.GetAssetPath(texture);

            // 从同一路径加载所有子资源
            var assets = AssetDatabase.LoadAllAssetsAtPath(path);

            // 过滤出 Sprite 类型
            return assets.OfType<Sprite>();
        }
    }
}