using System;
using UnityEngine;

public class EnemyController : Enemy
{
    public Transform target;
    public float moveSpeed;
    private SpriteRenderer sr;
    public bool isDead = false;

    protected override void Start()
    {
        target = FindObjectOfType<TreeManager>().transform;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (health <= 0 || Vector2.Distance(transform.position, target.position) <= 0.5f || GameManager.Instance.gameMode == GameManager.GameMode.GameWin)
        {
            isDead = true;
            Death();
        }
        else if(!isDead)
        {
            MoveToTarget();
        }
        //Flip
        Vector3 pos = target.transform.position - transform.position;
        if (Vector3.Cross(transform.forward, pos).y > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    void MoveToTarget()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
    
}