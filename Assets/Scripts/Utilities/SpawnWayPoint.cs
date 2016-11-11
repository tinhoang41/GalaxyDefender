using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnData
{
    public Vector3 position;
    public EnemyDataParameter enemyData;
    public Quaternion rotation;

    public SpawnData(Vector3 pos, EnemyDataParameter data, Quaternion rot)
    {
        position  = pos;
        enemyData = data;
        rotation  = rot;
    }
}

[System.Serializable]
public struct WayPointData
{
    public Bounds boundary;
    public float initialEnemyRotation;

    public WayPointData(Bounds bound, float rotation)
    {
        boundary             = bound;
        initialEnemyRotation = rotation;
    }
}

public class SpawnWayPoint : MonoBehaviour {

    public List<WayPointData>  wayPoints;
    public GameObject 		   objectToSpawn;

    public int           currentLevel;
    public int 			 EnemiesLevelFactor;
    public int 			 maxEnemiesLevel;
    public int 			 minEnemiesLevel;

    public int 			 SpawnRateFactor;
    public float 		 spawnRateDecrement;
    public float         maxSpawnRateInSeconds;
    public float         minSpawnRateInSeconds;

    public int 			 EnemiesToSpawnFactor;
    public int           enemiesSpawnIncrement;
    public int           minNumberToSpawn;

    public int 			 EnemiesSpawnPerTimeFactor;
    public int           minSpawnPerTime;
    public int           maxSpawnPertime;

    public float         alertTime;

    protected float      spawnRate;
    protected int        currentWave;

    protected int        enemiesToSpawn;
    protected int        enemiesPerTime;
    protected int        currentWaypointIndex;
    protected int        currentEnemiesSpawn;
    protected int 	     enemiesLevel;
    protected bool 	     isFinishedSpawning;

    public bool pIsFinishedSpawning
    {
        get{return isFinishedSpawning;}
    }

    // Use this for initialization
    void Start ()
    {
        Initialize();
        //StartCoroutine("Spawning");
    }

    public void Run()
    {
        StartCoroutine ("Spawning");
    }

    public void Stop()
    {
        StopAllCoroutines ();
    }

    protected virtual void Initialize()
    {
        currentWave          = 45;
        currentWaypointIndex = 0;
        currentEnemiesSpawn  = 0;
        isFinishedSpawning   = true;
        SetUpSpawningVariables();

    }

    protected virtual void SetUpSpawningVariables()
    {
        isFinishedSpawning = false;
        EvaluateEnemiesLevel();
        EvaluateEnemiesSpawn();
        EvaluateSpawnPerTime ();
        EvaluateSpawnRate ();
    }

    public void UpdateWaveData()
    {
        currentWave++;
        currentWaypointIndex = 0;
        currentEnemiesSpawn = 0;
        SetUpSpawningVariables();
    }

    IEnumerator Spawning()
    {
        //bool spawningList = false;
        var numEnemies = 0;
        while(isStillSpawning())
        {
            var spawnDataList = GetCurrentSpawningList();
            ShowAlertSpawning(spawnDataList);
            currentEnemiesSpawn += spawnDataList.Count;
            yield return new WaitForSeconds(alertTime);
            yield return StartCoroutine("SpawnEnemies", spawnDataList);// SpawnEnemies(spawnDataList);
            yield return new WaitForSeconds(spawnRate);
            numEnemies++;
        }
        isFinishedSpawning = true;
    }

    protected virtual void ShowAlertSpawning(List<SpawnData> dataList){}

    protected virtual void SpawnEnemy(SpawnData dataList)
    {
    }
    protected virtual IEnumerator SpawnEnemies(List<SpawnData> dataList)
    {
        yield return null;
    }

    protected virtual List<Vector3> GetSpawningEnemyPosition(Bounds bound, int num)
    {
        var retVal = new List<Vector3> ();

        for (int i = 0; i < num; i++)
            retVal.Add (GetRandomPosition(bound));
        
        return retVal;
    }

    protected virtual Vector3 GetRandomPosition(Bounds bound)
    {
        return new Vector3
            (
                Random.Range(bound.min.x, bound.max.x),
                Random.Range(bound.min.y, bound.max.y),
                0
            );
    }

    protected virtual EnemyDataParameter GetEnemyData()
    {
        return new EnemyDataParameter(minEnemiesLevel, maxEnemiesLevel, enemiesLevel);
    }

    protected virtual List<SpawnData> GetSpawningDataList(WayPointData wayPoint, int numberOfSpawn)
    {
        var retVal = new List<SpawnData> ();
        var positions = GetSpawningEnemyPosition (wayPoint.boundary, numberOfSpawn);

        foreach (var position in positions)
            retVal.Add (new SpawnData (position, GetEnemyData (), Quaternion.Euler (0, 0, wayPoint.initialEnemyRotation)));
        return retVal;
    }

    protected virtual List<SpawnData> GetCurrentSpawningList()
    {
        var retVal = new List<SpawnData>();

        for (int i = 0; i < enemiesPerTime; i++)
        {
            var bound = wayPoints[currentWaypointIndex];
            retVal.AddRange(GetSpawningDataList(bound, 1));
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
        }
        return retVal;
    }

    protected virtual bool isStillSpawning()
    {
        return currentEnemiesSpawn < enemiesToSpawn;
    }

    protected virtual void EvaluateSpawnRate()
    {
        spawnRate = Mathf.Max(maxSpawnRateInSeconds - (currentWave / SpawnRateFactor) * spawnRateDecrement, minSpawnRateInSeconds);
    }

    protected virtual void EvaluateEnemiesSpawn()
    {
        enemiesToSpawn = minNumberToSpawn + (int)((currentWave / EnemiesToSpawnFactor) * enemiesSpawnIncrement);
    }

    protected virtual void EvaluateSpawnPerTime()
    {
        enemiesPerTime = Mathf.Min((currentWave / EnemiesSpawnPerTimeFactor) + 1, maxSpawnPertime);
    }

    protected virtual void EvaluateEnemiesLevel()
    {
        enemiesLevel = Mathf.Clamp(currentWave / EnemiesLevelFactor, minEnemiesLevel, maxEnemiesLevel);
    }

}
