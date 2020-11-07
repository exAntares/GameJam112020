using UnityEngine;
using GameJam;

public class Eatable : MonoBehaviour {
    public Collider MyCollider;
    public Rigidbody MyRigidBody;
    public float Size = 0.2f;
    public bool IsEatable { get; private set; } = true;

    private void Reset() {
        MyCollider = GetComponent<Collider>();
        MyRigidBody = GetComponent<Rigidbody>();
    }

    public async void StartCooldown() {
        IsEatable = false;
        await new WaitForSeconds(2).ToAwaitable(this);
        IsEatable = true;
    }
    
    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    void OnDrawGizmosSelected() {
        // A sphere that fully encloses the bounding box.
        var component = GetComponent<Renderer>();
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
