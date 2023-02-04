using UnityEngine;

public class Item : MonoBehaviour
{
    private QuestTarget _questTarget;

    private void Start()
    {
        _questTarget = GetComponent<QuestTarget>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerQuest.Instance.itemAmount += 1;
            _questTarget.CheckQuestIsComplete();
            Destroy(gameObject);
        }
    }
}
