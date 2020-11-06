using System.Threading.Tasks;
using UnityEngine;

public class Eatable : MonoBehaviour {
    public Collider MyCollider;
    public Rigidbody MyRigidBody;
    public float Size = 1.0f;
    public bool IsEatable { get; private set; } = true;
    
    public async void StartCooldown() {
        IsEatable = false;
        await Task.Delay(2000);
        IsEatable = true;
    }
}
