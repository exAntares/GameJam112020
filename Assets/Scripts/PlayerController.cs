using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform _camera;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _strength = 1.0f;
    [SerializeField] private ForceMode _forceMode = ForceMode.Force;
    
    private Vector3 _lastPosition;

    private void Awake() {
        if (_camera == null) {
            _camera = FindObjectOfType<Camera>().transform;
        }
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
        if (horizontal != 0 || vertical > 0) {
            var right = _camera.right * (horizontal * _strength * Time.deltaTime);
            var forward = _camera.forward * (vertical * _strength * Time.deltaTime);
            _rigidbody.AddForce(forward + right, _forceMode);
            if (!_audioSource.isPlaying) {
                _audioSource.Play();
            }
        }

        if ((_rigidbody.velocity - Vector3.zero).sqrMagnitude <= 0.001f) {
            _audioSource.Pause();
        }
    }
}
