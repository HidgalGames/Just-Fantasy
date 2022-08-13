using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int _actingCount = 0;

    public bool IsActing => _actingCount > 0;

    public void StartActing()
    {
        _actingCount++;
    }

    public void StopActing()
    {
        _actingCount--;
    }
}
