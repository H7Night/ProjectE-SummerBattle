using UnityEngine;

public class TreeManager : MonoBehaviour {
    /**
     * 是否生长完成
     */
    public bool isFinished;

    /**
     * SpriteRender组件
     */
    private SpriteRenderer tree;

    /**
     * 碰撞体
     */
    private BoxCollider2D treeCollider;

    /**
     * 存储树的各阶段
     */
    public Sprite[] treeStages;

    /**
     * 树的当前状态
     */
    public int treeStage;

    public float timer;
    private float speed = 1f;

    /**
     * 树更换下一阶段需要的事件
     */
    public float timeBtwStages;

    private Talkable _talkable;

    /**
     * 到达树这里的敌人的数量
     */
    public int enemyCount;

    void Start() {
        tree = GetComponent<SpriteRenderer>();
        treeCollider = GetComponent<BoxCollider2D>();
        _talkable = GetComponent<Talkable>();
        _talkable.canTalk = false;
    }

    // void Tree()
    // {
    //     treeStage = 0;
    //     UpdateTree();
    //     timer = timeBtwStages;
    //     tree.gameObject.SetActive(true);
    // }

    void Update() {
        //生长完成，可以对话
        if (isFinished) {
            _talkable.canTalk = true;
        }

        //正在进行树苗胜场
        if (GameManager.Instance.gameMode == GameManager.GameMode.GameProtect) {
            _talkable.canTalk = false;
            StartGrow();
        }

        //失败
        if (enemyCount == 3) {
            GameManager.Instance.gameMode = GameManager.GameMode.GamePlay;
        }

        //胜利
        if (treeStage == treeStages.Length - 1) {
            isFinished = true;
            FinishGrow();
        }
    }

    /**
     * 更换图片，控制碰撞体大小
     */
    void UpdateTree() {
        tree.sprite = treeStages[treeStage];
        treeCollider.size = tree.sprite.bounds.size;
    }

    /**
     * 开始生长
     */
    void StartGrow() {
        isFinished = false;
        timer += speed * Time.deltaTime;
        //换图片，下一阶段
        if (timer >= timeBtwStages && treeStage < treeStages.Length - 1) {
            timer = 0;
            treeStage++;
            UpdateTree();
        }
    }

    /**
     * 生长完成，胜利
     */
    void FinishGrow() {
        // GameManager.Instance.gameMode = GameManager.GameMode.GamePlay;
        GameManager.Instance.gameMode = GameManager.GameMode.GameWin;
        Debug.Log("Win!!!");
    }
}