using UnityEngine;

public class EatScript : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        var eatable = other.gameObject.GetComponent<Eatable>();
        if (eatable) {
            transform.localScale += new Vector3(eatable.Size, eatable.Size, eatable.Size);
            Destroy(other.gameObject);
        }
    }
}
