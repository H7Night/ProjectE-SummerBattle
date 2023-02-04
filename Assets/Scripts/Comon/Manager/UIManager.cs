using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    //TODO
    public GameObject dialoguePanel;
    public Text characterNameText;
    public Text dialogueLineText;
    public Image nextBar;

    public GameObject gamePanel;

    //Item
    private int itemCount;
    public Image waterItem;
    public Text waterCount;

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
        waterItem.gameObject.SetActive(false);
        waterCount.gameObject.SetActive(false);
    }

    private void Update() {
        if(PlayerController.Instance.isTalking)
            HideGamePanel();
        UpdateItemCount();
    }

    public void ToggleDialogueBox(bool isAcitive) {
        dialoguePanel.SetActive(isAcitive);
    }

    public void ToggleNextBar(bool isActive) {
        nextBar.gameObject.SetActive(isActive);
    }

    public void SetupDialogue(string dName, string dLine, int dSize) {
        characterNameText.text = dName;
        dialogueLineText.text = dLine;
        dialogueLineText.fontSize = dSize;
        ToggleDialogueBox(true);
    }

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

    void HideGamePanel() {
        gamePanel.SetActive(false);
    }
}