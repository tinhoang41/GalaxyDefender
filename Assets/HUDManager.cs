using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject liveText;
    public GameObject hintText;
    public GameObject currentWaveText;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void UpdateLiveText(int currentLives)
    {
        var currentText = liveText.GetComponent<Text>();
        currentText.text = "Live " + currentLives;
    }

    void UpdateCurrentWave(int currentLives)
    {
        var currentText = liveText.GetComponent<Text>();
        currentText.text = "Live " + currentLives;
    }
}
