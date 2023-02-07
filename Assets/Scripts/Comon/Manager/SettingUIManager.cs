using UnityEngine;
using UnityEngine.UI;

public class SettingUIManager : MonoBehaviour {
    private Button quitSettButton;
    private Button applySettButton;
    private Toggle bgmToggle;

    void Start() {
        quitSettButton = GameObject.Find("QuickButton").GetComponent<Button>();
        quitSettButton.onClick.AddListener(ClickQuitSetButton);
        applySettButton = GameObject.Find("ApplyButton").GetComponent<Button>();
        applySettButton.onClick.AddListener(ClickApplySetButton);

        bgmToggle = GameObject.Find("BGM Toggle").GetComponent<Toggle>();
        BgmManger();

    }

    /**
     * 应用设置
     */
    public void ClickApplySetButton() {
        if (bgmToggle.isOn) {
            PlayerPrefs.SetInt("BGM", 1);
            Debug.Log(PlayerPrefs.GetInt("BGM") + "BGM set true");
        }
        else {
            PlayerPrefs.SetInt("BGM", 0);
            Debug.Log(PlayerPrefs.GetInt("BGM") + "BGM set flase");
        }

        HideSettingPanel();
    }

    /**
     * 退出设置
     */
    public void ClickQuitSetButton() {
        HideSettingPanel();
    }

    /**
     * 背景音乐设置
     */
    void BgmManger() {
        if (PlayerPrefs.GetInt("BGM") == 1) {
            bgmToggle.isOn = true;
            // BGMSource.enabled = true;
        }
        else if (PlayerPrefs.GetInt("BGM") == 0) {
            bgmToggle.isOn = false;
            // BGMSource.enabled = true;
        }
    }

    /**
     * 隐藏SettingPanel
     */
    void HideSettingPanel() {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}