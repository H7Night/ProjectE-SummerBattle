using UnityEngine;

public class ToUnderSea : MonoBehaviour {
    private Transform seaPoint;
    
    void Start() {
        seaPoint = GameObject.Find("SeaPoint").GetComponent<Transform>();
    }
    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerController.Instance.transform.position = seaPoint.position;
        }
    }
}
