using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _collider;
    [Space]
    [SerializeField] private LayerMask _layerMask;
    [Space]
    [Range(0.01f, 1f)][SerializeField] private float _speedDecreaseOnCollision = 0.5f;

    public Vector3 CalculateDirection(Vector3 position, Vector3 direction, ref float moveDelta, float maxCollisionHeight)
    {
        if (Physics.Raycast(position + maxCollisionHeight * Vector3.up, direction, out var lowerHit, moveDelta + _collider.radius, _layerMask, QueryTriggerInteraction.Ignore))
        {
            var collider = lowerHit.collider;

            if (Physics.ComputePenetration(_collider, position, Quaternion.identity,
                collider, collider.transform.position, collider.transform.rotation, out var dir, out var dist))
            {
                direction = (dir + transform.forward).normalized;
                moveDelta = _speedDecreaseOnCollision * moveDelta;
            }
        }

        return direction;
    }
}
