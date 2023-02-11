using UnityEngine;

public class ToUnderSea : MonoBehaviour {
    private Transform seaPoint;

    void Start() {
        seaPoint = GameObject.Find("SeaPoint").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Go~~~~");
            PlayerController.Instance.transform.position = seaPoint.position;
        }
    }
}