using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    public Text scoreText;
    public Text costText;
    public int costHealHumans, costUpgradeDamage, costUpgradeCadence;
    public AudioClip buySound, denySound;
    AudioSource audio;

    //Shop
    int timesHealHumans = 1, timesUpgradeDamage = 1, timesUpgradeCadence = 1;
    int currentButton = 0;
    public int limitUpgradeDamage, limitUpgradeCadence;
    int score = 0;
    int cost = 0;

    //Modifiers
    FPS player;

    private void Start()
    {
        player = FindObjectOfType<FPS>();
        score = GameManager._instance.score;
        scoreText.text = "Score : "+ score.ToString();
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("volume", 1.0f);
    }

    public void HealHumans()
    {
        if (currentButton != 1)
        {
            currentButton = 1;
            cost = timesHealHumans * costHealHumans;
            costText.text = "Cost : " + cost.ToString();
            score = GameManager._instance.score;
            scoreText.text = "Score : " + score.ToString();
        }
        else
        {
            //Confirm
            PlaySound(CheckPrice(score, cost));
            if (CheckPrice(score, cost))
            {
                NPC[] humansOnScene = new NPC[5];
                humansOnScene = FindObjectsOfType<NPC>();
                timesHealHumans++;
                for (int i = 0; i < 5; i++)
                {
                    if (humansOnScene[i] != null)
                        humansOnScene[i].Heal();
                }
            }
            else
            {
                scoreText.text = "Score is not enough";
            }
        }
    }
    public void UpgradeDamage()
    {
        if (currentButton != 2)
        {
            currentButton = 2;
            cost = timesUpgradeDamage * costUpgradeDamage;
            costText.text = "Cost : " + cost.ToString();
            score = GameManager._instance.score;
            scoreText.text = "Score : " + score.ToString();
        }
        else
        {
            //Confirm
            
            if (CheckPrice(score, cost))
            {
                if(timesUpgradeDamage!=limitUpgradeDamage)
                {
                    PlaySound(true);
                    player.damage++;
                    timesUpgradeDamage++;
                    GameManager._instance.Buy(cost);
                    score = GameManager._instance.score;
                    scoreText.text = "Score : " + GameManager._instance.score.ToString();
                }
                else
                {
                    PlaySound(false);
                    scoreText.text = "Max damage reached";
                }
                    
            }
            else
            {
                PlaySound(false);
                scoreText.text = "Score is not enough";
            }
        }
    }
    public void UpgradeCadence()
    {
        if (currentButton != 3)
        {
            currentButton = 3;
            cost = costUpgradeCadence * timesUpgradeCadence;
            if(player.cadence -0.03f > 0 && timesUpgradeCadence!=limitUpgradeCadence)
                costText.text = "Cost : " + cost.ToString();
            else
                costText.text = "You have the minimun cadence";
            score = GameManager._instance.score;
            scoreText.text = "Score : " + score.ToString();
        }
        else
        {
            //Confirm
            if (CheckPrice(score, cost))
            {
                if (player.cadence - 0.03f > 0 && timesUpgradeCadence!=limitUpgradeCadence)
                {
                    PlaySound(true);
                    timesUpgradeCadence++;
                    player.cadence -= 0.015f;
                    GameManager._instance.Buy(cost);
                    score = GameManager._instance.score;
                    scoreText.text = "Score : " + GameManager._instance.score.ToString();
                }
                else
                {
                    PlaySound(false);
                    costText.text = "You have the minimun cadence";
                }
            }
            else
            {
                PlaySound(false);
                scoreText.text = "Score is not enough";
            }
            
        }
    }
    bool CheckPrice(int score,int cost)
    {
        if (score >= cost)
            return true;
        return false;
    }
    public void CloseShop()
    {
        currentButton = 0;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
    void PlaySound(bool buyed)
    {
        if (buyed)
            audio.clip = buySound;
        else
            audio.clip = denySound;
        audio.Play();
    }
}
