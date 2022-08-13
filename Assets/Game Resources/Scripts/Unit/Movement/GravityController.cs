using System;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private float _gravityForce = 9.8f;
    [Space]
    [SerializeField] private float _collisionRadius;
    [Space]
    [SerializeField] private LayerMask _collisionLayers;
    [Space]
    [Header("Debug")]
    [SerializeField] private bool _isGrounded;

    private Vector3 _targetPosition;

    private RaycastHit _hit;
    private float _fallSpeed;

    private bool _isStartFallingInvoked;
    private bool _isOnGroundedInvoked;

    public bool IsGrounded => _isGrounded;

    public bool ApplyGravity { get; set; } = true;

    public event Action OnGrounded;
    public event Action OnStartFalling;

    private void FixedUpdate()
    {
        if (!ApplyGravity) return;        

        if(Physics.SphereCast(_root.position + Vector3.up * (_collisionRadius + 2f), _collisionRadius, Vector3.down, out _hit, Mathf.Infinity, _collisionLayers))
        {
            _isGrounded = Vector3.Distance(_root.position, _hit.point) <= _collisionRadius;
        }
        else
        {
            _isGrounded = false;
        }

        if (!IsGrounded)
        {
            _fallSpeed += _gravityForce * Time.fixedDeltaTime;

            _root.position = Vector3.MoveTowards(_root.position, _hit.point, _fallSpeed);

            InvokeStartFalling();
        }
        else
        {
            _fallSpeed = 0f;

            _targetPosition = _root.position;
            _targetPosition.y = _hit.point.y;
            _root.position = _targetPosition;

            InvokeOnGrouneded();
        }
    }

    private void InvokeStartFalling()
    {
        if (!_isStartFallingInvoked)
        {
            _isOnGroundedInvoked = false;
            _isStartFallingInvoked = true;

            OnStartFalling?.Invoke();
        }
    }

    private void InvokeOnGrouneded()
    {
        if (!_isOnGroundedInvoked)
        {
            _isStartFallingInvoked = false;
            _isOnGroundedInvoked = true;

            OnGrounded?.Invoke();
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(_root.position, _collisionRadius);
    }

#endif
}
