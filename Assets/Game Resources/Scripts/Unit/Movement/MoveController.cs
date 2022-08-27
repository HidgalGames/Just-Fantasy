using System;
using UnityEngine;
using Zenject;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GravityController _gravityController;
    [SerializeField] private CollisionChecker _collisionChecker;
    [Space]
    [SerializeField] private Transform _root;
    [SerializeField] private Animator _animator;
    [Space]
    [Range(0, 1)][SerializeField] private float _speedSmoothing = 1f;
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
    [SerializeField] private float _resultingSpeed;
    
    private float _currentSpeed;
    private float _targetSpeed;
    private float _lerpTargetSpeed;
    private float _moveDelta;

    private DateTime _startRunTime;
    private float _runAccelerateCoef;

    private Vector3 _direction = Vector3.zero;
    
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private const string IS_MOVING_NAME = "IsMoving";
    private const string MOVE_SPEED_NAME = "MoveSpeed";

    public float CurrentMoveSpeed => _resultingSpeed;
    public float BaseMoveSpeed => _moveSpeed;

    public bool IsGrounded => _gravityController.IsGrounded;
    public bool IsRunning { get; private set; }
    public bool IsMoving { get; private set; }
    public bool IsUpdatingPosition { get; private set; } = true;

    private void Start()
    {
        SetRunState(false);
    }

    public void MoveInDirection(Vector3 direction)
    {
        IsUpdatingPosition = true;

        IsMoving = direction.magnitude > 0;
        _animator.SetBool(IS_MOVING_NAME, IsMoving);

        _direction = direction.normalized;
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

    private void FixedUpdate()
    {
        if (!IsUpdatingPosition) return;

        _lerpTargetSpeed = IsMoving ? _targetSpeed : 0f;

        _currentSpeed = Mathf.Lerp(_currentSpeed, _lerpTargetSpeed, 1f / _accelerationTime * Time.fixedDeltaTime);
        _resultingSpeed = _currentSpeed * _runAccelerateCoef;
        _moveDelta = _resultingSpeed * Time.fixedDeltaTime;

        if (_currentSpeed > 0)
        {
            if (IsRunning)
            {
                _runAccelerateCoef = _accelerationGraph.Evaluate(Mathf.Clamp01((float)(DateTime.Now - _startRunTime).TotalSeconds / _runAccelerationTime));
            }

            _resultingSpeed = Mathf.Lerp(_resultingSpeed, _currentSpeed * _runAccelerateCoef, 1f / _runAccelerationTime * Time.fixedDeltaTime);

            if (IsMoving)
            {
                _targetRotation = Quaternion.LookRotation(_direction, _root.up);
                _root.localRotation = Quaternion.Slerp(_root.localRotation, _targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }

            _direction = _collisionChecker.CalculateDirection(_root.position, _direction, ref _moveDelta, _maxStepHeight);
        }

        _targetPosition = _root.position + _direction * _moveDelta;
        _root.position = Vector3.Lerp(_root.position, _targetPosition, (1.1f -_speedSmoothing) * 100f * Time.fixedDeltaTime);

        if (_animator)
        {
            _animator.SetFloat(MOVE_SPEED_NAME, _resultingSpeed / _moveSpeed);
        }
    }
}
