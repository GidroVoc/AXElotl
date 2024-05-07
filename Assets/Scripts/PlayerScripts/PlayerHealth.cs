using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int currentLives;
    public int MaxLives = 3;
    public float invincibilityTime = 2f; // Время неуязвимости в секундах
    private float invincibilityTimer = 0f; // Таймер для отслеживания времени неуязвимости
    private bool isInvincible = false; // Флаг, указывающий, находится ли персонаж в состоянии неуязвимости
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = MaxLives;
        UpdateLivesText();
    }

    void FixedUpdate()
    {
        CheckInvincible();
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfCharacterFellIntoAbyss();   
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        UpdateLivesText();
        if (currentLives <= 0)
        {
            // Здесь можно добавить логику смерти персонажа
            RestartLevel();
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "Жизни: " + currentLives; // Обновите текст, чтобы он отображал текущее количество жизней
    }

    public void CheckInvincible()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            Debug.Log("проверил");
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
     
    void CheckIfCharacterFellIntoAbyss()
    {
        if (transform.position.y < -10) // Замените -10 на уровень, который вы определяете как "бездну"
        {
            TakeDamage(currentLives); // Уменьшает жизни персонажа до нуля
        }
    }

   
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageObject") && !isInvincible)
        {
            TakeDamage(1); // Наносит 1 единицу урона при столкновении
            isInvincible = true; // Переводит персонажа в состояние неуязвимости
            invincibilityTimer = invincibilityTime; // Сброс таймера неуязвимости
            Debug.Log("уров");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DamageObject") && !isInvincible)
        {
            TakeDamage(1); // Наносит 1 единицу урона при столкновении
            isInvincible = true; // Переводит персонажа в состояние неуязвимости
            invincibilityTimer = invincibilityTime; // Сброс таймера неуязвимости
        }
    }

    void RestartLevel()
    {
        // Загрузка текущего уровня заново
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
