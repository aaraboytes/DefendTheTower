using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
