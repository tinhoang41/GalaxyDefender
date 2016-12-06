using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject liveText;
    public GameObject hintText;
    public GameObject currentWaveText;
    public GameObject scoreText;

    public void ResetHint()
    {
        hintText.GetComponent<Text>().text = "";
    }

    public void UpdateHintText(string newText)
    {
        hintText.GetComponent<Text>().text = newText;
    }

    public void UpdateLiveText(int currentLives)
    {
        var currentText = liveText.GetComponent<Text>();
        currentText.text = "Live " + currentLives;
    }

    public void UpdateCurrentWave(int currentWave)
    {
        currentWaveText.GetComponent<Text>().text = "Wave : " + currentWave;

    }

    public void UpdateScore(int score)
    {
        scoreText.GetComponent<Text>().text = "Score : " + score;
    }
}
