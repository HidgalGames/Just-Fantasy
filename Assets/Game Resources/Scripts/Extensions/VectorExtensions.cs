using UnityEngine;

namespace Extensions.Vectors
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3XZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Vector3 XZ(this Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }
    }
}

