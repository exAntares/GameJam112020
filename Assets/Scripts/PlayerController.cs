using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _strength = 1.0f;
    [SerializeField] private ForceMode _forceMode = ForceMode.Force;
    
    private Vector3 _lastPosition;
    private Transform _virtualCameraTransform;

    private void Awake() {
        _virtualCameraTransform = _virtualCamera.transform;
    }

    private void OnEnable() {
        _rigidbody.useGravity = true;
    }
    
    private void OnDisable() {
        _rigidbody.useGravity = false;
    }

    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical >= 0) {
            var right = _virtualCameraTransform.right * (horizontal * _strength * Time.deltaTime);
            var forward = _virtualCameraTransform.forward * (vertical * _strength * Time.deltaTime);
            _rigidbody.AddForce(forward + right, _forceMode);
        }
    }
}
