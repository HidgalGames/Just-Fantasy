using UnityEngine;

public class PlayerUnit : Unit
{
    [SerializeField] private MoveController _moveController;

    public MoveController MoveController => _moveController;
}
