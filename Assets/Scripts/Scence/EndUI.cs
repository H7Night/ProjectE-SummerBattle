using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour {
    private GameObject gameOverPanel;
    private Button quickButton;
    private Button cancelButton;

    void Start() {
        quickButton = GameObject.Find("QuickButton").GetComponent<Button>();
        quickButton = GameObject.Find("CancelButton").GetComponent<Button>();
        quickButton.onClick.AddListener(QuickGame);
        quickButton.onClick.AddListener(ClickCancel);

        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            gameOverPanel.SetActive(true);
        }
    }

    public void QuickGame() {
        Application.Quit();
    }

    public void ClickCancel() {
        gameOverPanel.SetActive(false);
    }
}