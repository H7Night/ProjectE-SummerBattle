using System;
using UnityEngine;

namespace API
{
    public class LivingEntity : MonoBehaviour, IDamagable
    {
        public float maxHealth;
        public float health;
        private bool _isDead;

        public event Action onDeath;
        protected virtual void Start()
        {
            health = maxHealth;
        }
    
        //实现虚方法
        public virtual void TakenDamage(float damage)
        {
            health -= damage;
            if (health <= 0 && _isDead == false)
            {
                Die();
            }
        }

        protected void Die()
        {
            _isDead = true;
            Destroy(gameObject);
            if (onDeath != null)
                onDeath();
        }
    
    }
}
