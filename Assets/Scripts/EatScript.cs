using System.Collections.Generic;
using UnityEngine;

public class EatScript : MonoBehaviour {
    public Rigidbody MyRigidbody;
    public float EatProportion = 1.0f / 4.0f;
    public float LosePiecesAngularVelocityMagnitud = 8.0f;
    
    private void OnCollisionEnter(Collision other) {
        var eatable = other.gameObject.GetComponent<Eatable>();
        if (eatable) {
            Debug.Log($"Hit {other.gameObject} with angularVelocity {MyRigidbody.angularVelocity.magnitude} {transform.localScale.x * EatProportion} vs {eatable.Size} ");
            var myTransform = transform;
            var canEat = eatable.IsEatable && eatable.Size <= myTransform.localScale.x * EatProportion;
            if (canEat) {
                ChangeParentScale(myTransform, myTransform.localScale + Vector3.one * eatable.Size);
                other.collider.enabled = false;
                other.rigidbody.isKinematic = true;
                other.transform.SetParent(transform, true);                
            }
            else if(MyRigidbody.angularVelocity.magnitude >= LosePiecesAngularVelocityMagnitud) {
                Debug.Log($"Lost Pieces!");
                LosePieces();
            }
        }
    }

    private void LosePieces() {
        var lost = new List<Eatable>();
        foreach (Transform child in transform) {
            var eatable = child.GetComponent<Eatable>();
            if (eatable != null && Random.Range(0.0f,1.0f) <= 0.5f) {
                lost.Add(eatable);
            }
        }

        var lostSize = Vector3.zero;
        foreach (var eatable in lost) {
            lostSize += Vector3.one * eatable.Size;
            eatable.transform.parent = null;
            eatable.MyCollider.enabled = true;
            eatable.MyRigidBody.isKinematic = false;
            eatable.StartCooldown();
        }

        var myTransform = transform;
        ChangeParentScale(myTransform, myTransform.localScale - lostSize);
    }

    private void ChangeParentScale(Transform parent, Vector3 scale) {
        var children = new List<Transform>();
        foreach (Transform child in parent) {
            if (child.GetComponent<Eatable>() != null) {
                child.parent = null;
                children.Add(child);                
            }
        }

        parent.localScale = scale;
        foreach (Transform child in children) {
            child.SetParent(parent, true);
        }
    }
}
