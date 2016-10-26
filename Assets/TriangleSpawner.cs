using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleSpawner : SpawnWayPoint {

    protected const int SPAWN_TIME_PER_TURN_LEVEL_DIFF = 50;
    protected const float SPAWN_TIMER_DECREMENT = 0.25f;

    public int SPAWN_RATE_LEVEL_DIFF = 10;
    public int ENEMIES_LEVEL_DIFF    = 9;
    public int ENEMIES_TO_SPAWN_DIFF = 10;

    protected int maxSpawnHorizontal;
    protected int maxSpawnVertical;

    public GameObject triangleObject;
    protected GameObject player;
    protected float gap;
    protected float triangleWidth;
    protected float factor;
    protected int triangleLevel;


    protected override void Initialize()
    {
        base.Initialize();
        player        = GameObject.FindWithTag("Player");
        gap           = player.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        triangleWidth = triangleObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        
    }

    protected override void SetUpSpawningVariables()
    {
        base.SetUpSpawningVariables();
        enemiesPerTime = Mathf.Min(currentWave / SPAWN_TIME_PER_TURN_LEVEL_DIFF + 1, 4);
        // speed of enemies fast or slow
        // reset every 10 level 1, 11, 21, 31 becomes min
        triangleLevel = currentWave % ENEMIES_LEVEL_DIFF;
        
        spawnRate = Mathf.Max(maxSpawnRateInSeconds - (currentWave / SPAWN_RATE_LEVEL_DIFF) * SPAWN_TIMER_DECREMENT, minSpawnRateInSeconds);

        // number of enemies spawn will incease every 10 level
        // 1 in 1-> 10, 2 in 11->20 ... until it can no longer fit the 
        // how fast do we spawn enemies
        maxSpawnHorizontal = GetTriangleNumber(wayPoints[0].boundary.size.y * 0.9f);
        maxSpawnVertical   = GetTriangleNumber(wayPoints[1].boundary.size.x * 0.9f);
        enemiesToSpawn     = Mathf.Min(currentWave / ENEMIES_TO_SPAWN_DIFF, Mathf.Max(maxSpawnHorizontal, maxSpawnVertical));

    }

    private int GetTriangleNumber(float length)
    {
        return (int)((length + gap) / (gap + triangleWidth));
    }
    protected override void SpawnEnemies(List<SpawnData> dataList)
    {
        foreach (var spawnData in dataList)
        {
            EnemyGenerator.GenerateTriangle(spawnData.enemyData, spawnData.position, Vector3.zero, spawnData.rotation);
        }
    }

    protected override Vector3 GetSpawningEnemyPosition(Bounds bound)
    {
        //var xPosition = 
        return new Vector3
            (
                Mathf.Clamp(player.transform.position.x, bound.min.x, bound.max.x),
                Mathf.Clamp(player.transform.position.y, bound.min.y, bound.max.y),
                0
            );
    }

    protected override List<SpawnData> GetCurrentSpawningList()
    {
        var retVal = new List<SpawnData>();

        for (int i = 0; i < enemiesPerTime; i++)
        {
            var bound = wayPoints[currentWaypointIndex++ % wayPoints.Count];
            retVal.Add(GetSpawningData(bound));
        }

        return retVal;
    }
}
