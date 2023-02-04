using UnityEngine;
using System.Collections.Generic;

public class PlayerQuest : MonoBehaviour
{
    public static PlayerQuest Instance;

    public int itemAmount;

    public List<Quest> questList = new List<Quest>();
    
    //可用字典记录
    //public Dictionary<string, Quest> questDictionary = new Dictionary<string, Quest>();

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
}
