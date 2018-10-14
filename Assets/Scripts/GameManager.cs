using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    [Header("Game vars")]
    public int score = 0;
    public int aliveHumans = 5;
    int maxMultiplier = 5;
    [SerializeField]
    int multiplier = 1;
    [Header("Game components")]
    public GameObject tower;
    public GameObject shopPanel,advicePanel,pausePanel,gameOverPanel;
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

    public float time = 0;

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
        //Restart time
        time = 0;
    }
    private void Update()
    {
        //Check if the AR Card is focused
        if(!tower.GetComponent<MeshRenderer>().enabled)
        {
            Time.timeScale = 0;
            advicePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            advicePanel.SetActive(false);
        }
        //Increase game time
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "game")
        {
            time += Time.deltaTime;
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
            multiplierText.text = "X" + multiplier.ToString();
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
        //Update score
        this.score += score * multiplier;
        scoreText.text = this.score.ToString();
    }
    public void HumanKilled()
    {
        aliveHumans--;
        hearts[aliveHumans].gameObject.SetActive(false);
        if(aliveHumans == 0)
        {
            //Game over
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public Transform GetHumanTarget(Transform currentTarget = null)
    {
        Transform newTarget = null;
        int i = 0;
        int humanTargetLenght = GameManager._instance.humanTargets.Length;
        for (i = 0; i < humanTargetLenght; i++)
        {
            if (currentTarget == GameManager._instance.humanTargets[i])
                break;
        }
        float decision = Random.Range(0, 2);
        Debug.Log("Decision : "+decision);
        if (decision<0.5f) {  //Avanzar
            if(i == humanTargetLenght - 1)
                newTarget = GameManager._instance.humanTargets[i - 1];
            else
                newTarget = GameManager._instance.humanTargets[i + 1];
        }
        else//Retroceder
        {
            if (i == 0)
                newTarget = GameManager._instance.humanTargets[i + 1];
            else
                newTarget = GameManager._instance.humanTargets[i - 1];
        }
        //Avoid target repetition with recursivity
        if (currentTarget == newTarget)
            newTarget = GetHumanTarget(currentTarget);
        return newTarget;
    }
    public void Buy(int cost)
    {
        score -= cost;
        scoreText.text = score.ToString();
    }
    public void DestroyGameManager()
    {
        Debug.Log("Bye bye boy!");
        Destroy(gameObject);
    }
}
