using UnityEngine;
using UnityEngine.UI;

public class TreeTimer : MonoBehaviour {
    private TreeManager tree;
    [SerializeField] private Slider slider;

    void Start() {
        slider = GetComponentInChildren<Slider>();
        tree = GetComponent<TreeManager>();
        slider.maxValue = tree.timeBtwStages;
    }

    void Update() {
        if (tree.treeStage == tree.treeStages.Length - 1) {
            slider.gameObject.SetActive(false);
        }
        else {
            slider.value = tree.timer;
        }
    }
}