using UnityEngine;

public class Follower : MonoBehaviour {
    [SerializeField] private Transform _target;
    
    void Update() {
        transform.position = _target.position;
    }
}
