using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform _sphere;
    [SerializeField] private Transform _follower;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _strength = 1.0f;
    [SerializeField] private ForceMode _forceMode = ForceMode.Force;
    [SerializeField] private float _cameraDistanceFactor = 5.0f;
    
    private Vector3 _lastPosition;
    private Cinemachine3rdPersonFollow _thirdPersonFollow;
    private Transform _virtualCameraTransform;

    private void Awake() {
        CinemachineComponentBase componentBase = _virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        _virtualCameraTransform = _virtualCamera.transform;
        if (componentBase is Cinemachine3rdPersonFollow) {
            _thirdPersonFollow = componentBase as Cinemachine3rdPersonFollow;
        }
    }

    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical >= 0) {
            var right = _virtualCameraTransform.right * (horizontal * _strength * Time.deltaTime);
            var forward = _virtualCameraTransform.forward * (vertical * _strength * Time.deltaTime);
            _rigidbody.AddForce(forward + right, _forceMode);
        }

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
