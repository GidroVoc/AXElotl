using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health; // здоровье врага

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // код для удаления врага из игры
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DamageArea"))
        {
            // Проверяем, что объект, вызвавший столкновение, не является игроком
            if (collider.gameObject.GetComponent<PlayerCombat>() == null)
            {
                PlayerCombat playerCombat = collider.gameObject.GetComponentInParent<PlayerCombat>();
                if (playerCombat != null)
                {
                    TakeDamage(playerCombat.damage);
                }
            }
        }
    }

}

