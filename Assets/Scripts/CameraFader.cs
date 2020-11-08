using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraFader : MonoBehaviour {
    private EatScript _eatScript;
    private readonly Dictionary<Material, float> _disolveValueByMaterial = new Dictionary<Material, float>();
    private static readonly int Dissolve = Shader.PropertyToID("Dissolve");
    [SerializeField] private float _disolveDuration = 2.0f;
    
    private void Awake() {
        _eatScript = FindObjectOfType<EatScript>();
    }

    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _eatScript.transform.position - transform.position, out hit, float.MaxValue)) {
            var meshRenderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer) {
                if (!_disolveValueByMaterial.TryGetValue(meshRenderer.material, out var value)) {
                    _disolveValueByMaterial[meshRenderer.material] = Time.realtimeSinceStartup + _disolveDuration;
                }
                else if(GetRemainingTime(value) <= 0) {
                    _disolveValueByMaterial[meshRenderer.material] = Time.realtimeSinceStartup + 0.5f;
                }
            }
        }
        
        foreach (var dissolveByMaterial in _disolveValueByMaterial) {
            var remainingTime = Mathf.Max(0, GetRemainingTime(dissolveByMaterial.Value));
            if (remainingTime > 0) {
                dissolveByMaterial.Key.SetFloat(Dissolve, 1 - (remainingTime / _disolveDuration));
            }
            else {
                dissolveByMaterial.Key.SetFloat(Dissolve, 0);
            }
        }
    }

    private float GetRemainingTime(float realtimeSinceStartupEnd) => realtimeSinceStartupEnd - Time.realtimeSinceStartup;

    void OnDrawGizmos() {
        if (_eatScript) {
            Gizmos.color = Color.red;
            var position = transform.position;
            Gizmos.DrawRay(position, _eatScript.transform.position - position);            
        }
    }
}
