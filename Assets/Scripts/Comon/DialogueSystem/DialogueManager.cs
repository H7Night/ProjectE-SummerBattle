using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Text dialogueText, nameText;
    public GameObject dialoguePanel;


    [TextArea(1, 3)] public string[] dialogueLines;
    [SerializeField] private int currentLine;

    private bool _isScrolling;
    [SerializeField] private float scrollingSpeed;

    public Talkable talkable;

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
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0) && !_isScrolling)
            {
                currentLine++;

                if (currentLine < dialogueLines.Length)
                {
                    CheckName();
                    StartCoroutine(ScrollingText()); //开启协程
                }
                else //对话即将结束时候
                {
                    if (GetQuestResult() && talkable.questable.isFinished == false) //如果当前对话的这个任务【已经完成】
                    {
                        ShowDialogue(talkable.congratsLines, talkable.hasName); //祝福台词
                        talkable.questable.isFinished = true; //开关，保证一次
                        
                        print(string.Format("QUEST: {0} HAS COMPLETED", talkable.questable.quest.questName));

                        //MARKER 补充：任务完成以后可以将原先的任务，从questList中移除，不移除也可以，根据游戏要求决定
                        //for(int i = 0; i < Player.instance.questList.Count; i++)
                        //{
                        //    if(Player.instance.questList[i].questName == talkable.questable.quest.questName)
                        //    {
                        //        Player.instance.questList.RemoveAt(i);
                        //    }
                        //}
                    }
                    else //如果当前对话的这个任务【没有完成】
                    {
                        PlayerController.Instance.isTalking = false;
                        dialoguePanel.SetActive(false);
                        
                        if (talkable.questable == null)
                        {
                            Debug.Log("There is no Quest on this person");
                        }
                        else
                        {
                            talkable.questable.DelegateQuest();
                
                            //MARKER 这里其实是针对【收集类类型】的任务，如果在DelegateQuest方法调用以后
                            //我们直接判断我们是否已经完成了这个任务
                            //这部分先转移到DelegateQuest方法中试试
                            //if (GetQuestResult() && talkable.questable.isFinished == false)
                            //{
                            //    ShowDialogue(talkable.congratsLines, talkable.hasName);
                            //    talkable.questable.isFinished = true;
                            //}
                        }
                
                        //这部分是当和任务要求的游戏对象，比如隐藏NPC对话时，hasTalked等于True
                        if (talkable.questTarget != null)
                        {
                            for (int i = 0; i < PlayerQuest.Instance.questList.Count; i++)
                            {
                                if (talkable.questTarget.questName == PlayerQuest.Instance.questList[i].questName)
                                {
                                    talkable.questTarget.hasTalked = true;
                                    talkable.questTarget.CheckQuestIsComplete();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    //交流任务 -- 检查任务是否已经完成，如果完成的话返回值为True
    public bool GetQuestResult()
    {
        if (talkable.questable == null)
            return false;

        for (int i = 0; i < PlayerQuest.Instance.questList.Count; i++)
        {
            if (talkable.questable.quest.questName == PlayerQuest.Instance.questList[i].questName
                && PlayerQuest.Instance.questList[i].questStatus == Quest.QuestStatus.Completed)
            {
                talkable.questable.quest.questStatus = Quest.QuestStatus.Completed;
                return true;
            }
        }

        return false;
    }

    //打开对话窗口，文字滚动
    public void ShowDialogue(string[] newLines, bool hasName)
    {
        dialogueLines = newLines;
        currentLine = 0;

        CheckName();
        SetTextAlign(hasName);

        StartCoroutine(ScrollingText());

        dialoguePanel.SetActive(true);
        nameText.gameObject.SetActive(hasName);

        PlayerController.Instance.isTalking = true;
    }

    //可对话的NPC对话【居左对齐】工具类游戏对象，比如路标【居中对齐】
    private void SetTextAlign(bool hasName)
    {
        if (hasName)
            dialogueText.alignment = (UnityEngine.TextAnchor)TextAlignment.Left;
        else
            dialogueText.alignment = (UnityEngine.TextAnchor)TextAlignment.Center;
    }

    //检查对话内容是否有对话者的名字
    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            //在NameText处显示名字，并且去除标记n-
            nameText.text = dialogueLines[currentLine].Replace("n-", ""); 
            currentLine++; //跳过显示名字的这一行
        }
    }

    //文字滚动效果
    private IEnumerator ScrollingText()
    {
        _isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine])
        {
            dialogueText.text += letter; //Letter by Letter Show
            yield return new WaitForSeconds(scrollingSpeed);
        }

        _isScrolling = false;
    }
}