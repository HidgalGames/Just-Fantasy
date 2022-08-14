using UnityEngine;
using Zenject;

public class CameraDistanceController : MonoBehaviour
{
    [Range(1f, 3f)][SerializeField] private float _maxDistanceInPercents = 1.5f;
    [Range(0.1f, 1f)][SerializeField] private float _minDistanceInPercents = 0.8f;
    [Space]
    [SerializeField] private float _scrollScensitivity = 0.2f;
    [SerializeField] private float _changeDistanceSpeed = 0.5f;

    [Inject] CameraController _cameraController;

    private float _targetCoef = 1f;
    private float _currentDistanceCoef = 1f;

    private float[] _baseValues;

    private void Awake()
    {
        _baseValues = new float[_cameraController.ThirdPersonCamera.m_Orbits.Length];

        for (int i = 0; i < _cameraController.ThirdPersonCamera.m_Orbits.Length; i++)
        {
            _baseValues[i] = _cameraController.ThirdPersonCamera.m_Orbits[i].m_Radius;
        }
    }

    private void Update()
    {
        _currentDistanceCoef -= Input.mouseScrollDelta.y * _scrollScensitivity;
        _currentDistanceCoef = Mathf.Clamp(_currentDistanceCoef, _minDistanceInPercents, _maxDistanceInPercents);

        for (int i = 0; i < _cameraController.ThirdPersonCamera.m_Orbits.Length; i++)
        {
            _cameraController.ThirdPersonCamera.m_Orbits[i].m_Radius = Mathf.Lerp(_cameraController.ThirdPersonCamera.m_Orbits[i].m_Radius, _baseValues[i] * _currentDistanceCoef, _changeDistanceSpeed * Time.smoothDeltaTime);
        }
    }
}
