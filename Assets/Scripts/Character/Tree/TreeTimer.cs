using UnityEngine;
using UnityEngine.UI;

public class TreeTimer : MonoBehaviour {
    private TreeManager tree;

    /**
     * 进度条
     */
    [SerializeField] private Slider slider;

    void Start() {
        slider = GetComponentInChildren<Slider>();
        tree = GetComponent<TreeManager>();
        slider.maxValue = tree.timeBtwStages;
    }

    void Update() {
        //隐藏进度条与否
        if (GameManager.Instance.gameMode == GameManager.GameMode.GameProtect) {
            slider.gameObject.SetActive(true);
        }
        else {
            slider.gameObject.SetActive(false);
        }

        //如果到达最后一个阶段，则隐藏进度条
        if (tree.treeStage == tree.treeStages.Length - 1) {
            slider.gameObject.SetActive(false);
        }
        else {
            slider.value = tree.timer;
        }
    }
}