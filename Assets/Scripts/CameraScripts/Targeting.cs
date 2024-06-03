using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public Vector3 offset; // �������� ������ ������������ ������
    public float dampTime = 0.15f; // ����� ���������
    private Vector3 velocity = Vector3.zero; // ��������

    void Update()
    {
        if (player)
        {
            // ������� ������� ������
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);

            // ������� ����������� ������ � ������� �������
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampTime);
        }
    }
}
