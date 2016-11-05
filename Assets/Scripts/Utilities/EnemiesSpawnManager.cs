using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesSpawnManager : MonoBehaviour {

    protected Dictionary<WaveType, List<SpawnWayPoint>> spawnerForWaveTable;
    // Use this for initialization
	protected List<SpawnWayPoint> currenSpawners;	
    void Start ()
    {
        SetUpTable();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

	public void EndWave()
	{
		foreach (var spawner in currenSpawners)
			spawner.Stop();
	}

	public bool isFinished()
	{
		foreach (var spawner in currenSpawners)
			if (!spawner.pIsFinishedSpawning)
				return false;
		return true;
	}

	public void StartNewWave(WaveType waveType)
	{
		if (!spawnerForWaveTable.TryGetValue (waveType, out currenSpawners))
			return;
		foreach (var spawner in currenSpawners) 
		{
			spawner.UpdateWaveData ();
			spawner.Run ();
		}
	}

	public void Cleanup()
	{
		
	}

    protected void SetUpTable()
    {
        spawnerForWaveTable = new Dictionary<WaveType, List<SpawnWayPoint>>();
        for(WaveType i = WaveType.KILL; i < WaveType.INVALID; i++)
        {
            var spawnerList = GetSpawner(i);
            spawnerForWaveTable.Add(i, spawnerList);
        }
    }
		
    protected List<SpawnWayPoint> GetSpawner(WaveType type)
    {
        var retVal = new List<SpawnWayPoint>();
        switch (type)
        {
            case WaveType.KILL:
                {
                    retVal.Add(GetComponent<PolygonWaypoints>());
                    retVal.Add(GetComponent<DiamondSpawner>());

                    break;
                }
            case WaveType.DODGE:
                {
                    retVal.Add(GetComponent<TriangleSpawner>());
                    break;
                }
            default:
                break;
        }

        return retVal;
    }
}
