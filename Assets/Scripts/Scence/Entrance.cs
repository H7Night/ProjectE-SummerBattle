using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour {
    public string entrancePassword;
    public string sceneName;

    private void Start() {
        if (PlayerController.Instance.scenePassword == entrancePassword) {
            PlayerController.Instance.transform.position = transform.position;
            SceneManager.LoadSceneAsync(sceneName);
        }
        else {
            Debug.LogError("密码错误. 检查场景名和密码");
        }
    }
}