using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleSpawner : SpawnWayPoint {

    

    private float GetInitialAngleForEnemies(Bounds wayPoint)
    {
        var retVal = 0.0f;
        // vertical spawn
        if (wayPoint.center.x == 0)
            retVal = wayPoint.center.y > 0 ? 180.0f : 0.0f;
        else
            retVal = wayPoint.center.x > 0 ? 90.0f : 270.0f;
        return retVal;


    }
    protected override void SpawnEnemies(List<SpawnData> dataList)
    {
        foreach (var spawnData in dataList)
        {
            EnemyGenerator.GeneratePolygon(spawnData.enemyData, spawnData.position, Vector3.zero, Quaternion.identity);
        }
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
}
