using Extensions.Objects;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Extensions.ComponentsFinder
{
    public class ComponentsFinder
    {
#if UNITY_EDITOR
        public static void GetComponentsForAllFields(Object target, SerializedObject serializedObject = null)
        {
            if (target.IsNull()) return;

            var fields = target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var unitObject = (target as MonoBehaviour).gameObject;

            foreach (var field in fields)
            {
                if (!field.CanWrite) continue;

                if (field.PropertyType.IsSubclassOf(typeof(Component)))
                {
                    var valueAsObject = field.GetValue(target) as Object;
                    if (valueAsObject.IsNull())
                    {
                        var component = unitObject.GetComponentInChildren(field.PropertyType);

                        if (component)
                        {
                            field.SetValue(target, component);
                        }
                    }
                }
            }

            if (serializedObject != null)
            {
                serializedObject.ApplyModifiedProperties();
            }

            EditorUtility.SetDirty(target);
        }
#endif
    }
}
