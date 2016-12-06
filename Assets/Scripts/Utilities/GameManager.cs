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
    protected bool isGameOver;
    protected bool isPause;

    // Use this for initialization
    void Start ()
    {
        waveManager = GetComponentInChildren<WaveManager>();
        StartCoroutine("Run");
        StartCoroutine("CheckForPause");

    }


    IEnumerator Run()
    {
        yield return null;
        GlobalControl.Instance.StartNewGame();
        while (!checkGameOver())
            yield return waveManager.HandleRunningWave();
        HandleGameOver();
    }

    IEnumerator CheckForPause()
    {
        yield return null;
        while (!checkGameOver())
        {
            var playerData = GlobalControl.Instance.pPlayer.GetComponent<PlayerData>();
            isPause = !GlobalControl.Instance.pPlayer.GetComponent<LeftJoyStickController>().pIsControlling && !GlobalControl.Instance.pPlayer.GetComponent<RightJoyStickController>().pIsControlling;
            if (isPause)
                EnableGamePauseMenu();
            else
                DisableGamePauseMenu();
            yield return null;
        }
    }

    bool checkGameOver()
    {
        var hello = GlobalControl.Instance.pPlayer.GetComponent<PlayerData>();
        isGameOver = GlobalControl.Instance.pPlayer.GetComponent<PlayerData>().pIsDead;
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
        GlobalControl.Instance.pGameOverMenu.SetActive(true);
    }

    void EnableGamePauseMenu()
    {
        Time.timeScale = 0;
        GlobalControl.Instance.pPauseMenu.SetActive(true);
    }

    void DisableGamePauseMenu()
    {
        Time.timeScale = 1;
        GlobalControl.Instance.pPauseMenu.SetActive(false);
    }

    void HandleGameOver()
    {
        DestroyObject(GlobalControl.Instance.pPlayer.gameObject);
        isGameOver = true;
        waveManager.EndWave();
        CleanUpEnemies();
        EnableGameOverMenu();
    }

}
