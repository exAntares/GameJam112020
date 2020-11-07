using Bolt;
using UnityEngine;

namespace HalfBlind.ScriptableVariables {
    public class GlobalFloatToBolt : MonoBehaviour {
        [SerializeField] private GlobalFloat _float;

        private void Awake() {
            _float.OnTValueChanged += OnChanged;
        }

        private void OnChanged(float value) {
            CustomEvent.Trigger(gameObject, _float.name, value);
        }

        private void OnDestroy() {
            _float.OnTValueChanged -= OnChanged;
        }
    }
}