using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenceTeleport : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] public string password;

    public GameObject talkIcon;
    public GameObject surePanel;

    public Button yesButton;
    public Button noButton;

    public bool isEntered;

    private void Start()
    {
        surePanel.SetActive(false);
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.E))
        {
            surePanel.SetActive(true);
        }
    }

    //玩家进入
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkIcon.SetActive(true); //开启的时候其实透明度还是等于0
            StartCoroutine(FadeIn()); //渐变效果
            isEntered = true;
        }
    }

    //玩家离开
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            talkIcon.GetComponent<CanvasGroup>().alpha = 0;
            talkIcon.SetActive(false);
            isEntered = false;
        }
    }

    public void ClickYesButton()
    {
        PlayerController.Instance.scenePassword = password;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ClickNoButton()
    {
        surePanel.SetActive(false);
    }

    //talkIcon 渐出
    IEnumerator FadeIn()
    {
        talkIcon.GetComponent<CanvasGroup>().alpha = 0;
        while (talkIcon.GetComponent<CanvasGroup>().alpha < 1)
        {
            talkIcon.GetComponent<CanvasGroup>().alpha += 0.02f;
            yield return null;
        }
    }
}