using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLTest.Enemies
{
    public class EnemyHealth : EnemyComponent
    {
        public float health;

        public void SetDamage(float damage)
        {
            if (health > 0)
            {
                health = Mathf.Max(0, health - damage);
                if (health == 0)
                {
                    enemy.Kill();
                }
                else
                {
                    enemy.hitEffect.PlayHitEffect();
                }
            }
        }

        public void SetHealth(float health)
        {
            this.health = health;
        }

    }
}