using UnityEngine;

public class ReloadAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().LoadFireSize();
        }
    }
}