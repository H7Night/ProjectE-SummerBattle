using UnityEngine;
using UnityEngine.UI;

//MARKER 这个脚本将会放在【UI Canvas】游戏对象上
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public GameObject[] questArray;

    public GameObject questPanel;
    private Button closeButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateQuestList();
        questPanel.SetActive(false);
        closeButton = questPanel.GetComponentInChildren<Button>(); 
        closeButton.onClick.AddListener(ClickCloseButton);
    }

    //在【领取好任务】【任务完成后】调用
    public void UpdateQuestList()
    {
        for (int i = 0; i < PlayerQuest.Instance.questList.Count; i++)//有多少个任务显示多少个List，而不是有多少List显示多少个任务
        {
            questArray[i].transform.GetChild(0).GetComponent<Text>().text = PlayerQuest.Instance.questList[i].questName;

            if (PlayerQuest.Instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                questArray[i].transform.GetChild(1).GetComponent<Text>().text
                    = "<color=red>" + PlayerQuest.Instance.questList[i].questStatus + "</color>";
            }
            else if (PlayerQuest.Instance.questList[i].questStatus == Quest.QuestStatus.Completed)
            {
                questArray[i].transform.GetChild(1).GetComponent<Text>().text
                    = "<color=lime>" + PlayerQuest.Instance.questList[i].questStatus + "</color>";
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerController.Instance.isTalking == false)
        {
            questPanel.gameObject.SetActive(!questPanel.activeInHierarchy);
        }

        //SOLVED 修复：当开启【UI任务列表】时，和NPC开启对话【UI任务列表】还开启的问题
        if (PlayerController.Instance.isTalking && questPanel.activeInHierarchy)
        {
            questPanel.gameObject.SetActive(false);
        }
    }

    void ClickCloseButton()
    {
        questPanel.SetActive(false);
    }
}