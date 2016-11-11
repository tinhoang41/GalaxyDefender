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
        foreach (var spawner in currenSpawners)
            spawner.Stop();
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

    protected List<SpawnWayPoint> GetSpawner(WaveType type)
    {
        var retVal = new List<SpawnWayPoint>();
        switch (type)
        {
            case WaveType.KILL:
                {
                    retVal.Add(GetComponent<PolygonWaypoints>());
                    retVal.Add(GetComponent<DiamondSpawner>());

                    break;
                }
            case WaveType.DODGE:
                {
                    retVal.Add(GetComponent<TriangleSpawner>());
                    break;
                }
            default:
                break;
        }

        return retVal;
    }
}
