using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    [SerializeField] int startLife= 5;
    [SerializeField] int currentLife;
    [SerializeField] TextMeshProUGUI lifeText;

    public int CurrentLife { get { return currentLife; } }

    private void Awake()
    {
        currentLife = startLife;
        UpdateLifeText();
    }

    private void UpdateLifeText()
    {
        lifeText.text = $"Life: {currentLife.ToString()}";
    }

    public void ReduceLife()
    {
        currentLife -= 1;
        UpdateLifeText();
        if(currentLife <= 0) ReloadScene();
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
