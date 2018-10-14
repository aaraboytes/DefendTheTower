using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text score;
    public Text time;
    public Text win;

    private void Start()
    {
        int currentScore = GameManager._instance.score;
        float currentTime = GameManager._instance.time/60;
        if (GameManager._instance.GetWin())
        {
            win.gameObject.SetActive(true);
            currentScore += 100000;
        }
        GameManager._instance.DestroyGameManager();
        if (PlayerPrefs.GetInt("score") < currentScore)
            PlayerPrefs.SetInt("score", currentScore);
        if (PlayerPrefs.GetFloat("time") > currentTime)
            PlayerPrefs.SetFloat("time", currentTime);

        score.text = currentScore.ToString();
        time.text = currentTime.ToString();
        
    }
}
