using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esc : MonoBehaviour
{
    public GameObject panel;
    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}
