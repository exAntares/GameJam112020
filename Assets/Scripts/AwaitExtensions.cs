using UnityEngine;

namespace GameJam {
    public static class AwaitExtensions {
        public static CoroutineAwaiter ToAwaitable(this WaitForEndOfFrame instruction, MonoBehaviour coroutineExecuter) {
            return new CoroutineAwaiter(instruction, coroutineExecuter);
        }

        public static CoroutineAwaiter ToAwaitable(this WaitForFixedUpdate instruction, MonoBehaviour coroutineExecuter) {
            return new CoroutineAwaiter(instruction, coroutineExecuter);
        }

        public static CoroutineAwaiter ToAwaitable(this WaitForSeconds instruction, MonoBehaviour coroutineExecuter) {
            return new CoroutineAwaiter(instruction, coroutineExecuter);
        }

        public static CoroutineAwaiter ToAwaitable(this WaitForSecondsRealtime instruction, MonoBehaviour coroutineExecuter) {
            return new CoroutineAwaiter(instruction, coroutineExecuter);
        }

        public static CoroutineAwaiter ToAwaitable(this WaitUntil instruction, MonoBehaviour coroutineExecuter) {
            return new CoroutineAwaiter(instruction, coroutineExecuter);
        }

        public static CoroutineAwaiter ToAwaitable(this WaitWhile instruction, MonoBehaviour coroutineExecuter) {
            return new CoroutineAwaiter(instruction, coroutineExecuter);
        }
    }
}
