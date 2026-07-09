using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace VMFramework.Core
{
    public static class StyleSheetSetUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveIfNotNull(this VisualElementStyleSheetSet set, StyleSheet sheet)
        {
            if (sheet != null)
            {
                set.Remove(sheet);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddIfNotNull(this VisualElementStyleSheetSet set, StyleSheet sheet)
        {
            if (sheet != null)
            {
                set.Add(sheet);
            }
        }
    }
}