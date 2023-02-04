using System;
using UnityEngine;

public class TreeManager : MonoBehaviour {
    public bool isFinished;
    private SpriteRenderer tree;

    private BoxCollider2D treeCollider;

    public Sprite[] treeStages;
    public int treeStage;

    public float timer;
    private float speed = 1f;
    public float timeBtwStages;
    private Talkable _talkable;

    void Start() {
        tree = GetComponent<SpriteRenderer>();
        treeCollider = GetComponent<BoxCollider2D>();
    }

    // void Tree()
    // {
    //     treeStage = 0;
    //     UpdateTree();
    //     timer = timeBtwStages;
    //     tree.gameObject.SetActive(true);
    // }

    void Update() {
        if (isFinished) {
            _talkable.canTalk = true;
        }

        if (GameManager.Instance.startProtect) {
            _talkable.canTalk = false;
            StartGrow();
        }

        if (treeStage == treeStages.Length - 1) {
            isFinished = true;
            FinishGrow();
        }
    }

    void UpdateTree() {
        tree.sprite = treeStages[treeStage];
        treeCollider.size = tree.sprite.bounds.size;
    }

    void StartGrow() {
        isFinished = false;
        timer += speed * Time.deltaTime;
        //Change State
        if (timer >= timeBtwStages && treeStage < treeStages.Length - 1) {
            timer = 0;
            treeStage++;
            UpdateTree();
        }
    }

    void FinishGrow() {
        GameManager.Instance.startProtect = false;
        // GameManager.Instance.gameMode = GameManager.GameMode.GameWin;
    }
}