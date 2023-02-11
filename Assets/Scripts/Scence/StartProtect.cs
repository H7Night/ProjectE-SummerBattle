using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartProtect : MonoBehaviour {
    private GameObject startPanel;
    private Questable _questable;

    private Button yesButton;
    private Button noButton;
    public bool isEntered;

    public GameObject talkIcon;

    private void Start() {
        startPanel = GameObject.Find("StartPanel");
        yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        noButton = GameObject.Find("NoButton").GetComponent<Button>();
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
        startPanel.SetActive(false);
    }

    private void Update() {
        if (isEntered && Input.GetKeyDown(KeyCode.E)) {
            startPanel.SetActive(true);
        }
    }
    
    /**
     * 确定
     */
    public void ClickYesButton() {
        GameManager.Instance.gameMode = GameManager.GameMode.GameProtect;
        PlayerController.Instance.canShoot = true;
        startPanel.SetActive(false);
    }

    /**
     * 取消
     */
    public void ClickNoButton() {
        startPanel.SetActive(false);
    }

    //玩家进入
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isEntered = true;
            talkIcon.SetActive(true); //开启的时候其实透明度还是等于0
            StartCoroutine(FadeIn()); //渐变效果
        }
    }

    //玩家离开
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            talkIcon.GetComponent<CanvasGroup>().alpha = 0;
            talkIcon.SetActive(false);
            isEntered = false;
        }
    }

    //talkIcon 渐出
    IEnumerator FadeIn() {
        talkIcon.GetComponent<CanvasGroup>().alpha = 0;
        while (talkIcon.GetComponent<CanvasGroup>().alpha < 1) {
            talkIcon.GetComponent<CanvasGroup>().alpha += 0.02f;
            yield return null;
        }
    }
}