using System;
using UnityEngine;

public class FishController : Enemy {
    public float moveSpeed;
    public float totalTime;
    private float _waitTime;

    public Transform[] movePos;
    //i是1则右，是0则变成左

    public int isBack; //1=上，右；0=下左
    public static event Action onDeathStatic; //事件

    protected override void Start() {
        base.Start();
        _waitTime = totalTime;
        transform.DetachChildren(); //断绝父子关系
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        transform.position =
            Vector2.MoveTowards(transform.position, movePos[isBack].position, moveSpeed * Time.deltaTime);
        //如果两点的距离小于等于0.1
        if (Vector2.Distance(transform.position, movePos[isBack].position) <= 0.1f) {
            //且等待时间小于0
            if (_waitTime < 0) {
                if (isBack == 1) {
                    isBack = 0;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else {
                    isBack = 1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

                _waitTime = totalTime;
            }
            else {
                _waitTime -= Time.deltaTime;
            }
        }
    }

    void TakeHit() {
        if (onDeathStatic != null) {
            onDeathStatic();
        }
    }
}