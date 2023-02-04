using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 2;
    private float effectTime = 0;
    protected Animator anim;
    private static readonly int Dying = Animator.StringToHash("Dying");

    public event Action onDeath;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        //获取材质本来的属性  
        GetComponent<Renderer>().material.color = new Color
        (
            GetComponent<Renderer>().material.color.r,
            GetComponent<Renderer>().material.color.g,
            GetComponent<Renderer>().material.color.b,
            //需要改的就是这个属性：Alpha值  
            GetComponent<Renderer>().material.color.a);
    }
    
    public void Death()
    {
        if (onDeath != null)
            onDeath();
        GetComponent<Collider2D>().enabled = false;
        DeathEffect();
    }

    public void JumpOn()
    {
        anim.SetTrigger(Dying);
    }

    private void DeathEffect()
    {
        if (effectTime < 1)
        {
            effectTime += Time.deltaTime;
        }

        if (GetComponent<Renderer>().material.color.a <= 1)
        {
            GetComponent<Renderer>().material.color = new Color
            (
                GetComponent<Renderer>().material.color.r,
                GetComponent<Renderer>().material.color.g,
                GetComponent<Renderer>().material.color.b,

                //减小Alpha值，从1-2秒逐渐淡化 ,数值越大淡化越慢 
                gameObject.GetComponent<Renderer>().material.color.a - effectTime / 2 * Time.deltaTime
            );
        }

        Destroy(this.gameObject, 3f); //3秒后消除
    }
}