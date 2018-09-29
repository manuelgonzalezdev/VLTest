using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Player;

namespace VLTest.Enemies
{

    public class EnemyAttack : EnemyComponent
    {

        private void OnTriggerEnter(Collider collision)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.SetDamage(enemy.config.damage);
                enemy.Dead();
            }
        }

    }

}