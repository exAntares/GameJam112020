using Cinemachine;
using UnityEngine;

public class Follower : MonoBehaviour {
    [SerializeField] private Transform _sphere;
    [SerializeField] private Transform _follower;
    [SerializeField] private CinemachineFreeLook _virtualCamera;
    
    [SerializeField] private float _cameraDistanceFactor = 5.0f;
    
    private Vector3 _lastPosition;
    private Cinemachine3rdPersonFollow _thirdPersonFollow;

    private void Update() {
        var sphereScaleX = _sphere.transform.localScale.x;
        var radius = 10 + sphereScaleX * _cameraDistanceFactor;
        _virtualCamera.m_Orbits[1].m_Height = radius * 0.5f;
        _virtualCamera.m_Orbits[0].m_Height = radius;
        _virtualCamera.m_Orbits[0].m_Radius = radius;
        _virtualCamera.m_Orbits[1].m_Radius = radius;
        _virtualCamera.m_Orbits[2].m_Radius = radius;
        var spherePosition = _sphere.position;
        var direction = spherePosition - _lastPosition;
        var targetPosition = spherePosition + direction;
        targetPosition.y = _follower.position.y;
        _follower.LookAt(targetPosition);
        _follower.position = spherePosition;
        _lastPosition = spherePosition;
    }
}
