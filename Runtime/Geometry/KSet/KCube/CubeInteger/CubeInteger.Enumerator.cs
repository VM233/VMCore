using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct CubeInteger
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Vector3Int> IEnumerable<Vector3Int>.GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public struct Enumerator : IEnumerator<Vector3Int>
        {
            private readonly CubeInteger cube;
            private readonly bool isValid;
            private int x, y, z;

            public Enumerator(CubeInteger cube)
            {
                if (cube.min.x > cube.max.x || cube.min.y > cube.max.y || cube.min.z > cube.max.z)
                {
                    isValid = false;
                    x = y = z = 0;
                }
                else
                {
                    isValid = true;

                    if (cube.inverseX)
                    {
                        (cube.min.x, cube.max.x) = (-cube.max.x, -cube.min.x);
                    }
                    
                    if (cube.inverseY)
                    {
                        (cube.min.y, cube.max.y) = (-cube.max.y, -cube.min.y);
                    }
                    
                    if (cube.inverseZ)
                    {
                        (cube.min.z, cube.max.z) = (-cube.max.z, -cube.min.z);
                    }
                    
                    x = cube.min.x;
                    y = cube.min.y;
                    z = cube.min.z - 1;
                }
                
                this.cube = cube;
            }

            public Vector3Int Current
            {
                get
                {
                    var currentX = cube.inverseX? -x : x;
                    var currentY = cube.inverseY? -y : y;
                    var currentZ = cube.inverseZ? -z : z;
                    return new(currentX, currentY, currentZ);
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (isValid == false)
                {
                    return false;
                }

                z++;

                if (z > cube.max.z)
                {
                    z = cube.min.z;
                    y++;

                    if (y > cube.max.y)
                    {
                        y = cube.min.y;
                        x++;

                        if (x > cube.max.x)
                        {
                            return false;
                        }
                    }
                }
                
                return true;
            }

            public void Reset()
            {
                x = cube.min.x;
                y = cube.min.y;
                z = cube.min.z - 1;
            }

            public void Dispose() { }
        }
    }
}