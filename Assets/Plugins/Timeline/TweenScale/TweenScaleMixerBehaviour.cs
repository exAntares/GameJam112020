﻿using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Playables;

namespace HalfBlind.Timeline {
    [Serializable]
    public class TweenScaleMixerBehaviour : TweenMixerBehaviour<TweenScaleBehaviour, Vector3> {
        private Vector3? _initialScale;

        public override void OnGraphStop(Playable playable) {
            base.OnGraphStop(playable);
            if (_targetTransform && _initialScale.HasValue) {
                _targetTransform.localScale = _initialScale.Value;
            }
        }

        public override void SetValue(Vector3 lastKnown) {
            _targetTransform.localScale = lastKnown;
        }

        public override Vector3 GetInitialValue() {
            if (_initialScale == null) {
                _initialScale = _targetTransform.localScale;
            }
            
            return _initialScale.Value;
        }

        public override Vector3 Evaluate(TweenScaleBehaviour tweenScaleBehaviour, Vector3 initial, float time, float duration) {
            var t = EasingUtils.Evaluate(tweenScaleBehaviour.Easing, tweenScaleBehaviour.CustomCurve, time, duration);
            var target = tweenScaleBehaviour.TargetScale;
            if (tweenScaleBehaviour.IsFrom) {
                target = initial;
                initial = tweenScaleBehaviour.TargetScale;
            }
            return initial * (1 - t) + target * t;
        }
    }
}
