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
    private float invincibilityTimer = 0f; // ������ ��� ������������ ������� ������������
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
            // ����� ����� �������� ������ ������ ���������
            RestartLevel();
        }
    }

    void UpdateLivesText()
    {
        livesText.text = "�����: " + currentLives; // �������� �����, ����� �� ��������� ������� ���������� ������
    }

    public void CheckInvincible()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            Debug.Log("��������");
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }
     
    void CheckIfCharacterFellIntoAbyss()
    {
        if (transform.position.y < -10) // �������� -10 �� �������, ������� �� ����������� ��� "������"
        {
            TakeDamage(currentLives); // ��������� ����� ��������� �� ����
        }
    }

   
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageObject") && !isInvincible)
        {
            TakeDamage(1); // ������� 1 ������� ����� ��� ������������
            isInvincible = true; // ��������� ��������� � ��������� ������������
            invincibilityTimer = invincibilityTime; // ����� ������� ������������
            Debug.Log("����");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DamageObject") && !isInvincible)
        {
            TakeDamage(1); // ������� 1 ������� ����� ��� ������������
            isInvincible = true; // ��������� ��������� � ��������� ������������
            invincibilityTimer = invincibilityTime; // ����� ������� ������������
        }
    }

    void RestartLevel()
    {
        // �������� �������� ������ ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
