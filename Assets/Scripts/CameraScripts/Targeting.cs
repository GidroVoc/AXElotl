using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ссылка на игрока
    public Vector3 offset; // смещение камеры относительно игрока
    public float dampTime = 0.15f; // время затухания
    private Vector3 velocity = Vector3.zero; // скорость

    void Update()
    {
        if (player)
        {
            // Целевая позиция камеры
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);

            // Плавное перемещение камеры к целевой позиции
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampTime);
        }
    }
}
