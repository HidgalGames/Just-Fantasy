using UnityEngine;
using Zenject;

public class PlayerStartPoint : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;

    [Inject] private PlayerUnit _player;

    private void Start()
    {
        _player.transform.position = _startPoint.position;
    }
}
