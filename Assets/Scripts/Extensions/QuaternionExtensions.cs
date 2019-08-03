using UnityEngine;

namespace Extensions
{
    public static class QuaternionExtensions
    {
        public static Quaternion With(this Quaternion original, float? x = null, float? y = null, float? z = null)
        {
            return Quaternion.Euler(x ?? original.x, y ?? original.y, z ?? original.z);
        }
    }
}