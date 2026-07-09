using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public class CapsuleCollider2DParameterCopy : MonoBehaviour
    {
        public CapsuleCollider2D source;

        public List<CapsuleCollider2D> targets = new();

        protected virtual void Update()
        {
            foreach (var target in targets)
            {
                target.offset = source.offset;
                target.size = source.size;
                target.direction = source.direction;
            }
        }
    }
}