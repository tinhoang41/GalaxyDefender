using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {

    public EnemyDataParameter data;

    public EnemyData(int min, int max, int current)
    {
        data.minLevel = min;
        data.maxLevel = max;
        data.currentLevel = current;
    }
    // Use this for initialization
    void Start () {

    }
    
    public virtual void Initialize(int min, int max, int current)
    {
        data.minLevel     = min;
        data.maxLevel     = max;
        data.currentLevel = current;
    }

    public virtual void Initialize(EnemyDataParameter d)
    {
        data.minLevel     = d.minLevel;
        data.maxLevel     = d.maxLevel;
        data.currentLevel = d.currentLevel;
    }

    public EnemyDataParameter GetData()
    {
        return data;
    }
}
