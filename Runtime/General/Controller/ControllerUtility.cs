using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class ControllerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TComponent SafeGetComponent<TComponent>(this IController controller)
        {
            if (controller == null)
            {
                return default;
            }
            
            return controller.GetComponent<TComponent>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SafeTryGetComponent<TController, TComponent>(this TController controller, out TComponent component)
            where TController : IController
        {
            if (controller == null)
            {
                component = default;
                return false;
            }
            
            return controller.TryGetComponent(out component);
        }
    }
}