using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour {
    //Main Panel
    private Button startButton;
    private Button settingButton;

    private Button quitGameButton;

    //Setting Panel
    private GameObject settingPanel;
    
    public AudioMixer audioMixer;

    private void Start() {
        //Main Panel
        settingPanel = GameObject.Find("SettingPanel");
        startButton = GameObject.Find("StartBtn").GetComponent<Button>();
        settingButton = GameObject.Find("SettingBtn").GetComponent<Button>();
        quitGameButton = GameObject.Find("QuitBtn").GetComponent<Button>();

        startButton.onClick.AddListener(ClickStartButton);
        settingButton.onClick.AddListener(ClickSettingButton);
        quitGameButton.onClick.AddListener(ClickQuitGameButton);

        // HideSettingPanel();
    }

    /**
     * 开始
     */
    public void ClickStartButton() {
        SceneManager.LoadScene("01");
    }

    /**
     * 设置
     */
    public void ClickSettingButton() {
        ShowSettingPanel();
    }

    /**
     * 退出游戏
     */
    public void ClickQuitGameButton() {
        Application.Quit();
    }

    /**
     * 显示SettingPanel
     */
    void ShowSettingPanel() {
        settingPanel.GetComponent<CanvasGroup>().alpha = 1;
        settingPanel.GetComponent<CanvasGroup>().interactable = true;
        settingPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /**
     * 隐藏SettingPanel
     */
    void HideSettingPanel() {
        settingPanel.GetComponent<CanvasGroup>().alpha = 0;
        settingPanel.GetComponent<CanvasGroup>().interactable = false;
        settingPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    /**
     * 调节音量
     */
    public void SetVolume(float value) {
        audioMixer.SetFloat("MainVolume", value);
    }
}