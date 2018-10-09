using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public int score = 0;
    public int aliveHumans = 5;
    public int enemiesOnScene = 0;
    public int maxEnemiesOnScene = 20;
    public GameObject ShopPanel;
    public Transform[] targets;
    public Transform[] humanTargets;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public Transform GetHumanTarget(Transform currentTarget = null)
    {
        Transform newTarget = null;
        do
            newTarget = humanTargets[Random.RandomRange(0, humanTargets.Length - 1)];
        while (currentTarget == newTarget);
        return newTarget;
    }
    public void Buy(int cost)
    {
        score -= cost;
    }
}
