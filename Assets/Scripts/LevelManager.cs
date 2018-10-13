using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager _instance;
    [Header("Waves")]
    [SerializeField]
    int wave = 1;
    public float[] waveTimes;
    public float bridgeTime;
    public int[] maxEnemiesPerWave;
    public Text timerText;
    [SerializeField]
    int enemiesOnScene=0;       //Enemies alive in the current wave
    [SerializeField]
    int currentEnemies = 0;     //Enemies spawned in the current wave

    private void Awake()
    {
        _instance = this;
    }
    public bool CheckNewWave()
    {
        //Check if the current wave is over to start a bridge
        if (currentEnemies == maxEnemiesPerWave[wave - 1] && enemiesOnScene == 0)
            return true;
        else
            return false;
    }
    public void IncrementEnemiesOnScene(){
        //Increment the enemies that have spawned and the current enemies on scene
        enemiesOnScene++;
        currentEnemies++;
    }
    public bool SpawnMoreEnemies()
    {
        //Returns if the number of current enemies on scene is less than the max in the wave
        if (currentEnemies < maxEnemiesPerWave[wave - 1])
            return true;
        return false;
    }
    public void EnemyKilled()
    {
        enemiesOnScene--;
    }
    public float GetBridgeTime()
    {
        return bridgeTime;
    }
    public void StartNewWave()
    {
        //Reset wave values
        currentEnemies = 0;
        enemiesOnScene = 0;
        if(wave<10)
            wave++;
    }
    public void UpdateUITimer(float time)
    {
        timerText.text = time.ToString().Substring(0,5);
    }
    public void ShowWave()
    {
        timerText.text = "Wave " + wave.ToString();
    }
}
