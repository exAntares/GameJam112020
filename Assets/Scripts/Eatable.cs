using UnityEngine;
using GameJam;

public class Eatable : MonoBehaviour {
    public Collider MyCollider;
    public Rigidbody MyRigidBody;
    public float Size = 1.0f;
    public bool IsEatable { get; private set; } = true;
    
    public async void StartCooldown() {
        IsEatable = false;
        await new WaitForSeconds(2).ToAwaitable(this);
        IsEatable = true;
    }
}
