using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public GameObject pausePanel;
    private bool GamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GamePaused == false)
        {
            GamePaused = true;
            if (GamePaused)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GamePaused == true)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
