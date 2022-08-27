using UnityEngine;

public class Blockable : MonoBehaviour
{
    [Header("Debug")]
    [ReadOnly] [SerializeField] private int _blockersCount = 0;

    public bool IsBlocked => _blockersCount > 0;

    public void SetBlocked(bool isBlocked)
    {
        _blockersCount += isBlocked ? 1 : -1;

        if (_blockersCount < 0) _blockersCount = 0;
    }
}
