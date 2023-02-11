using UnityEngine;

public class DeadLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            GameManager.Instance.gameMode = GameManager.GameMode.GameLose;
        }
    }
}
