using UnityEngine;
using UnityEngine.EventSystems;

public class RotateRenderedObject : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Transform _rotateable;
    [Min(0)] [SerializeField] private float _sensitivity = 1f;
    [SerializeField] private bool _isInversed = true;
    [SerializeField] private bool _resetOnEnable = true;

    private int _coef;
    private Quaternion _baseRotation;

    private void Awake()
    {
        _coef = _isInversed ? -1 : 1;
        _baseRotation = _rotateable.localRotation;
    }

    private void OnEnable()
    {
        if (_resetOnEnable)
        {
            ResetRotation();
        }
    }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData)
    {
        _rotateable.Rotate(_rotateable.up, eventData.delta.x * _sensitivity * _coef);
    }

    public void ResetRotation()
    {
        _rotateable.localRotation = _baseRotation;
    }
}
