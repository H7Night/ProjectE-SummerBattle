using UnityEngine;

//每个【可以派发任务】的NPC都会添加这个脚本
public class Questable : MonoBehaviour
{
    public Quest quest; //可委派的具体任务

    public bool isFinished;

    public QuestTarget questTarget;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        LoadData();
    }

    public void DelegateQuest()
    {
        if (isFinished == false)//未完成任务才可以接受任务
        {
            if (quest.questStatus == Quest.QuestStatus.Waitting)
            {
                quest.questStatus = Quest.QuestStatus.Accepted; //初次委托时将任务更改为【接收】状态
                PlayerQuest.Instance.questList.Add(quest);

                if (quest.questType == Quest.QuestType.Gathering)
                {
                    questTarget.CheckQuestIsComplete();

                    #region 完成任务

                    if (DialogueManager.Instance.GetQuestResult())
                    {
                        DialogueManager.Instance.ShowDialogue(DialogueManager.Instance.talkable.congratsLines,
                            DialogueManager.Instance.talkable.hasName);
                        isFinished = true;
                    }

                    #endregion
                }
            }
            else
            {
                Debug.Log(string.Format("QUEST: {0} has accepted already!", quest.questName));
            }
        }
        else
        {
            Debug.Log("You have finished this quest!");
        }

        QuestManager.Instance.UpdateQuestList();
    }
    

    //这方法会在SceneTransition脚本中调用 -- 先简单处理一下「场景转换」过程中，NPC任务不能保存的情况
    public void SaveData()
    {
        switch (quest.questStatus)
        {
            case Quest.QuestStatus.Waitting:
                PlayerPrefs.SetInt(quest.questName, (int)Quest.QuestStatus.Waitting);
                break;

            case Quest.QuestStatus.Accepted:
                PlayerPrefs.SetInt(quest.questName, (int)Quest.QuestStatus.Accepted);
                break;

            case Quest.QuestStatus.Completed:
                PlayerPrefs.SetInt(quest.questName, (int)Quest.QuestStatus.Completed);
                break;
        }

        switch (isFinished)
        {
            case true:
                PlayerPrefs.SetInt(quest.questName + " isFinished", 0);
                break;

            case false:
                PlayerPrefs.SetInt(quest.questName + " isFinished", 1);
                break;
        }
    }
    
    //这方法会在Start方法中被调用 -- CORE hasKey的检测是必须的，因为游戏开始的时候不存在任何的KEY，肯定会出错，需要检验
    public void LoadData()
    {
        if (PlayerPrefs.HasKey(quest.questName))
        {
            switch (PlayerPrefs.GetInt(quest.questName))
            {
                case 0:
                    quest.questStatus = Quest.QuestStatus.Waitting;
                    break;

                case 1:
                    quest.questStatus = Quest.QuestStatus.Accepted;
                    break;

                case 2:
                    quest.questStatus = Quest.QuestStatus.Completed;
                    break;
            }
        }
        else
        {
            Debug.LogWarning(string.Format("Preferences dont have this key: {0}", quest.questName));
        }

        if (PlayerPrefs.HasKey(quest.questName + " isFinished"))
        {
            switch (PlayerPrefs.GetInt(quest.questName + " isFinished"))
            {
                case 0:
                    isFinished = true;
                    break;

                case 1:
                    isFinished = false;
                    break;
            }
        }
        else
        {
            Debug.LogWarning(string.Format("Preferences dont have this key: {0}", quest.questName + " isFinished"));
        }
    }
}