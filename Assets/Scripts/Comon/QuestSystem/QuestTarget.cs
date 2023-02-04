using UnityEngine;

//这个脚本将会放在【所有和任务完成】有关的游戏对象上
//比如说可收集的物品、隐藏的NPC、探索的区域等
public class QuestTarget : MonoBehaviour
{
    public string questName;

    public enum QuestType
    {
        Gathering,
        Talk,
        Reach
    };

    public QuestType questType;

    [Header("Talk Type Quest")] public bool hasTalked;

    [Header("Reach Type Quest")] public bool hasReach;

    //这个方法会在【完成的时候】触发 : 比如说，NPC对话完成后、到达探索区域、收集完物品
    public void CheckQuestIsComplete()
    {
        for (int i = 0; i < PlayerQuest.Instance.questList.Count; i++)
        {
            if (questName == PlayerQuest.Instance.questList[i].questName
                && PlayerQuest.Instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
            {
                switch (questType)
                {
                    //收集类型
                    case QuestType.Gathering:
                        if (PlayerQuest.Instance.itemAmount >= PlayerQuest.Instance.questList[i].requireAmount)
                        {
                            PlayerQuest.Instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.Instance.UpdateQuestList();
                        }

                        break;
                    //交流类型
                    case QuestType.Talk:
                        if (hasTalked)
                        {
                            PlayerQuest.Instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.Instance.UpdateQuestList();
                        }

                        break;
                    //探索类型
                    case QuestType.Reach:
                        if (hasReach)
                        {
                            PlayerQuest.Instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                            QuestManager.Instance.UpdateQuestList();
                        }

                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < PlayerQuest.Instance.questList.Count; i++)
            {
                if (PlayerQuest.Instance.questList[i].questName == questName)
                {
                    if (questType == QuestType.Reach)
                    {
                        hasReach = true;
                        CheckQuestIsComplete();
                    }
                }
            }
        }
    }
}