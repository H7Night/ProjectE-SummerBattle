using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour {
    //Main Panel
    private Button startButton;
    private Button settingButton;

    private Button quitGameButton;

    //Setting Panel
    private GameObject settingPanel;


    private void Start() {
        //Main Panel
        settingPanel = GameObject.Find("SettingPanel");
        startButton = GameObject.Find("StartBtn").GetComponent<Button>();
        settingButton = GameObject.Find("SettingBtn").GetComponent<Button>();
        quitGameButton = GameObject.Find("QuitBtn").GetComponent<Button>();

        startButton.onClick.AddListener(ClickStartButton);
        settingButton.onClick.AddListener(ClickSettingButton);
        quitGameButton.onClick.AddListener(ClickQuitGameButton);




        settingPanel.SetActive(false);
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
        settingPanel.SetActive(true);
    }

    /**
     * 退出游戏
     */
    public void ClickQuitGameButton() {
        Application.Quit();
    }


    
}