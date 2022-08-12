using UnityEngine;

public class ParentChanger : MonoBehaviour
{
    [SerializeField] private Transform _targetParent;
    [SerializeField] private Vector3 _localPosition;
    [SerializeField] private Vector3 _localRotation;

    private void Start()
    {
        if (_targetParent)
        {
            transform.parent = _targetParent;
            transform.localPosition = _localPosition;
            transform.localRotation = Quaternion.Euler(_localRotation);
        }
    }
}
