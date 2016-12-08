using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;




public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;
    private UserDataManager userManager;

    private GameObject playerObject;
    private GameObject pauseMenu;
    private GameObject gameOverMenu;
    private GameObject HUD;

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

    public UserDataManager pUserManager
    {
        get { return userManager; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            InitializeComponents();
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void InitializeComponents()
    {
        AddListeners();
        userManager = new UserDataManager();
    }

    void AddListeners()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnLevelWasUnloaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
        userManager.StartNewGame();
    }

    public void UpdateScore(int addtionalScore)
    {
        userManager.pCurrentPlayData.UpdateScore(addtionalScore);
        HUD.GetComponent<HUDManager>().UpdateScore(userManager.pCurrentPlayData.pTotalScore);
    }

    void GetSaveData()
    {
        
    }
}
