using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType
    {
        Gathering,//收集
        Talk,     //交流
        Reach     //探索
    };

    public enum QuestStatus
    {
        Waitting,//未接受
        Accepted,//已接受
        Completed//已完成
    };

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;

    [Header("Gathering Type Quest")] public int requireAmount;
}
