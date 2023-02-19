using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenceTeleport : MonoBehaviour {
    [SerializeField] private string sceneName;
    [SerializeField] public string password = "01";

    // public GameObject talkIcon;
    public GameObject surePanel;
    private Questable _questable;

    public Button yesButton;
    public Button noButton;

    public bool isEntered;

    private void Start() {
        surePanel.SetActive(false);
        _questable = GetComponent<Questable>();
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
    }

    private void Update() {
        if (_questable.isFinished && isEntered && Input.GetKeyDown(KeyCode.T)) {
            Debug.Log("传送了");
            surePanel.SetActive(true);
        }
    }

    //玩家进入
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // talkIcon.SetActive(true); //开启的时候其实透明度还是等于0
            // StartCoroutine(FadeIn()); //渐变效果
            isEntered = true;
        }
    }

    //玩家离开
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         talkIcon.GetComponent<CanvasGroup>().alpha = 0;
    //         talkIcon.SetActive(false);
    //         isEntered = false;
    //     }
    // }
    /**
     * 确定传送
     */
    public void ClickYesButton() {
        // PlayerController.Instance.sceneIndex = 1;
        PlayerController.Instance.scenePassword = password;
        if(PlayerController.Instance.scenePassword == password)
            SceneManager.LoadSceneAsync(sceneName);
        surePanel.SetActive(false);
    }

    /**
     * 取消传送
     */
    public void ClickNoButton() {
        surePanel.SetActive(false);
    }

    //talkIcon 渐出
    // IEnumerator FadeIn()
    // {
    //     talkIcon.GetComponent<CanvasGroup>().alpha = 0;
    //     while (talkIcon.GetComponent<CanvasGroup>().alpha < 1)
    //     {
    //         talkIcon.GetComponent<CanvasGroup>().alpha += 0.02f;
    //         yield return null;
    //     }
    // }
}