using UnityEngine;

public class Attack : MonoBehaviour
{
    public float lifetime;
    public float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            //other.GetComponentInChildren<Enemy_01_HealthBar>().hp -= damage;
            Destroy(gameObject);
        }
    }
}
