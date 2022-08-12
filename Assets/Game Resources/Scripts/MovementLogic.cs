using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _accelerationTime;
    [Space]
    [SerializeField] private float _rotationSpeed;

    private float _currentSpeed;
    private float _targetSpeed;

    private Vector3 _direction = Vector3.zero;
    private Quaternion _targetRotation;

    private const string IS_RUNNING_NAME = "IsRunning";
    private const string RUN_SPEED_NAME = "RunSpeed";

    public bool IsMoving { get; private set; }
    public bool IsUpdatingPosition { get; private set; } = true;

    public void MoveInDirection(Vector3 direction)
    {
        IsUpdatingPosition = true;

        IsMoving = direction.magnitude > 0;
        _animator.SetBool(IS_RUNNING_NAME, IsMoving);

        _direction = direction.normalized;
    }

    public void StopMovig()
    {
        IsMoving = false;
    }

    public void ForceStopMoving()
    {
        StopMovig();
        _currentSpeed = 0f;
        IsUpdatingPosition = false;

        if (_animator)
        {
            _animator.SetBool(IS_RUNNING_NAME, false);
        }
    }

    private void FixedUpdate()
    {
        if (!IsUpdatingPosition) return;

        _targetSpeed = IsMoving ? _moveSpeed : 0f;
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, 1f / _accelerationTime * Time.fixedDeltaTime);

        _root.position = Vector3.Lerp(_root.transform.position, _root.transform.position + _direction * _currentSpeed, _currentSpeed * Time.deltaTime);

        if (IsMoving)
        {
            _targetRotation = Quaternion.LookRotation(_direction, _root.up);
            _root.localRotation = Quaternion.Slerp(_root.localRotation, _targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }

        if (_animator)
        {
            _animator.SetFloat(RUN_SPEED_NAME, _currentSpeed / _moveSpeed);
        }
    }
}
