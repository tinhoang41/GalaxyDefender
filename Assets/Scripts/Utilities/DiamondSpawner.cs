	using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiamondSpawner : SpawnWayPoint
{
    public List<WayPointData> antiCornersWaypoints;
    protected GameObject player;
    public override EnemyType pEnemyTypeForSpawning
    {
        get { return EnemyType.DIAMOND; }
    }


    public bool pPlayerHidesInCorner
    {
        get
        {
            foreach (var waypoint in antiCornersWaypoints)
                if (waypoint.boundary.Contains(player.transform.position))
                    return true;
            return false;
                
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        player = GameObject.FindWithTag("Player");
    }

    protected override IEnumerator SpawnEnemies(List<SpawnData> dataList)
    {
        foreach (var spawnData in dataList)
            EnemyGenerator.GenerateDiamond(spawnData.enemyData, spawnData.position, Vector3.zero, Quaternion.identity);
        yield return null;
    }
}
