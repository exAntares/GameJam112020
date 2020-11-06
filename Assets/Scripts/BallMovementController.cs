using UnityEngine;

public class BallMovementController : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _strength = 1.0f;
    
    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var x = horizontal * _strength * Time.deltaTime;
        var z = vertical * _strength * Time.deltaTime;
        _rigidbody.AddForce(x, 0, z);
    }
}
