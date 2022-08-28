using System;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ItemRarityColors))]
public class ItemColorsInspector : Editor
{
    private int _rarityCount;

    private void OnEnable()
    {
        _rarityCount = Enum.GetNames(typeof(ItemRarity)).Length;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Rarity Types:", EditorStyles.boldLabel);

        for (int i = 0; i < _rarityCount; i++)
        {
            EditorGUILayout.LabelField($"{i + 1}. {(ItemRarity)i}");
        }

        base.OnInspectorGUI();
    }
}
