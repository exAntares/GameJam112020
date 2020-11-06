using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameJam {
    public class CoroutineAwaiter : INotifyCompletion {
        private bool _isDone;
        private Exception _exception;
        private Action _continuation;

        public CoroutineAwaiter(YieldInstruction instruction, MonoBehaviour coroutineExecuter) {
            coroutineExecuter.StartCoroutine(CoroutineWithCallback(instruction, () => Complete(null)));
        }

        public CoroutineAwaiter(CustomYieldInstruction instruction, MonoBehaviour coroutineExecuter) {
            coroutineExecuter.StartCoroutine(CoroutineWithCallback(instruction, () => Complete(null)));
        }

        public IEnumerator CoroutineWithCallback(YieldInstruction instruction, Action callback) {
            yield return instruction;
            callback?.Invoke();
        }

        public IEnumerator CoroutineWithCallback(CustomYieldInstruction instruction, Action callback) {
            yield return instruction;
            callback?.Invoke();
        }

        public bool IsCompleted {
            get { return _isDone; }
        }

        public void GetResult() {
            Assert(_isDone);

            if (_exception != null) {
                throw _exception;
            }
        }

        public void Complete(Exception e) {
            Assert(!_isDone);
            _isDone = true;
            _exception = e;
            _continuation?.Invoke();
        }

        void INotifyCompletion.OnCompleted(Action continuation) {
            Assert(_continuation == null);
            Assert(!_isDone);

            _continuation = continuation;
        }

        private void Assert(bool condition) {
            if (!condition) {
                throw new Exception($"Assert failed {nameof(CoroutineAwaiter)}");
            }
        }

        public CoroutineAwaiter GetAwaiter() {
            return this;
        }
    }
}