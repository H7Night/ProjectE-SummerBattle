using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    private String currentSceceName = "01";

    //TODO
    public GameObject dialoguePanel;
    public Text characterNameText;
    public Text dialogueLineText;
    public Image nextBar;

    /**
     * 游戏中UI
     */
    private GameObject gamePanel;

    /**
     * 游戏失败UI
     */
    private GameObject gameOverPanel;

    /**
     * 失败重来按钮
     */
    private Button reStartButton;

    /**
     * 失败退出按钮
     */
    private Button loseQuickButton;

    //物品数量
    private int itemCount;

    /**
     * 物品数量UI图片
     */
    public Image waterItem;

    /**
     * 物品数量UI文字
     */
    public Text waterCount;

    /**
     * 跳转游戏胜利场景按钮
     */
    private Button nextButton;

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

    private void Start() {
        gamePanel = GameObject.Find("GamePanel");
        gameOverPanel = GameObject.Find("GameOverPanel");

        
        waterItem.gameObject.SetActive(false);
        waterCount.gameObject.SetActive(false);
        //失败，重开按钮
        reStartButton = GameObject.Find("ReStartButton").GetComponent<Button>();
        reStartButton.onClick.AddListener(LoseReStart);
        //失败，退出按钮
        loseQuickButton = GameObject.Find("QuickButton").GetComponent<Button>();
        loseQuickButton.onClick.AddListener(LoseQuick);
        //游戏胜利，跳转场景按钮
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(ClickNextButton);
        nextButton.gameObject.SetActive(false);
        
        gameOverPanel.SetActive(false);
    }

    private void Update() {
        CheckIsOver();
        if (PlayerController.Instance.isTalking)
            HideGamePanel();
        else {
            ShowGamePanel();
        }

        UpdateItemCount();
        CheckScence();
    }

    //打开对话框
    public void ToggleDialogueBox(bool isAcitive) {
        dialoguePanel.SetActive(isAcitive);
    }

    //对话框中的next按钮
    public void ToggleNextBar(bool isActive) {
        nextBar.gameObject.SetActive(isActive);
    }

    //配置对话框
    public void SetupDialogue(string dName, string dLine, int dSize) {
        characterNameText.text = dName;
        dialogueLineText.text = dLine;
        dialogueLineText.fontSize = dSize;
        ToggleDialogueBox(true);
    }

    //更新收集物品数量
    void UpdateItemCount() {
        itemCount = GameObject.FindWithTag("Player").GetComponent<PlayerQuest>().itemAmount;
        if (itemCount != 0) {
            waterItem.gameObject.SetActive(true);
            waterCount.gameObject.SetActive(true);
        }
        else {
            waterItem.gameObject.SetActive(false);
            waterCount.gameObject.SetActive(false);
        }

        waterCount.text = "X" + itemCount;
    }

    //打开游戏时UI
    void ShowGamePanel() {
        gamePanel.SetActive(true);
    }

    //隐藏游戏结束时UI
    void HideGamePanel() {
        gamePanel.SetActive(false);
    }

    void CheckIsOver() {
        if (GameManager.Instance.gameMode == GameManager.GameMode.GameLose) {
            gameOverPanel.SetActive(true);
        }
    }

    //失败重开
    public void LoseReStart() {
        // PlayerController.Instance.transform = startPoint.transform;
        SceneManager.LoadScene(currentSceceName);
    }

    //失败退出
    public void LoseQuick() {
    }

    /**
     * 跳转场景04
     */
    void ClickNextButton() {
        SceneManager.LoadScene("04");
    }

    /**
     * 场景检测
     */
    private void CheckScence() {
        var activeScene = SceneManager.GetActiveScene();

        if (currentSceceName != activeScene.name) {
            currentSceceName = activeScene.name;
            Console.WriteLine(currentSceceName);
        }
    }
}