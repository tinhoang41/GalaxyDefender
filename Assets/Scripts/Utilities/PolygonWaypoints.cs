using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonWaypoints : SpawnWayPoint {

    protected override void SpawnEnemies(List<SpawnData> dataList)
    {
        foreach(var spawnData in dataList)
           EnemyGenerator.GeneratePolygon(spawnData.enemyData, spawnData.position, Vector3.zero, Quaternion.identity);
    }

    protected override Vector3 GetSpawningEnemyPosition(Bounds bound)
    {
        return new Vector3
            (
                Random.Range(bound.min.x, bound.max.x),
                Random.Range(bound.min.y, bound.max.y),
                0
            );
    }

	protected override List<SpawnData> GetCurrentSpawningList()
	{
		var retVal = new List<SpawnData>();

		for (int i = 0; i < enemiesPerTime; i++)
		{
			var bound = wayPoints[currentWaypointIndex++ % wayPoints.Count];
			retVal.AddRange(GetSpawningDataList(bound));
		}

		return retVal;
	}
}
