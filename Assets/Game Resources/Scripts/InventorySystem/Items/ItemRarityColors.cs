using UnityEngine;

//[CreateAssetMenu(menuName = "Items/Colors List", fileName = "Item Rarity Colors")]
public class ItemRarityColors : ScriptableObject
{
    [field: SerializeField] public Color[] RarityColors { get; private set; }
}
