using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public float startBridgeTime;
    public float decrementTime;
    public float minTime;
    public GameObject enemy;
    public Transform target;

    float timer;
	void Update () {
        timer += Time.deltaTime;
        if (timer > startBridgeTime)
        {
            GameObject currentEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
            Enemy enemyAI = currentEnemy.GetComponent<Enemy>();
            enemyAI.target = target;
            timer = 0;
            if(startBridgeTime>minTime)
                startBridgeTime -= decrementTime;
        }
	}
}
