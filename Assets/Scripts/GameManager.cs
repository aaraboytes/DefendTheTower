using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    [Header("Game vars")]
    public int score = 0;
    public int aliveHumans = 5;
    public int enemiesOnScene = 0;
    public int maxEnemiesOnScene = 20;
    int maxMultiplier = 5;
    [SerializeField]
    int multiplier = 1;
    [Header("Game components")]
    public GameObject tower;
    public GameObject shopPanel,advicePanel,pausePanel;
    public Transform[] targets;
    public Transform[] humanTargets;
    [Header("UI Components")]
    public Slider comboSlider;
    public Image comboText;
    public Sprite[] comboWords;
    public int[] maxSliderValues;
    public Image[] hearts;
    public Text scoreText;
    public Text multiplierText;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //Setup UI
        comboSlider.maxValue = maxSliderValues[0];
        comboSlider.value = 0;
    }
    private void Update()
    {
        if(!tower.activeInHierarchy)
        {
            Time.timeScale = 0;
            advicePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            advicePanel.SetActive(false);
        }
    }
    public void IncreseCombo()
    {
        //Set max values
        comboSlider.maxValue = maxSliderValues[multiplier - 1];
        //Increase value
        if (comboSlider.value < maxSliderValues[maxMultiplier - 1])
            comboSlider.value++;

        //Update the slider when it reaches the max
        if (comboSlider.value == comboSlider.maxValue && multiplier<maxMultiplier)
        {
            comboSlider.maxValue = maxSliderValues[multiplier - 1];
            comboSlider.value = 0;
            multiplier++;
            multiplierText.text = "x" + multiplier.ToString();
        }
    }
    public void DecreaseCombo()
    {
        //Check if it can decrease
        if (comboSlider.value > 0)
        {
            //Decrease
            comboSlider.value--;
            //Decrease Combo Status
            if (comboSlider.value <= 0 && multiplier>1)
            {
                multiplier--;
                multiplierText.text = "x" + multiplier.ToString();
                comboSlider.maxValue = maxSliderValues[multiplier];
                comboSlider.value = comboSlider.maxValue - 1;
            }
        }
    }
    public void EnemyKilled(int score)
    {
        enemiesOnScene--;
        this.score += score * multiplier;
        scoreText.text = this.score.ToString();
    }
    public void HumanKilled()
    {
        aliveHumans--;
        hearts[aliveHumans].gameObject.SetActive(false);
    }
    public Transform GetHumanTarget(Transform currentTarget = null)
    {
        Transform newTarget = null;
        do
        {
            int i = 0;
            int humanTargetLenght = GameManager._instance.humanTargets.Length;
            for (i = 0; i < humanTargetLenght; i++)
            {
                if (currentTarget == GameManager._instance.humanTargets[i])
                    break;
            }
            if (Random.Range(0, 1)<0.5f) {  //Avanzar
                if(i == humanTargetLenght - 1)
                    newTarget = GameManager._instance.humanTargets[0];
                else
                    newTarget = GameManager._instance.humanTargets[i + 1];
            }
            else//Retroceder
            {
                if (i == 0)
                    newTarget = GameManager._instance.humanTargets[humanTargetLenght - 1];
                else
                    newTarget = GameManager._instance.humanTargets[i - 1];
            }
            
        }
        while (currentTarget == newTarget);
        return newTarget;
    }
    public void Buy(int cost)
    {
        score -= cost;
        scoreText.text = score.ToString();
    }
}
