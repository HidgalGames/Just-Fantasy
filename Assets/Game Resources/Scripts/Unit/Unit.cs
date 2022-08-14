using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;

    protected int _actingCount = 0;

    public bool IsActing => _actingCount > 0;

    public HealthSystem HealthSystem => _healthSystem;

    public void StartActing()
    {
        _actingCount++;
    }

    public void StopActing()
    {
        _actingCount--;
    }
}
