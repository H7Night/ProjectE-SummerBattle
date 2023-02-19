using UnityEngine;

public class EnemyController : Enemy {
    /**
     * 目标
     */
    public Vector3 target;

    /**
     * 移动速度
     */
    public float moveSpeed;

    /**
     * 图片
     */
    private SpriteRenderer sr;

    protected override void Start() {
        sr = GetComponent<SpriteRenderer>();
        target = new Vector3(65, -8.5f, 0);
    }

    private void Update() {
        // 被攻击杀死，游戏模式切换（失败或胜利）
        if (health <= 0 || GameManager.Instance.gameMode != GameManager.GameMode.GameProtect) {
            Death();
        }
        else {
            MoveToTarget();
        }

        //翻转
        Vector3 pos = target - transform.position;
        if (Vector3.Cross(transform.forward, pos).y > 0) {
            sr.flipX = true;
        }
        else {
            sr.flipX = false;
        }
    }

    //移动到目标
    void MoveToTarget() {
        transform.position =
            Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}