using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject winMenu;
    public GameObject taskMenu;
    public string escenaActual;
    
    public void Update()
    {
        if(!deathMenu.activeSelf && !winMenu.activeSelf && !taskMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        if (deathMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(escenaActual);
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    
}
