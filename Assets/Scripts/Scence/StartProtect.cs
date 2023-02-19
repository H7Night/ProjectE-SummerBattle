using UnityEngine;
using UnityEngine.UI;

public class StartProtect : MonoBehaviour {
    /**
     * UI窗口
     */
    public GameObject startPanel;

    /**
     * 有任务的
     */
    private Questable _questable;

    /**
     * 确定按钮
     */
    private Button yesButton;

    /**
     * 取消按钮
     */
    private Button noButton;

    /**
     * 是否进入
     */
    public bool isEntered;

    /**
     * 对话图标
     */
    public GameObject talkIcon;

    private void Start() {
        startPanel = GameObject.Find("StartPanel");
        yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        noButton = GameObject.Find("NoButton").GetComponent<Button>();
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
        HideStartPanel();
    }

    private void Update() {
        if (isEntered && Input.GetKeyDown(KeyCode.T)) {
            Debug.Log("按下T");
            ShowStartPanel();
        }
    }

    /**
     * 确定
     */
    public void ClickYesButton() {
        GameManager.Instance.gameMode = GameManager.GameMode.GameProtect;
        PlayerController.Instance.canShoot = true;
        // startPanel.SetActive(false);
        HideStartPanel();
    }

    /**
     * 取消
     */
    public void ClickNoButton() {
        // startPanel.SetActive(false);
        HideStartPanel();
    }

    //玩家进入
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isEntered = true;
            // talkIcon.SetActive(true); //开启的时候其实透明度还是等于0
            // StartCoroutine(FadeIn()); //渐变效果
        }
    }

    //玩家离开
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // talkIcon.GetComponent<CanvasGroup>().alpha = 0;
            // talkIcon.SetActive(false);
            isEntered = false;
        }
    }
    //
    // //talkIcon 渐出
    // IEnumerator FadeIn() {
    //     talkIcon.GetComponent<CanvasGroup>().alpha = 0;
    //     while (talkIcon.GetComponent<CanvasGroup>().alpha < 1) {
    //         talkIcon.GetComponent<CanvasGroup>().alpha += 0.02f;
    //         yield return null;
    //     }
    // }

    //隐藏开始保护Panel
    public void HideStartPanel() {
        startPanel.GetComponent<CanvasGroup>().alpha = 0;
        startPanel.GetComponent<CanvasGroup>().interactable = false;
        startPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //显示开始保护Panel
    public void ShowStartPanel() {
        startPanel.GetComponent<CanvasGroup>().alpha = 1;
        startPanel.GetComponent<CanvasGroup>().interactable = true;
        startPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}