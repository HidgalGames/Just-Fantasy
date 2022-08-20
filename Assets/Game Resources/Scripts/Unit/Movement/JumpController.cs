using System;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private GravityController _gravity;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private Transform _root;
    [SerializeField] private Transform _unitUpperPont;
    [Space]
    [SerializeField] private float _minimalJumpheight = 0.2f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _jumpSpeed;
    [Space]
    [SerializeField] private LayerMask _collisionLayers;

    private RaycastHit _hit;
    private float _distanceToUpperCollision;
    private float _currentJumpHeight;
    private float _targetHeight;

    private Vector3 _targetPosition;

    private bool _isGrounded => _gravity.IsGrounded;

    private const string JUMP_TRIGGER_NAME = "Jump";

    public bool IsJumping { get; private set; }

    public event Action OnJump;

    public void Jump()
    {
        if (_isGrounded && !IsJumping)
        {
            if (!IsUpperCollisionBlocksJump())
            {
                _gravity.ApplyGravity = false;

                _currentJumpHeight = CalculateJumpHeight();
                _targetHeight = _root.position.y + _currentJumpHeight;

                IsJumping = true;

                if (_animator)
                {
                    _animator.SetTrigger(JUMP_TRIGGER_NAME);
                }

                _unit.StartActing();

                OnJump?.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsJumping) return;

        if(_root.position.y < _targetHeight)
        {
            _targetPosition = Vector3.Lerp(_root.position, _root.position + Vector3.up * _jumpHeight, _jumpSpeed * Time.fixedDeltaTime);
            _targetPosition.y = Mathf.Clamp(_targetPosition.y, _root.position.y, _targetHeight);

            _root.position = _targetPosition;
        }
        else
        {
            OnJumpEnd();
        }
    }

    private void OnJumpEnd()
    {
        IsJumping = false;
        _gravity.ApplyGravity = true;

        _unit.StopActing();
    }

    private bool IsUpperCollisionBlocksJump()
    {
        if (Physics.Raycast(_unitUpperPont.position, Vector3.up, out _hit, Mathf.Infinity, _collisionLayers))
        {
            _distanceToUpperCollision = Vector3.Distance(_unitUpperPont.position, _hit.point);

            if (_distanceToUpperCollision < _minimalJumpheight)
            {
                return true;
            }
        }
        else
        {
            _distanceToUpperCollision = float.MaxValue;
        }

        return false;
    }

    private float CalculateJumpHeight()
    {
        if (_distanceToUpperCollision < _jumpHeight)
        {
            return _distanceToUpperCollision;
        }
        else
        {
            return _jumpHeight;
        }
    }
}
