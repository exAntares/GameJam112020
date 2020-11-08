using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour {
    [SerializeField] private int _sceneIndex;
    
    void Awake() {
        SceneManager.LoadScene(_sceneIndex);
    }
}
