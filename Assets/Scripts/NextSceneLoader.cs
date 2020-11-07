using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour {
    void Awake() {
        SceneManager.LoadScene(1);
    }
}
