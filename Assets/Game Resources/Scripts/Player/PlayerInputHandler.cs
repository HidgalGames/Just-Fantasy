using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private MovementLogic _moveLogic;

    private Vector3 _moveDirection;
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        _moveDirection = Input.GetAxisRaw("Horizontal") * _cameraTransform.right;
        _moveDirection += Input.GetAxisRaw("Vertical") * _cameraTransform.forward;
        _moveDirection.y = 0f;

        _moveLogic.MoveInDirection(_moveDirection);
    }
}
