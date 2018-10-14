using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour {
    public void ContinueToGameOverScene(string gameOverSceneName)
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}
