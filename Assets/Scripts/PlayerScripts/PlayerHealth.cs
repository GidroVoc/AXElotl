using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int currentLives;
    public int MaxLives = 3;
    public float invincibilityTime = 2f; // ����� ������������ � ��������
    private bool isInvincible = false; // ����, �����������, ��������� �� �������� � ��������� ������������
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
                // ����� ����� �������� ������ ������ ���������
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
        livesText.text = " " + currentLives; // �������� �����, ����� �� ��������� ������� ���������� ������
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void CheckIfCharacterFellIntoAbyss()
    {
        if (transform.position.y < -10) // �������� -10 �� �������, ������� �� ����������� ��� "������"
            TakeDamage(currentLives); // ��������� ����� ��������� �� ����
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageObject") && collision.gameObject.tag != "DamageArea")
        {
            TakeDamage(1); // ������� 1 ������� ����� ��� ������������
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DamageObject") && collider.gameObject.tag != "DamageArea")
        {
            TakeDamage(1); // ������� 1 ������� ����� ��� ������������
        }
    }

    void RestartLevel()
    {
        // �������� �������� ������ ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}