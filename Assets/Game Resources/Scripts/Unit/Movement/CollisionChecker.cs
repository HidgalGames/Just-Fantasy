using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private Vector3 _collisionSize = Vector3.one;
    [SerializeField] private Vector3 _centerOffset;
    [Space]
    [SerializeField] private LayerMask _layerMask;

    public Vector3 GetPositionByCollision(Vector3 position, Vector3 direction, float moveDelta, float maxCollisionHeight)
    {
        var delta = moveDelta;
        Vector3 newDirection;
        Vector3 newPosition = position + direction * moveDelta;

        Debug.DrawLine(position + maxCollisionHeight * Vector3.up, position + maxCollisionHeight * Vector3.up + direction * delta, Color.red);

        if (Physics.Raycast(position + maxCollisionHeight * Vector3.up, direction, out var lowerHit, delta, _layerMask))
        {
            var boxCenterPosition = position + Vector3.up * (_collisionSize.y / 2f) + _centerOffset;

            newDirection = (lowerHit.point - position).normalized;
            newPosition = position + newDirection * moveDelta;

            if (Physics.BoxCast(boxCenterPosition, _collisionSize / 2f, direction, out var boxHit, Quaternion.identity, delta, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (boxHit.point.y - position.y > maxCollisionHeight)
                {
                    newPosition = boxHit.point + boxHit.normal * _collisionSize.x;
                    newPosition.y = position.y;

                    Debug.DrawLine(position + maxCollisionHeight * Vector3.up, position + maxCollisionHeight * Vector3.up + newDirection * delta, Color.blue);
                }
            }
        }

        return newPosition;
    }

    /*
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position + Vector3.up * (_collisionSize.y / 2f) + _centerOffset, _collisionSize);
    }

#endif
    */
}
