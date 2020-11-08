using UnityEngine;
using GameJam;

public class Eatable : MonoBehaviour {
    public Collider MyCollider;
    public Rigidbody MyRigidBody;
    public float Size = 0.2f;
    public bool IsEatable { get; private set; } = true;

    public Vector3 GetSize => Vector3.one * (Size / 7); 
    private MeshRenderer _renderer;
    private EatScript _eatScript;

    private void Reset() {
        MyCollider = GetComponent<Collider>();
        MyRigidBody = GetComponent<Rigidbody>();
    }

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();
        _eatScript = FindObjectOfType<EatScript>();
    }

    private void Update() {
        if (_eatScript.CanEat(this) && _renderer) {
            if (MyCollider.enabled) {
                _renderer.material.SetFloat("Eatable", 1);
            }
            else {
                _renderer.material.SetFloat("Eatable", 0);
            }
        }
    }

    public async void StartCooldown() {
        IsEatable = false;
        await new WaitForSeconds(2).ToAwaitable(this);
        IsEatable = true;
    }
    
    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    void OnDrawGizmosSelected() {
        var component = GetComponent<Renderer>();
        if (component) {
            var componentBounds = component.bounds;
            Vector3 center = componentBounds.center;
            var boundsExtents = componentBounds.extents;
            Size = Mathf.Max(boundsExtents.x, boundsExtents.y, boundsExtents.z);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(center, Size);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, boundsExtents);
        }
    }
}
