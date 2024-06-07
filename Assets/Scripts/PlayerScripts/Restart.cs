using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R peresed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }
}
