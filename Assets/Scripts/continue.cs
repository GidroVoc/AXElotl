using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGame : MonoBehaviour
{
    public GameObject panel;
    public void Continue()
    {        
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
