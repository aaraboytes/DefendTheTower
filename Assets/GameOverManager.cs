using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text score;
    public Text time;
    private void Start()
    {
        score.text = GameManager._instance.score.ToString();
        time.text = GameManager._instance.time.ToString();
        GameManager._instance.DestroyGameManager();
    }
}
