using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnWayPoint : MonoBehaviour {

    public List<Vector3> wayPoints;
    public Vector3       boundSize;
    public float         maxSpawnRateInSeconds;
    public float         minSpawnRateInSeconds;
    public int           maxNumberToSpawn;
    public int           minNumberToSpawn;
    public int           minSpawnPerTime;
    public int           maxSpawnPertime;

    protected int       waveNumber;
    protected float     spawnRate;
    protected int       enemiesToSpawn;
    private List<Bounds> wayPointsBounds;

    // Use this for initialization
    void Start () {
        wayPointsBounds = new List<Bounds>();
        foreach (var center in wayPoints)
            wayPointsBounds.Add(new Bounds(center, boundSize));

    }
    
    protected virtual void SetUpSpawningVariables()
    {
        spawnRate      = maxSpawnRateInSeconds;
        enemiesToSpawn = minNumberToSpawn;
    }

    IEnumerator SpawnEnemies()
    {
        var numEnemies = 0;
        while(numEnemies < enemiesToSpawn)
        {

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
