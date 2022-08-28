using UnityEngine;

public class Item : MonoBehaviour
{
    [field: Header("Base Settings")]
    [field: SerializeField] public string ItemID { get; protected set; }
    [field: SerializeField] public string DisplayName { get; protected set; }
    [field: SerializeField] public Sprite ItemIcon { get; protected set; }
    [field: TextArea] [field: SerializeField] public string Description { get; protected set; }
}
