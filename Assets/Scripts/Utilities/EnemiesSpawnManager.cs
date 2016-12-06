using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public enum EnemyType : int
{
    INVALID = -1,
    POLYGON = 0,
    TRIANGLE = 1,
    DIAMOND = 2,
    CIRCLE = 3
}

[System.Serializable]
public class WaveData
{
    public WaveType waveType;
    public List<EnemyType> enemies;
}

public class EnemiesSpawnManager : MonoBehaviour {

    protected Dictionary<WaveType, List<SpawnWayPoint>> spawnerForWaveTable;
    // Use this for initialization
    protected List<SpawnWayPoint> currenSpawners;	

    public void Initialize(List<WaveData> waveData)
    {
        SetUpTable(waveData);
        currenSpawners = new List<SpawnWayPoint>();
    }

    public void InitWaveNumber(int waveNumber)
    {
        foreach (var spawnerList in spawnerForWaveTable.Values)
        {
            foreach (var spawner in spawnerList)
                spawner.currentLevel = waveNumber;
        }
    }
    public void EndWave()
    {
        var bonusWaveScore = 0;
        foreach (var spawner in currenSpawners)
        {
            spawner.Stop();
            bonusWaveScore += spawner.pBonusScore;
        }
        GlobalControl.Instance.UpdateScore(bonusWaveScore);
    }

    public bool isFinished()
    {
        foreach (var spawner in currenSpawners)
            if (!spawner.pIsFinishedSpawning)
                return false;
        return true;
    }

    public void StartNewWave(WaveType waveType)
    {
        if (!spawnerForWaveTable.TryGetValue (waveType, out currenSpawners))
            return;

        foreach (var spawner in currenSpawners) 
        {
            spawner.UpdateWaveData ();
            spawner.Run ();
        }
    }

    public void Cleanup()
    {
        
    }

    protected void SetUpTable(List<WaveData> waveData)
    {
        spawnerForWaveTable = new Dictionary<WaveType, List<SpawnWayPoint>>();
       foreach(var data in waveData)
        {
            var waveType = data.waveType;
            var spawnersList = new List<SpawnWayPoint>();
            foreach (var enemyType in data.enemies)
                spawnersList.Add(GetSpawnerBasedOnType(enemyType));
            spawnerForWaveTable.Add(waveType, spawnersList);
        }
    }
    
    protected SpawnWayPoint GetSpawnerBasedOnType(EnemyType type)
    {
        SpawnWayPoint retVal = null;
        switch(type)
        {
            case EnemyType.POLYGON:
                retVal = GetComponent<PolygonWaypoints>();
                break;
            case EnemyType.DIAMOND:
                retVal = GetComponent<DiamondSpawner>();
                break;
            case EnemyType.TRIANGLE:
                retVal = GetComponent<TriangleSpawner>();
                break;
        }

        return retVal;
    }

    public string GetCurrentWaveHint(WaveType currentWaveType)
    {
        var retVal = "";
        switch(currentWaveType)
        {
            case WaveType.KILL:
                retVal = "Kill all enemies";
                break;
            case WaveType.DODGE:
                {
                    var remainTime = GetComponent<TriangleSpawner>().GetRemaindingTime();
                    retVal = "Time: " + remainTime.ToString("00.##") + " s";
                    break;
                }
        }
        return retVal;
    }

    public void AddNewSpawner(WaveType waveType, EnemyType enemyTypeToSpawn)
    {
        var spawnerListForWaveType = spawnerForWaveTable[waveType];
        if (DidContainSpawner(spawnerListForWaveType, enemyTypeToSpawn))
            return;
        spawnerListForWaveType.Add(GetSpawnerBasedOnType(enemyTypeToSpawn));
    }

    bool DidContainSpawner(List<SpawnWayPoint> spawnersList, EnemyType enemyTypeToSpawn)
    {
        foreach (var spawner in spawnersList)
            if (spawner.pEnemyTypeForSpawning == enemyTypeToSpawn)
                return true;
        return false;
       
    }
}
