using UnityEngine;

namespace VMFramework.Core
{
    public class NonRotationControl : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}