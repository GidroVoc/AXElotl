using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterEnemies : MonoBehaviour
{
    public TextMeshProUGUI enemyCounter;
    public int maxEnemies;
    public int currentEnemies;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Awake()
    {
        currentEnemies = 0;
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemiesText();
    }

    public void UpdateEnemiesCounter()
    {
        currentEnemies += 1;

        if (currentEnemies == maxEnemies)
        {
            uiManager.WinScreen();
            return;
        }
    }

    public void UpdateEnemiesText()
    {
        enemyCounter.text = currentEnemies + "/" + maxEnemies;
    }
}
