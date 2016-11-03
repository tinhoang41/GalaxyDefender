using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {

    private EnemyDataParameter data;
    private int maxLife;
    private int currentLife;

    public EnemyDataParameter pData
    {
        get { return data; }
    }

    public int pMaxlife
    {
        get { return maxLife; }
    }

    public int pCurrentLife
    {
        get { return currentLife; }
    }

    void Start()
    {
        maxLife     = data.maxLevel;
        currentLife = data.currentLevel;
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

    public void AddDamage(int damageDealt)
    {
        currentLife -= damageDealt;
        Mathf.Max(currentLife, 0);
    }
}
