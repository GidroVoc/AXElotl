using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{ 
    public float currentLives { get; private set; }
    public int MaxLives = 3;
    public float invincibilityTime = 2f; 
    private bool isInvincible = false; 
    public TextMeshProUGUI livesText;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentLives = MaxLives;
    }

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
        livesText.text = " " + currentLives; 
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
        if (transform.position.y < -6) 
            RestartLevel(); 
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}