using UnityEngine;
using System.Collections;

public enum WaveType : uint
{
   
    KILL    = 0,
    DODGE   = 1,
    INVALID = uint.MaxValue,
}
public class GameManager : MonoBehaviour {

    protected EnemiesSpawnManager enemiesSpawnManager;
    // Use this for initialization
    void Start ()
    {
        enemiesSpawnManager = GetComponentInChildren<EnemiesSpawnManager>();
    }
    
    // Update is called once per frame
    void Update ()
    {
    
    }
}
