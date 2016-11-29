using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[Serializable]
public class UserData
{
    float totalScore;
    float finalWaveLevel;
}

[Serializable]
public class SaveData
{
    public List<UserData> recordedData;
}


public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;
    private UserData             currentPlayData;
	private SaveData             saveData;

	private GameObject playerObject;
	private GameObject pauseMenu;
	private GameObject gameOverMenu;
	private GameObject HUD;


	public UserData pCurrentPlayData
	{
		get{ return currentPlayData; }
	}

	public SaveData pSaveData
	{
		get { return saveData; }
	}

	public GameObject pPlayer
	{
		get{ return playerObject; }
	}

	public GameObject pPauseMenu
	{
		get { return playerObject; }
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
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

	void AddOnLoadedListeners()
	{
		SceneManager.sceneLoaded += OnLevelWasLoaded;
	}

	void OnLevelWasLoaded(Scene scene, LoadSceneMode mode)
	{
		//validate scene

		// add references
		playerObject = GameObject.FindGameObjectWithTag("Player");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		gameOverMenu = GameObject.FindGameObjectWithTag ("GameOverMenu");
		HUD = GameObject.FindGameObjectWithTag ("HUD");
	}

	void OnLevelWasUnloaded(Scene scene, LoadSceneMode mode)
	{

	}

	void GetSaveData()
	{
		
	}
}
