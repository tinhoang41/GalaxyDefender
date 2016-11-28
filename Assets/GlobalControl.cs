using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
    public UserData             currentPlayData;
    public SaveData             saveData;

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


}
