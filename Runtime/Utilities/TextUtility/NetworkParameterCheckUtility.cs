using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class NetworkParameterCheckUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckIsPort(this string input, out string errorMessage, out ushort port)
        {
            if (input.IsNullOrEmpty())
            {
                port = 0;
                errorMessage = "Port field is empty.";
                return false;
            }

            if (ushort.TryParse(input, out port) == false)
            {
                errorMessage = $"Port field : {input} is not a valid port number.";
                return false;
            }
            
            errorMessage = null;
            return true;
        }
    }
}