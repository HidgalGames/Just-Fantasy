using UnityEngine;
using UnityEditor;
using Extensions.ComponentsFinder;

[CanEditMultipleObjects]
[CustomEditor(typeof(Unit), true)]
public class UnitInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Fill Fields"))
        {
            ComponentsFinder.GetComponentsForAllFields(target, serializedObject);
        }

        base.OnInspectorGUI();
    }
}
