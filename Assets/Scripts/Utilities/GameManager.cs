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
    public GameObject player;
    protected bool isGameOver;
    // Use this for initialization
    void Start ()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        StartCoroutine ("Run");
    }
    

    IEnumerator Run()
    {
        while(!checkGameOver())
            yield return waveManager.HandleRunningWave();
        isGameOver = true;
        waveManager.EndWave();
        CleanUpEnemies();

    }

    bool checkGameOver()
    {
        isGameOver = player.GetComponent<PlayerData>().pIsDead;
        return isGameOver;
    }

    void CleanUpEnemies()
    {
        var enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemiesList)
            DestroyObject(enemy.gameObject);
    }

}
