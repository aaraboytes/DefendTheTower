using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Header("Closest point")]
    public int closestIndexPoint;
    [Header("Spawn times")]
    public float startBridgeTime;
    public float decrementTime;
    public float minTime;
    [Header("Objects")]
    public GameObject enemy;

    float timer;

    void Update () {
        timer += Time.deltaTime;
        if (timer > startBridgeTime && GameManager._instance.enemiesOnScene < GameManager._instance.maxEnemiesOnScene)
        {
            //Spawn new enemy
            GameObject currentEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
            Enemy enemyAI = currentEnemy.GetComponent<Enemy>();
            enemyAI.FindPath(closestIndexPoint);
            enemyAI.indexTarget = closestIndexPoint;
            //Notice to gamemanager
            GameManager._instance.enemiesOnScene++;
            //Restart timer
            timer = 0;
            if(startBridgeTime>minTime)
                startBridgeTime -= decrementTime;
        }
	}
}
