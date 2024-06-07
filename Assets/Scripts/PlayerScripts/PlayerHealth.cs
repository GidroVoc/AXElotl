using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int currentLives;
    public int MaxLives = 3;
    public float invincibilityTime = 2f; // ¬рем€ неу€звимости в секундах
    private bool isInvincible = false; // ‘лаг, указывающий, находитс€ ли персонаж в состо€нии неу€звимости
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = MaxLives;
        UpdateLivesText();
    }

    void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        CheckIfCharacterFellIntoAbyss();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentLives -= damage;
            UpdateLivesText();
            if (currentLives <= 0)
            {
                // «десь можно добавить логику смерти персонажа
                RestartLevel();
            }
            else
            {
                StartCoroutine(Invincibility());
            }
        }
    }

    void UpdateLivesText()
    {
        livesText.text = " " + currentLives; // ќбновите текст, чтобы он отображал текущее количество жизней
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void CheckIfCharacterFellIntoAbyss()
    {
        if (transform.position.y < -10) // «амените -10 на уровень, который вы определ€ете как "бездну"
            TakeDamage(currentLives); // ”меньшает жизни персонажа до нул€
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageObject") && collision.gameObject.tag != "DamageArea")
        {
            TakeDamage(1); // Ќаносит 1 единицу урона при столкновении
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DamageObject") && collider.gameObject.tag != "DamageArea")
        {
            TakeDamage(1); // Ќаносит 1 единицу урона при столкновении
        }
    }

    void RestartLevel()
    {
        // «агрузка текущего уровн€ заново
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}