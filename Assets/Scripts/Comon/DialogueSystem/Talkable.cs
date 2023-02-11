using UnityEngine;
using System.Collections;

public class Talkable : MonoBehaviour {
    [SerializeField] private bool isEntered;

    public bool hasName; //默认没名字
    [TextArea(1, 5)] public string[] lines;
    [TextArea(1, 4)] public string[] congratsLines;
    [TextArea(1, 4)] public string[] completedLines;

    public GameObject talkIcon;

    public Questable questable; //当前说话的NPC，是否含有可以委派任务的能力
    public QuestTarget questTarget; //这个脚本中并没有访问，但是在DialogueManager脚本中有使用到这个变量

    public bool canTalk = true;

    private void Update() {
        if (isEntered &&
            canTalk &&
            DialogueManager.Instance.dialoguePanel.activeInHierarchy == false) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)) {
                DialogueManager.Instance.ShowDialogue(lines, hasName);
                if (questable == null) {
                    DialogueManager.Instance.ShowDialogue(lines, hasName);
                }
                else {
                    if (questable.quest.questStatus == Quest.QuestStatus.Completed) {
                        DialogueManager.Instance.ShowDialogue(completedLines, hasName);
                    }
                    else {
                        DialogueManager.Instance.ShowDialogue(lines, hasName);
                    }
                }
            }
        }
    }

    //玩家进入
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && canTalk) {
            isEntered = true;

            talkIcon.SetActive(true); //开启的时候其实透明度还是等于0
            StartCoroutine(FadeIn()); //渐变效果

            DialogueManager.Instance.talkable = this;
        }
    }

    //玩家离开
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && canTalk) {
            isEntered = false;

            talkIcon.GetComponent<CanvasGroup>().alpha = 0;
            talkIcon.SetActive(false);

            DialogueManager.Instance.talkable = null;
        }
    }

    //talkIcon 渐出
    IEnumerator FadeIn() {
        talkIcon.GetComponent<CanvasGroup>().alpha = 0;
        while (talkIcon.GetComponent<CanvasGroup>().alpha < 1) {
            talkIcon.GetComponent<CanvasGroup>().alpha += 0.02f;
            yield return null;
        }
    }
}