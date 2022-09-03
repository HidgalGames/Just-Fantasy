using UnityEngine;

namespace Extensions.Objects
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this Object obj)
        {
            return !obj;
        }
    }
}