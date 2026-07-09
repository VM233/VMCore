using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static class AssetDeleteUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteAsset(this Object obj)
        {
            if (obj.IsAsset() == false)
            {
                Debug.LogWarning($"{obj} is not an asset, cannot delete it.");
                return;
            }
            
            AssetDatabase.DeleteAsset(obj.GetAssetPath());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteAssetWithDialog(this Object obj)
        {
            if (obj == null)
            {
                Debug.LogWarning($"The object is null, cannot delete the asset.");
                return;
            }

            if (obj.IsAsset() == false)
            {
                Debug.LogWarning($"{obj} is not an asset, cannot delete it.");
                return;
            }
            
            if ($"Are you sure you want to delete the asset : {obj.name} ?".DisplayWarningDialog())
            {
                AssetDatabase.DeleteAsset(obj.GetAssetPath());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteAssetsWithDialog(this IEnumerable<Object> objects)
        {
            if (objects == null)
            {
                Debug.LogWarning($"The objects are null, cannot delete the assets.");
                return;
            }
            
            var assets = objects.Where(x => x.IsAsset()).ToList();
            if (assets.Count == 0)
            {
                Debug.LogWarning($"No assets found in the list, cannot delete any assets.");
                return;
            }

            var names = assets.Select(a => a.name).ToFormattedString();

            if ($"Are you sure you want to delete the assets : {names}?".DisplayWarningDialog())
            {
                foreach (var asset in assets)
                {
                    AssetDatabase.DeleteAsset(asset.GetAssetPath());
                }
            }
        }
    }
}