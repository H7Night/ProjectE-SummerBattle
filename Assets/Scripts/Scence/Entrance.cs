using UnityEngine;

public class Entrance : MonoBehaviour {
    public string entrancePassword;

    private void Start() {
        if (PlayerController.Instance.scenePassword == entrancePassword) {
            PlayerController.Instance.transform.position = transform.position;
        }
        else {
            Debug.LogError("密码错误. 检查场景名和密码");
        }
    }
}