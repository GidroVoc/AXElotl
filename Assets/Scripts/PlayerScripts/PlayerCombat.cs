using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject hitbox; // ссылка на ваш объект hitbox
    public int damage = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // замените Space на нужную вам кнопку
        {
            Attack();
        }
    }

    void Attack()
    {
        // ¬ключение hitbox
        hitbox.SetActive(true);

        // ќтключение hitbox после небольшой задержки
        StartCoroutine(DisableHitboxAfterDelay(0.1f)); // замените 0.5 на длительность вашей анимации атаки
        Debug.Log("attack");
    }

    IEnumerator DisableHitboxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hitbox.SetActive(false);
    }
}
