using UnityEngine;

public class ReloadAttack : MonoBehaviour {
    private void OnTriggerStay2D(Collider2D col) {
        // 按R填充子弹
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Load!");
            col.GetComponent<PlayerController>().LoadFireSize();
            
        }
    }
}