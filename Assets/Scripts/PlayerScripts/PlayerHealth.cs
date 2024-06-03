using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{ 
    public float currentLives { get; private set; }
    public int MaxLives = 3;
    public float invincibilityTime = 2f; // Время неуязвимости в секундах
    private bool isInvincible = false; // Флаг, указывающий, находится ли персонаж в состоянии неуязвимости
    public TextMeshProUGUI livesText;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentLives = MaxLives;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCharacterFellIntoAbyss();
        UpdateLivesText();
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        currentLives = Mathf.Clamp(currentLives - damage, 0, MaxLives);

        if (currentLives <= 0)
        {
            anim.SetTrigger("die");
        }
        else
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invincibility());
        }
    }

    void UpdateLivesText()
    {
        livesText.text = " " + currentLives; // Обновите текст, чтобы он отображал текущее количество жизней
    }

    public void AddHealth(float _value)
    {
        currentLives = Mathf.Clamp(currentLives + _value, 0, MaxLives);
        
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void CheckIfCharacterFellIntoAbyss()
    {
        if (transform.position.y < -6) // Замените -10 на уровень, который вы определяете как "бездну"
            RestartLevel(); // Уменьшает жизни персонажа до нуля
    }

    void RestartLevel()
    {
        // Загрузка текущего уровня заново
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}