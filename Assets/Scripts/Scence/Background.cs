using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    public bool lockY;
    //public bool lockX;
    public bool moveWith;
    
    private float startPointX;
    private float startPointY;

    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    void Update()
    {
        if(lockY)
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, startPointY + cam.position.y * moveRate);
        }
        // if(lockX)
        // {
        //     transform.position = new Vector2(transform.position.x, startPointY + Cam.position.y * moveRate);
        // }
        if(moveWith)
        {
            transform.position = new Vector3(0, 0, -10f);
        }
    }
}
