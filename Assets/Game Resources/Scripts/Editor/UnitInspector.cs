using UnityEngine;
using UnityEditor;
using System.Reflection;
using Unity.VisualScripting;

[CanEditMultipleObjects]
[CustomEditor(typeof(Unit), true)]
public class UnitInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Get Components From Children"))
        {
            var fields = target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var unitObject = (target as MonoBehaviour).gameObject;

            foreach(var field in fields)
            {
                if(field.PropertyType.IsSubclassOf(typeof(MonoBehaviour)))
                {
                    var valueAsObject = (Object)field.GetValue(target);
                    if (valueAsObject.IsUnityNull())
                    {
                        var component = unitObject.GetComponentInChildren(field.PropertyType);                        

                        if (component)
                        {
                            field.SetValue(target, component);
                        }
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        base.OnInspectorGUI();
    }
}
