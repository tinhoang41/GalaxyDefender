using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;

public class PauseMenuManager : UIManager {

    // Use this for initialization
    public override void Start () {
        base.Start();
        gameObject.SetActive(false);
    }

    protected override void SetUpActions()
    {
        actions = new List<UnityAction>()
        {
            () => RestartLevel(),
            () => Quit(),
        };
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }

    void Quit()
    {
        Application.Quit();
    }
}
