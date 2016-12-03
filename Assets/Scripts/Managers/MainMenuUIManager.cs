using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;

public class MainMenuUIManager : UIManager {

    // Use this for initialization
    protected override void SetUpActions()
    {
        actions = new List<UnityAction>()
        {
            () => Play(),
            () => Options(),
            () => Tutorial(),
            () => Exit(),
        };
    }

    void Play()
    {
        SceneManager.LoadScene(1);
    }

    void Options()
    {

    }

    void Tutorial()
    {

    }

    void Exit()
    {

    }

}
