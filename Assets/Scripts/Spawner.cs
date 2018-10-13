using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Header("Closest point")]
    public int closestIndexPoint;
    [Header("Times")]
    public float startBridgeTime;
    public float decrementTime;
    public float minTime;
    [Header("Objects")]
    public GameObject[] enemies;
    public GameObject tower;

    float timer;
    int wave = 1;
    bool bridge = false;

    void Update () {
        timer += Time.deltaTime;
        //You can spawn more enemies
        if (LevelManager._instance.SpawnMoreEnemies() && timer>startBridgeTime)
        {
            LevelManager._instance.ShowWave();
            //Spawns a new enemy taking the wave value and setting probabilities, add first target too
            SpawnEnemy();
            //Notice to level manager
            LevelManager._instance.IncrementEnemiesOnScene();
            //Restart timer
            timer = 0;
            if (startBridgeTime > minTime)
                startBridgeTime = minTime;
        }
        //The enemies on the scene are the maximum of the current wave
        else
        {   
            if (!bridge && LevelManager._instance.CheckNewWave())
            {
                //Finds the bridge time
                startBridgeTime = LevelManager._instance.GetBridgeTime();
                timer = 0;
                bridge = true;
            }
            //bridge ended
            if (timer>startBridgeTime && bridge)
            {
                bridge = false;
                LevelManager._instance.StartNewWave();
                wave++;
            }
        }
        if (bridge)
            LevelManager._instance.UpdateUITimer(startBridgeTime - timer);
        else
            LevelManager._instance.ShowWave();
	}
    void SpawnEnemy()
    {
        //Spawn new enemy
        int prob = Random.RandomRange(0, 100);
        int demonIndex = 0;
        if (wave < 3)
        {//Spawn normal and fast enemies
            if (prob > 50)
                demonIndex = 0;
            else
                demonIndex = 1;
        }
        else if (wave < 5)
        {//Spawn normal, fast and tanks
            if (prob < 45)
                demonIndex = 0;
            else if (prob < 90)
                demonIndex = 1;
            else
                demonIndex = 2;
        }
        else if (wave < 7)
        {//Spawn normal, fast, tanks and climbers
            if (prob < 40)
                demonIndex = 0;
            else if (prob < 80)
                demonIndex = 1;
            else if (prob < 90)
                demonIndex = 2;
            else
                demonIndex = 3;
        }
        GameObject currentEnemy = (GameObject)Instantiate(enemies[demonIndex], transform.position, Quaternion.identity);
        currentEnemy.transform.SetParent(tower.transform);
        //Set target
        Enemy enemyAI = currentEnemy.GetComponent<Enemy>();
        enemyAI.FindPath(closestIndexPoint);
        enemyAI.indexTarget = closestIndexPoint;
    }
}
