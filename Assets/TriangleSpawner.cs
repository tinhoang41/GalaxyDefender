using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleSpawner : SpawnWayPoint {
	
    protected int maxSpawnHorizontal;
    protected int maxSpawnVertical;

    protected GameObject player;
    protected float gap;
    protected float triangleWidth;
    protected float factor;
    protected int triangleLevel;

	protected float enemiesToSpawnPercentage;

    protected override void Initialize()
    {
        base.Initialize();
        player        = GameObject.FindWithTag("Player");
        gap           = player.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
		triangleWidth = objectToSpawn.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        
    }

	protected override void EvaluateEnemiesSpawn ()
	{
		enemiesToSpawn = 0;
		enemiesToSpawnPercentage = (float)((currentWave % EnemiesToSpawnFactor) + 1 ) / EnemiesToSpawnFactor;
	}

	protected override void EvaluateSpawnRate ()
	{
		spawnRate = Mathf.Max(maxSpawnRateInSeconds - (currentWave / SpawnRateFactor) * spawnRateDecrement, minSpawnRateInSeconds);
	}

	protected override void EvaluateSpawnPerTime ()
	{
		enemiesPerTime = Mathf.Min(currentWave / EnemiesSpawnPerTimeFactor + 1, 4);
	}

	protected override void EvaluateEnemiesLevel ()
	{
		enemiesLevel =  (int)(maxEnemiesLevel * ( 1.0f - (float)(currentWave % EnemiesLevelFactor + 1)/ EnemiesLevelFactor));
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


    protected override List<SpawnData> GetCurrentSpawningList()
    {
        var retVal = new List<SpawnData>();
		var playerPosition = player.transform.position;

        for (int i = 0; i < enemiesPerTime; i++)
        {
            var bound = wayPoints[currentWaypointIndex++ % wayPoints.Count];
			retVal.AddRange(GetSpawningDataList(bound));
        }

        return retVal;
    }
}
