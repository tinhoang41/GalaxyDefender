using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleSpawner : SpawnWayPoint {

    public float offsetTimer;
    public float EnemiesToSpawnIncrement;
    protected int maxSpawnHorizontal;
    protected int maxSpawnVertical;

    protected GameObject player;
    protected float gap;
    protected float triangleWidth;
    protected float factor;
    protected int triangleLevel;

    protected float enemiesToSpawnPercentage;

    private float minSpawnCenterX, maxSpawnCenterX;
    private float minSpawnCenterY, maxSpawnCenterY;

    private int horizontalSpawningNumber;
    private int verticalSpawningNumber;

    private float horizontalSpawnLength;
    private float verticalSpawnLength;

    protected override void Initialize()
    {
        player        = GameObject.FindWithTag("Player");
        gap           = player.GetComponentInChildren<SpriteRenderer>().bounds.size.x * 1.350f;
        triangleWidth = objectToSpawn.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

        base.Initialize();
       
    }

    protected override void SetUpSpawningVariables()
    {
        base.SetUpSpawningVariables();
        SetMinMaxCenterSpawning();
    }

    protected void SetMinMaxCenterSpawning()
    {
        var horizontolWaypointLength = 0.0f;
        var verticalWaypointLength   = 0.0f;

        foreach (var waypoint in wayPoints)
        {
            horizontolWaypointLength = waypoint.boundary.size.x > horizontolWaypointLength ? waypoint.boundary.size.x : horizontolWaypointLength;
            verticalWaypointLength   = waypoint.boundary.size.y > verticalWaypointLength ? waypoint.boundary.size.y : verticalWaypointLength;
        }

        horizontalSpawningNumber = Mathf.Max(GetTriangleNumber(horizontolWaypointLength * enemiesToSpawnPercentage), 1);
        verticalSpawningNumber   = Mathf.Max(GetTriangleNumber(verticalWaypointLength * enemiesToSpawnPercentage), 1);

        horizontalSpawnLength    = (horizontalSpawningNumber * triangleWidth) + (gap * (horizontalSpawningNumber));
        verticalSpawnLength      = (verticalSpawningNumber * triangleWidth) + (gap * (verticalSpawningNumber));

        minSpawnCenterX          = (0 - horizontolWaypointLength / 2.0f) + (horizontalSpawnLength / 2.0f);
        maxSpawnCenterX          = (0 + horizontolWaypointLength / 2.0f) - (horizontalSpawnLength / 2.0f);

        minSpawnCenterY          = (0 - verticalWaypointLength / 2.0f) + (verticalSpawnLength / 2.0f);
        maxSpawnCenterY          = (0 + verticalWaypointLength / 2.0f) - (verticalSpawnLength / 2.0f);
    }

    protected override void EvaluateEnemiesSpawn ()
    {
        enemiesToSpawn           = 0;
        var percent             = (float)(((currentWave / EnemiesToSpawnFactor) + 1 ) * EnemiesToSpawnIncrement);
        enemiesToSpawnPercentage = Mathf.Clamp(percent, 0.1f, 0.9f);
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
        var level  = currentWave % EnemiesLevelFactor;
        level      = level == 0 ? EnemiesLevelFactor : level;
        enemiesLevel = Mathf.Clamp(level, minEnemiesLevel, maxEnemiesLevel);
    }

    private int GetTriangleNumber(float length)
    {
        return Mathf.RoundToInt((length  ) / (gap + triangleWidth));
    }

    protected override void SpawnEnemy (SpawnData spawnData)
    {
        EnemyGenerator.GenerateTriangle(spawnData.enemyData, spawnData.position, Vector3.zero, spawnData.rotation);
    }

    protected override IEnumerator SpawnEnemies(List<SpawnData> dataList)
    {
        foreach (var spawnData in dataList)
        {
            EnemyGenerator.GenerateTriangle(spawnData.enemyData, spawnData.position, Vector3.zero, spawnData.rotation);
            yield return new WaitForSeconds(offsetTimer);
        }
        yield return null;
    }

    private bool IsSpawningVertical(Bounds bound)
    {
        return bound.center.y == 0;
    }

    protected override List<Vector3> GetSpawningEnemyPosition(Bounds bound, int num)
    {
        return IsSpawningVertical(bound) ? SpawnVerticalEnemies(bound) : SpawnHorizontalEnemies(bound);
    }

    private List<Vector3> SpawnVerticalEnemies(Bounds boundary)
    {
        var retVal          = new List<Vector3>();
        var decrementVector = new Vector3(0, -1, 0);
        var minValueOffset  =  boundary.min.x;
        var maxValueOffset  =  boundary.max.x;
        var playerPosition  = GetPlayerPositionForSpawn();

        var centerToSpawn = new Vector3
            (
                   boundary.center.x ,
                   Mathf.Clamp(playerPosition.y, minSpawnCenterY, maxSpawnCenterY),
                   0
            );

        var startingPoint = centerToSpawn + ((decrementVector * -1) * (verticalSpawnLength / 2.0f));

        for (int i = 0; i <= verticalSpawningNumber; i++)
        {
            var iPosition = new Vector3
                (
                    startingPoint.x,
                    startingPoint.y,
                    0
                );
            startingPoint += decrementVector * (gap + triangleWidth);
            retVal.Add(iPosition);
        }

        return retVal;
    }

    private List<Vector3> SpawnHorizontalEnemies(Bounds boundary)
    {
        var retVal         = new List<Vector3>();
        var decrementVector = new Vector3(-1, 0, 0);
        var minValueOffset = boundary.min.y;
        var maxValueOffset = boundary.max.y;
        var playerPosition = GetPlayerPositionForSpawn();

        var centerToSpawn = new Vector3
            (
                   Mathf.Clamp(playerPosition.x, minSpawnCenterX, maxSpawnCenterX),
                   boundary.center.y,
                   0
            );

        var startingPoint = centerToSpawn + ((decrementVector * -1) * (horizontalSpawnLength / 2.0f));

        for (int i = 0; i <= horizontalSpawningNumber; i++)
        {
            var iPosition = new Vector3
                (
                    startingPoint.x,
                    startingPoint.y,
                    0
                );

            startingPoint += decrementVector * (gap + triangleWidth);
            retVal.Add(iPosition);
        }

        return retVal;
    }

    private Vector3 GetPlayerPositionForSpawn()
    {
        return player.transform.position;
    }

    protected override bool isStillSpawning()
    {
        return true;
    }
}
