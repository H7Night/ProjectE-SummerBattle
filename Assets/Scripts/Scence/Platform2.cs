using UnityEngine;

public class Platform2 : MonoBehaviour
{
    public float moveSpeed;
    private float waitTime;
    public float totalTime;

    public Transform[] movePos;
    public int isBack; //1=上，右；0=下左

    public bool needPlayer;
    public bool canMove;
    private bool _getPlayer;
    public bool moveTo;

    private void Start()
    {
        waitTime = totalTime;
        transform.DetachChildren();
    }

    private void Update()
    {
        if ( _getPlayer)
            Move();
        else if (moveTo)
            MoveTo();
    }

    private void Move()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, movePos[isBack].position, moveSpeed * Time.deltaTime);
        //如果两点的距离小于等于0.1
        if (Vector2.Distance(transform.position, movePos[isBack].position) <= 0.1f)
        {
            //且等待时间小于0
            if (waitTime < 0)
            {
                if (isBack == 1)
                {
                    isBack = 0;
                }
                else
                {
                    isBack = 1;
                }

                waitTime = totalTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void MoveTo()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, movePos[isBack].position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _getPlayer = true;
    }
}