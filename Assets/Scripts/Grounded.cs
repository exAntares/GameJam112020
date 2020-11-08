using UnityEngine;

public class Grounded : MonoBehaviour {
    [SerializeField] private Transform _bug;
    
    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, float.MaxValue)) {
            _bug.position = hit.point;
        }
    }
}
