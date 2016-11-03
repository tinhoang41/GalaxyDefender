using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesSpawnManager : MonoBehaviour {

    protected Dictionary<WaveType, List<SpawnWayPoint>> spawnerForWaveTable;
    // Use this for initialization
    void Start ()
    {
        SetUpTable();
    }
    
    // Update is called once per frame
    void Update () {
    
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
