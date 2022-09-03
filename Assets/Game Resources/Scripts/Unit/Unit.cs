using UnityEngine;

public class Unit : MonoBehaviour
{
    [field: Header("Base Unit Components")]
    [field: SerializeField] public Animator Animator { get; protected set; }
    [field: SerializeField] public HealthSystem HealthSystem { get; protected set; }

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
