
using UnityEngine;
using UnityEngine.UI;

public class AttackItemUI : MonoBehaviour
{
    public Text attackItemText;
    public Image attackItemImg;
    public int itemCount;
    void Start()
    {
        
    }

    void Update()
    {
        itemCount = GameObject.FindWithTag("Player").GetComponent<PlayerController>().fireCurrentSize;
        if (itemCount != 0)
        {
            attackItemImg.gameObject.SetActive(true);
            attackItemText.gameObject.SetActive(true);
        }
        else
        {
            attackItemImg.gameObject.SetActive(false);
            attackItemText.gameObject.SetActive(false);
        }
        attackItemText.text = "X" + itemCount;
    }
}
