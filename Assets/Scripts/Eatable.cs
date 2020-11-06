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
}
