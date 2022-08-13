using System;
using UnityEngine;
using Zenject;

public class MoveController : MonoBehaviour
{
    [Inject] CameraController _cameraController;

    [SerializeField] private GravityController _gravityController;
    [SerializeField] private CollisionChecker _collisionChecker;
    [Space]
    [SerializeField] private Transform _root;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _accelerationTime;
    [Space]
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _runAccelerationTime;
    [SerializeField] private AnimationCurve _accelerationGraph;
    [Space]
    [SerializeField] private float _maxStepHeight;
    [Space]
    [SerializeField] private float _rotationSpeed;

    [Space]
    [Header("Debug")]
    [SerializeField] private float _currentSpeed;
    private float _targetSpeed;
    private float _lerpTargetSpeed;

    private DateTime _startRunTime;
    private float _runAccelerateCoef;

    private Vector3 _direction = Vector3.zero;
    
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private const string IS_MOVING_NAME = "IsMoving";
    private const string MOVE_SPEED_NAME = "MoveSpeed";

    public CameraFovController FovController => _cameraController.FovController;

    public float CurrentMoveSpeed => _currentSpeed;
    public float BaseMoveSpeed => _moveSpeed;

    public bool IsRunning { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsUpdatingPosition { get; private set; } = true;

    public bool CanChangeDirection { get; set; } = true;

    private void Start()
    {
        _gravityController.OnStartFalling += OnStartFalling;
        _gravityController.OnGrounded += OnGrounded;

        SetRunState(false);

        FovController.SetupMoveController(this);
    }

    private void OnDestroy()
    {
        _gravityController.OnStartFalling -= OnStartFalling;
        _gravityController.OnGrounded -= OnGrounded;
    }

    public void MoveInDirection(Vector3 direction)
    {
        IsUpdatingPosition = true;

        IsMoving = direction.magnitude > 0;
        _animator.SetBool(IS_MOVING_NAME, IsMoving);

        if (CanChangeDirection)
        {
            _direction = direction.normalized;
        }
    }

    public void SetRunState(bool isRunning)
    {
        IsRunning = isRunning;

        _targetSpeed = IsRunning ? _runSpeed : _moveSpeed;

        if (IsRunning)
        {
            _startRunTime = DateTime.Now;
        }
        else
        {
            _runAccelerateCoef = 1f;
        }
    }

    public void StopMovig()
    {
        IsMoving = false;
        IsRunning = false;
    }

    public void ForceStopMoving()
    {
        StopMovig();
        _currentSpeed = 0f;
        IsUpdatingPosition = false;

        if (_animator)
        {
            _animator.SetBool(IS_MOVING_NAME, false);
        }
    }

    private void OnGrounded()
    {
        CanChangeDirection = true;
    }

    private void OnStartFalling()
    {
        CanChangeDirection = false;
    }

    private void FixedUpdate()
    {
        if (!IsUpdatingPosition) return;

        _lerpTargetSpeed = IsMoving ? _targetSpeed : 0f;

        _currentSpeed = Mathf.Lerp(_currentSpeed, _lerpTargetSpeed, 1f / _accelerationTime * Time.fixedDeltaTime);

        if(_currentSpeed > 0)
        {
            if (IsRunning)
            {
                _runAccelerateCoef = _accelerationGraph.Evaluate(Mathf.Clamp01((float)(DateTime.Now - _startRunTime).TotalSeconds / _runAccelerationTime));
            }

            //_targetPosition = Vector3.Lerp(_root.transform.position, _root.transform.position + _direction * _currentSpeed * _runAccelerateCoef, _currentSpeed * Time.deltaTime);
            _targetPosition = _collisionChecker.GetPositionByCollision(_root.position, _direction, _currentSpeed * _runAccelerateCoef, _maxStepHeight);
            _root.position = Vector3.Lerp(_root.position, _targetPosition, _currentSpeed * Time.fixedDeltaTime);

            if (IsMoving)
            {
                _targetRotation = Quaternion.LookRotation(_direction, _root.up);
                _root.localRotation = Quaternion.Slerp(_root.localRotation, _targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }
        }

        if (_animator)
        {
            _animator.SetFloat(MOVE_SPEED_NAME, _currentSpeed / _moveSpeed);
        }
    }
}
