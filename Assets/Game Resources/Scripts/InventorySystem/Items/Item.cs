using UnityEngine;

public class Item : MonoBehaviour
{
    [field: Header("Base Settings")]
    [field: SerializeField] public string ItemID { get; protected set; }
    [field: SerializeField] public string DisplayName { get; protected set; }
    [field: SerializeField] public Sprite ItemIcon { get; protected set; }
    [field: TextArea] [field: SerializeField] public string Description { get; protected set; }
    [field: SerializeField] public ItemRarity Rarity { get; protected set; } = ItemRarity.Usual;

    public virtual void SetRarity(ItemRarity rarity)
    {
        Rarity = rarity;
    }

    public virtual string[] GetCharacteristics()
    {
        return default;
    }
}
