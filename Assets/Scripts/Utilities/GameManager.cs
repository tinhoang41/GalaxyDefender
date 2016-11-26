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
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    protected bool isGameOver;
    protected bool isPause;

    // Use this for initialization
    void Start ()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        StartCoroutine ("Run");
        StartCoroutine("CheckForPause");

    }


    IEnumerator Run()
    {
        while(!checkGameOver())
            yield return waveManager.HandleRunningWave();
        DestroyObject(player.gameObject);
        isGameOver = true;
        waveManager.EndWave();
        CleanUpEnemies();
        EnableGameOverMenu();

    }

    IEnumerator CheckForPause()
    {
        while (!checkGameOver())
        {
            isPause = !player.GetComponent<LeftJoyStickController>().pIsControlling && !player.GetComponent<RightJoyStickController>().pIsControlling;
            if (isPause)
                EnableGamePauseMenu();
            else
                DisableGamePauseMenu();
            yield return null;
        }
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

    void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    void EnableGamePauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    void DisableGamePauseMenu()
    {

        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

}
