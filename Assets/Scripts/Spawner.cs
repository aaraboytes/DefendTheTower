using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Header("Closest point")]
    public int closestIndexPoint;
    [Header("Times")]
    public float startBridgeTime;
    public float minTime;
    [Header("Objects")]
    public GameObject[] enemies;
    public GameObject tower;

    float timer;
    int wave = 1;

    void Update () {
        timer += Time.deltaTime;
        wave = LevelManager._instance.getWave();
        //Debug.Log("Running " + timer + " and the startbridgetimeis "+startBridgeTime+" the bridge is " + LevelManager._instance.bridge);
        if(timer>startBridgeTime)
        {
            Debug.Log("timer activated");
            if (!LevelManager._instance.bridge)
            {
                Debug.Log("Spawning an enemy");
                SpawnEnemy();
                LevelManager._instance.IncrementEnemiesOnScene();
                startBridgeTime = minTime;
                timer = 0;
            }
        }
        if(LevelManager._instance.bridge)
        {
            startBridgeTime = LevelManager._instance.GetBridgeTime();
        }
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
            if (prob < 33)
                demonIndex = 1;
            else if (prob < 66)
                demonIndex = 2;
            else
                demonIndex = 3;
        }else if(wave <8)
        {
            if (prob < 50)
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
