using UnityEngine;

public class WeaponItem : Item
{
    [field: Header("Weapon Settings")]
    [field: SerializeField] public Vector2Int Damage { get; private set; }

    public override string[] GetCharacteristics()
    {
        string[] result = new string[1];
        result[0] = $"Damage: {Damage.x}-{Damage.y}";

        return result;
    }
}
