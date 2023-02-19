using UnityEngine;

public class DontDestory : MonoBehaviour {
    public static DontDestory Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            if (Instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}