using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonWaypoints : SpawnWayPoint {

    protected override IEnumerator SpawnEnemies(List<SpawnData> dataList)
    {
        foreach(var spawnData in dataList)
           EnemyGenerator.GeneratePolygon(spawnData.enemyData, spawnData.position, Vector3.zero, Quaternion.identity);
        yield return null;
    }
}
