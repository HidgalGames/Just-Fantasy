using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Blockable _blockable;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private JumpController _jumpController;

    private Vector3 _moveDirection;
    private Transform _cameraTransform;

    public bool IsBlocked => _blockable.IsBlocked;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if (IsBlocked) return;

        _moveDirection = Input.GetAxisRaw("Horizontal") * _cameraTransform.right;
        _moveDirection += Input.GetAxisRaw("Vertical") * _cameraTransform.forward;
        _moveDirection.y = 0f;

        _moveController.MoveInDirection(_moveDirection);
    }

    private void Update()
    {
        if (IsBlocked) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveController.SetRunState(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveController.SetRunState(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpController.Jump();
        }
    }

    public void SetBlocked(bool isBlocked)
    {
        _blockable.SetBlocked(isBlocked);

        if (isBlocked)
        {
            _moveController.ForceStopMoving();
        }
    }
}
