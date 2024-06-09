using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    public void WinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
