using System;
using UnityEngine;

public class TransPoint : MonoBehaviour {
    public Questable treeQuestable;
    public bool show;

    private void Start() {
        treeQuestable = GameObject.Find("Tree").GetComponent<Questable>();
    }

    private void Update() {
        Check();
        if (show) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    void Check() {
        if (GameObject.Find("Tree").GetComponent<Questable>().isFinished) {
            show = true;
        }
        else {
            show = false;
        }
    }
}