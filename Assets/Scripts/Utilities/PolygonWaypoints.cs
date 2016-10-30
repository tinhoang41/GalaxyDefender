using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonWaypoints : SpawnWayPoint {

    protected override void SpawnEnemies(List<SpawnData> dataList)
    {
        foreach(var spawnData in dataList)
           EnemyGenerator.GeneratePolygon(spawnData.enemyData, spawnData.position, Vector3.zero, Quaternion.identity);
    }

    protected override List<SpawnData> GetCurrentSpawningList()
    {
        var retVal = new List<SpawnData>();

        for(int i = 0; i < enemiesPerTime; i++)
        {
            var bound = wayPoints[currentWaypointIndex];
            retVal.AddRange(GetSpawningDataList(bound, 1));
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
        }
        return retVal;
    }

}
