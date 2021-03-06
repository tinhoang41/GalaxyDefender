﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public enum ControllerSetting 
{
    INVALID  = -1,
    TYPE_ONE = 0,
    TYPE_TWO = 1,
}

[Serializable]
public class UserData : IComparable
{
    int totalScore;
    int finalWaveLevel;
	bool valid;

    public int pTotalScore
    {
        get { return totalScore; }
    }

	public int pFinalWaveLevel
	{
		get { return finalWaveLevel; }
	}

    public bool pIsValid
    {
        get { return valid; }
        set { valid = value; }
    }

    public UserData()
    {
        totalScore = 0;
        finalWaveLevel = 1;
		valid = false;
    }

	public UserData(bool isValid)
	{
		totalScore = 0;
		finalWaveLevel = 1;
		valid = isValid;
	}

    public void UpdateScore(int score)
    {
        totalScore += score;
    }

    public void IncreaseWave()
    {
        finalWaveLevel++;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        UserData otherData = obj as UserData;
        if (otherData != null)
        {
			return (otherData.pTotalScore - this.pTotalScore) ;
        }
        else
            throw new ArgumentException("Object is not a Temperature");
    }

}

[Serializable]
public abstract class SaveData
{
    protected string FileName;
    protected string DirectoryName;
    protected string FullFilePath;

    public SaveData()
    {
        SetFilePath();
    }

    public virtual void SetFilePath()
    {
        FullFilePath = DirectoryName + FileName;
    }


    public void Save()
    {
        if (string.IsNullOrEmpty(FileName) || !Directory.Exists(DirectoryName))
            return;

        try
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(FullFilePath);
            bf.Serialize(file, this);
            file.Close();
        }
        catch (Exception e)
        {
            Debug.Log("exception caught in Saving data" + e.ToString());
        }
    }

    public bool Load()
    {
		var createFile = false;
		if (string.IsNullOrEmpty(FileName) || !Directory.Exists(DirectoryName))
            return false;
		
		createFile = !File.Exists(FullFilePath);
        var retVal = false;

		if (createFile)
		{
			Save ();
		} 
		else {
			try {

				BinaryFormatter bf = new BinaryFormatter ();
				FileStream fileStream = File.Open (FullFilePath, FileMode.Open);
				retVal = GetSaveDataFromStream (fileStream, bf);            
				fileStream.Close ();
			} catch (Exception e) {
				Debug.Log ("exception caught in Loading data" + e.ToString ());
				retVal = false;
			}
		}
        return retVal;
    }

    public abstract bool GetSaveDataFromStream(FileStream fileStream, BinaryFormatter bf);
}

[Serializable]
public class ProfileSettings : SaveData
{
    public ControllerSetting controllerSettings;
    public bool MusicEnable;

    public override bool GetSaveDataFromStream(FileStream fileStream, BinaryFormatter bf)
    {
        var retVal               = false;
        ProfileSettings settings = (ProfileSettings)bf.Deserialize(fileStream);
        retVal                   = settings != null;

        if (retVal)
        {
            this.controllerSettings = settings.controllerSettings;
            this.MusicEnable = settings.MusicEnable;
        }
        return retVal;
    }

    public override void SetFilePath()
    {
        DirectoryName = Application.persistentDataPath + "/SaveData";
        FileName      = "/ProfileSettings.dat";
        base.SetFilePath();
    }

}

[Serializable]
public class HighScoreData : SaveData
{
    public UserData[] highScoresData;
    public const int highScoreSize = 10;

    public HighScoreData()
    {
        highScoresData = new UserData[highScoreSize];
        for (int i = 0; i < highScoresData.Length; i++)
            highScoresData[i] = new UserData();
    }

    public override bool GetSaveDataFromStream(FileStream fileStream, BinaryFormatter bf)
    {
        var retVal              = false;
        HighScoreData highscore = (HighScoreData)bf.Deserialize(fileStream);
        retVal                  = highscore != null;

        if(retVal)
            this.highScoresData = highscore.highScoresData;

        return retVal;
    }

    public override void SetFilePath()
    {
        DirectoryName = Application.persistentDataPath + "/SaveData";
        FileName      = "/HighScore.dat";
        base.SetFilePath();
    }

    public void UpdateHighScore(UserData currentPlayData)
    {
        //asuming the array is already sorted
        if (currentPlayData.pTotalScore <= highScoresData[highScoresData.Length - 1].pTotalScore)
            return;
        highScoresData[highScoresData.Length - 1] = currentPlayData;
        Array.Sort(highScoresData);
    }

}
public class UserDataManager {

    UserData        currentPlayData;
    HighScoreData   recordedData;
    ProfileSettings profileSettings;

    string DIRECTORY_PATH = Application.persistentDataPath + "/SaveData";

    public UserData pCurrentPlayData
    {
        get { return currentPlayData; }
    }

    public HighScoreData pRecordedData
    {
        get { return recordedData; }
    }

    public UserDataManager()
    {
        currentPlayData = new UserData(true);
		recordedData    = new HighScoreData ();
		profileSettings = new ProfileSettings ();
		CheckSaveDataDirectory ();
        LoadData();

    }
    
	public void LoadData()
	{
		profileSettings.Load ();
		recordedData.Load ();
	}

	public void SaveSettings()
	{
		profileSettings.Save ();
	}

	public void UpdateAndSaveHighScore()
	{
        recordedData.UpdateHighScore(currentPlayData);
		recordedData.Save ();
	}

    public void StartNewGame()
    {
        currentPlayData = new UserData();
    }

	void CheckSaveDataDirectory()
	{
		if (Directory.Exists (DIRECTORY_PATH))
			return;
		Directory.CreateDirectory (DIRECTORY_PATH);
	}
}
