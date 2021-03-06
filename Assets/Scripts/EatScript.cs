﻿using System.Collections.Generic;
using Bolt;
using HalfBlind.ScriptableVariables;
using UnityEngine;
using Random = UnityEngine.Random;

public class EatScript : MonoBehaviour {
    [SerializeField] private GlobalFloat _score;
    public Rigidbody MyRigidbody;
    public float EatProportion = 1.0f / 4.0f;
    public float LosePiecesAngularVelocityMagnitud = 8.0f;
    public Vector3 TotalAdded = Vector3.zero;

    public bool CanEat(Eatable eatable) {
        return eatable.IsEatable && eatable.Size <= transform.localScale.x * EatProportion;
    }
    
    private void OnCollisionEnter(Collision other) {
        var eatable = other.gameObject.GetComponent<Eatable>();
        if (eatable) {
            Debug.Log($"Hit {other.gameObject} with aV {MyRigidbody.angularVelocity.magnitude:F02} [{transform.localScale.x * EatProportion:F02}vs{eatable.Size}]");
            var myTransform = transform;
            if (CanEat(eatable)) {
                TotalAdded += eatable.GetSize;
                ChangeParentScale(myTransform, myTransform.localScale + eatable.GetSize);
                other.collider.enabled = false;
                if (eatable.MyRigidBody) {
                    other.rigidbody.isKinematic = true;
                }
                other.transform.SetParent(transform, true);
                _score.Value = myTransform.localScale.x;
                CustomEvent.Trigger(other.gameObject, "EatEvent");
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
            if (eatable != null && Random.Range(0.0f, 1.0f) <= 0.5f) {
                lost.Add(eatable);
            }
        }

        if (lost.Count > 0) {
            var lostSize = Vector3.zero;
            foreach (var eatable in lost) {
                lostSize += eatable.GetSize;
                eatable.transform.parent = null;
                eatable.MyCollider.enabled = true;
                if (eatable.MyRigidBody) {
                    eatable.MyRigidBody.isKinematic = false;
                }
                eatable.StartCooldown();
            }

            var myTransform = transform;
            TotalAdded -= lostSize;
            ChangeParentScale(myTransform, myTransform.localScale - lostSize);
            _score.Value = myTransform.localScale.x;
        }
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
