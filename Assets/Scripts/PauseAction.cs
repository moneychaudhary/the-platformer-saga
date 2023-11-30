using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAction : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject legendMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    public void Legend()
    {
        pauseMenu.SetActive(false);
        legendMenu.SetActive(true);
    }

    public void LegendBack()
    {
        legendMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
