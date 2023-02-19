using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextButton : MonoBehaviour {
    public Button nextButton;
    void Start()
    {
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(ClickNextButton);
        nextButton.gameObject.SetActive(false);
    }

    /**
     * 跳转场景04
     */
    void ClickNextButton() {
        SceneManager.LoadScene("04");
    }
}
