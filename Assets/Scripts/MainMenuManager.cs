using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public Text maxScore, minTime, playerName;
    private void Start()
    {
        maxScore.text = "Max score : " +PlayerPrefs.GetInt("score", 0).ToString();
        minTime.text = "Min time : " + PlayerPrefs.GetFloat("time", 999.99f).ToString();
        playerName.text = PlayerPrefs.GetString("playerName", "GUY");
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
