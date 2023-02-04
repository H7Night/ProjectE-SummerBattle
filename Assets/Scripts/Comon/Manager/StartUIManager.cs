using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    private Button startButton;
    private Button settingButton;
    private Button quitButton;

    private void Start()
    {
        startButton = transform.Find("StartBtn").GetComponent<Button>();
        settingButton = transform.Find("settingBtn").GetComponent<Button>();
        quitButton = transform.Find("QuitBtn").GetComponent<Button>();
        
        startButton.onClick.AddListener(ClickStartButton);
        settingButton.onClick.AddListener(ClickSettingButton);
        quitButton.onClick.AddListener(ClickQuitButton);
    }

    public void ClickStartButton()
    {
        SceneManager.LoadScene("01");
    }

    public void ClickSettingButton()
    {
        
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }
}
