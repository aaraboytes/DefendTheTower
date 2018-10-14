using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configurations : MonoBehaviour {
    public Slider volume;
    public InputField playerName;
    private void Start()
    {
        volume.value = PlayerPrefs.GetFloat("volume", 1.0f);
        playerName.text = PlayerPrefs.GetString("playerName", "Guy");
    }
    public void SaveConfiguration()
    {
        PlayerPrefs.SetFloat("volume", volume.value);
        PlayerPrefs.SetString("playerName", playerName.text);
    }
    public void EraseData()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetFloat("volume", 1.0f);
        PlayerPrefs.SetString("playerName", "Guy");
    }
    public void GetARImage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("arimage");
    }
}
