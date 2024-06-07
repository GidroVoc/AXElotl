using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject hitbox; // ������ �� ��� ������ hitbox
    public int damage = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // �������� Space �� ������ ��� ������
        {
            Attack();
        }
    }

    void Attack()
    {
        // ��������� hitbox
        hitbox.SetActive(true);

        // ���������� hitbox ����� ��������� ��������
        StartCoroutine(DisableHitboxAfterDelay(0.1f)); // �������� 0.5 �� ������������ ����� �������� �����
        Debug.Log("attack");
    }

    IEnumerator DisableHitboxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hitbox.SetActive(false);
    }
}
