using Cinemachine;
using UnityEngine;

public class Follower : MonoBehaviour {
    [SerializeField] private Transform _sphere;
    [SerializeField] private Transform _follower;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    [SerializeField] private float _cameraDistanceFactor = 5.0f;
    
    private Vector3 _lastPosition;
    private Cinemachine3rdPersonFollow _thirdPersonFollow;

    private void Awake() {
        CinemachineComponentBase componentBase = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is Cinemachine3rdPersonFollow) {
            _thirdPersonFollow = componentBase as Cinemachine3rdPersonFollow;
        }
    }

    private void Update() {
        _thirdPersonFollow.CameraDistance = _sphere.transform.localScale.x * _cameraDistanceFactor;
        var spherePosition = _sphere.position;
        var direction = spherePosition - _lastPosition;
        var targetPosition = spherePosition + direction;
        targetPosition.y = _follower.position.y;
        _follower.LookAt(targetPosition);
        _follower.position = spherePosition;
        _lastPosition = spherePosition;
    }
}
