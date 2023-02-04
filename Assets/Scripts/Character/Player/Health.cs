using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private PlayerController _player;
    //public int numOfHeart;

    private Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        _player = transform.GetComponent<PlayerController>();
        hearts = GameObject.Find("HealthBar").GetComponent<Image[]>();
    }

    void Update()
    {
        if (_player.health > _player.maxHealth)
        {
            _player.health = _player.maxHealth;
        }

        Hearts();
    }

    void Hearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < _player.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < _player.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}