using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;




public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;
    private UserData             currentPlayData;

    private GameObject playerObject;
    private GameObject pauseMenu;
    private GameObject gameOverMenu;
    private GameObject HUD;


    public UserData pCurrentPlayData
    {
        get{ return currentPlayData; }
    }


    public GameObject pPlayer
    {
        get{ return playerObject; }
    }

    public GameObject pPauseMenu
    {
        get { return pauseMenu; }
    }

    public GameObject pGameOverMenu
    {
        get { return gameOverMenu; }
    }

    public GameObject pHUD
    {
        get { return HUD; }
    }


    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            AddListeners();
            //GetReferenes();
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void AddListeners()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnLevelWasUnloaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //validate scene
        GetReferenes();
    }

    void GetReferenes()
    {
         // add references
        playerObject = GameObject.FindGameObjectWithTag ("Player");
        pauseMenu    = GameObject.FindGameObjectWithTag ("PauseMenu");
        gameOverMenu = GameObject.FindGameObjectWithTag ("GameOverMenu");
        HUD          = GameObject.FindGameObjectWithTag ("HUD");
    }

    void OnLevelWasUnloaded(Scene scene)
    {
        playerObject = null;
        pauseMenu    = null;
        gameOverMenu = null;
        HUD          = null;
    }

    public void StartNewGame()
    {
        currentPlayData = new UserData();
    }

    public void UpdateScore(int addtionalScore)
    {
        currentPlayData.UpdateScore(addtionalScore);
        HUD.GetComponent<HUDManager>().UpdateScore(currentPlayData.pTotalScore);
    }

    void GetSaveData()
    {
        
    }
}
