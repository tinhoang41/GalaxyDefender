using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnData
{
    public Vector3 position;
    public EnemyDataParameter enemyData;

    public SpawnData(Vector3 pos, EnemyDataParameter data)
    {
        position  = pos;
        enemyData = data;
    }
}

public class SpawnWayPoint : MonoBehaviour {

    public List<Bounds>  wayPoints;
    public float         maxSpawnRateInSeconds;
    public float         minSpawnRateInSeconds;
    public int           maxNumberToSpawn;
    public int           minNumberToSpawn;
    public int           minSpawnPerTime;
    public int           maxSpawnPertime;
    public float         alertTime;

    protected int        currentWave;
    protected float      spawnRate;
    protected int        enemiesToSpawn;
    protected int        enemiesPerTime;
    protected int        currentWaypointIndex;
    protected int        currentEnemiesSpawn;

    // Use this for initialization
    void Start ()
    {
        Initialize();
        StartCoroutine("Spawning");
    }
    
    protected virtual void Initialize()
    {
        currentWaypointIndex = 0;
        currentEnemiesSpawn  = 0;
        SetUpSpawningVariables();

    }
    protected virtual void SetUpSpawningVariables()
    {
        spawnRate      = maxSpawnRateInSeconds;
        enemiesToSpawn = minNumberToSpawn;
        enemiesPerTime = minSpawnPerTime;
    }

    IEnumerator Spawning()
    {
        var numEnemies = 0;
        while(isStillSpawning())
        {
            var spawnDataList = GetCurrentSpawningList();
            ShowAlertSpawning(spawnDataList);
            currentEnemiesSpawn += spawnDataList.Count;
            yield return new WaitForSeconds(alertTime);
            SpawnEnemies(spawnDataList);
            yield return new WaitForSeconds(spawnRate);
            numEnemies++;
        }
    }

    protected virtual void ShowAlertSpawning(List<SpawnData> dataList){}

    protected virtual void SpawnEnemies(List<SpawnData> dataList){}

    protected virtual Vector3 GetSpawningEnemyPosition(Bounds bound)
    {
        return Vector3.zero;
    }

    protected virtual EnemyDataParameter GetSpawningEnemyData()
    {
        return new EnemyDataParameter(1, 5, 1);
    }

    protected virtual SpawnData GetSpawningData(Bounds bound)
    {
        var position             = GetSpawningEnemyPosition(bound);
        var enemiesDataParameter = GetSpawningEnemyData();
        return new SpawnData(position, enemiesDataParameter);
    }

    protected virtual List<SpawnData> GetCurrentSpawningList()
    {
        var retVal = new List<SpawnData>();
        for(int i = 0; i < enemiesPerTime; i++)
        {
            var bound = wayPoints[currentWaypointIndex++ % wayPoints.Count];
            retVal.Add(GetSpawningData(bound));
        }
        return retVal;
    }

    protected virtual bool isStillSpawning()
    {
        return currentEnemiesSpawn < enemiesToSpawn;
    }
}
