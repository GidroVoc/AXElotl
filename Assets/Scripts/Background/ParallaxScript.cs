using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    private float startPos, length;
    public GameObject thisCamera;
    public float paralaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = thisCamera.transform.position.x * (1 - paralaxEffect);
        float dist = thisCamera.transform.position.x * paralaxEffect;

        // ������� ��� � ��������� �� paralaxEffect
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        // ���� ������ ����������� ������, �� ������ startPos
        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
