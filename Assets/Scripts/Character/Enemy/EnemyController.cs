using System;
using UnityEngine;

public class EnemyController : Enemy {
    public Transform target;
    public float moveSpeed;
    private SpriteRenderer sr;
    public bool isDead = false;

    public TreeManager tree;

    protected override void Start() {
        target = FindObjectOfType<TreeManager>().transform;
        sr = GetComponent<SpriteRenderer>();
        tree = GameObject.Find("Tree").GetComponent<TreeManager>();
    }

    private void Update() {
        // 被攻击杀死，游戏模式切换（失败或胜利）
        if (health <= 0 || GameManager.Instance.gameMode != GameManager.GameMode.GameProtect) {
            isDead = true;
            Death();
        }
        // 到达树苗处
        else if (Vector2.Distance(transform.position, target.position) <= 0.5f) {
            if (!isDead) {
                tree.enemyCount++;
                Death();
            }
        }
        else if (!isDead) {
            MoveToTarget();
        }

        //Flip
        Vector3 pos = target.transform.position - transform.position;
        if (Vector3.Cross(transform.forward, pos).y > 0) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
    }

    void MoveToTarget() {
        transform.position =
            Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}