using UnityEngine;
using System.Collections;

public enum WaveType : int
{
    INVALID = -1,
    KILL    = 0,
    DODGE   = 1,
}

public class GameManager : MonoBehaviour {

    protected WaveManager waveManager;
    // Use this for initialization
    void Start ()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        StartCoroutine ("Run");
    }
    

    IEnumerator Run()
    {
        while(!isGameOver())
            yield return waveManager.HandleRunningWave();
    }

    bool isGameOver()
    {
        return false;
    }

}
