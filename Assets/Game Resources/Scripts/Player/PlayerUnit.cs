using UnityEngine;

public class PlayerUnit : Unit
{
    [field: Space]
    [field: Header("Player Unit Components")]
    [field: SerializeField] public PlayerInputHandler InputHandler { get; private set; }
    [field: SerializeField] public MoveController MoveController { get; private set; }
}
