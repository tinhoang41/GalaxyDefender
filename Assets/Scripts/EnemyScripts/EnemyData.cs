using UnityEngine;
using System.Collections;

public class EnemyData : ActorData {

    private EnemyDataParameter data;
    private int maxLife;
	public float immortalTimeWhenSpawn;

    public EnemyDataParameter pData
    {
        get { return data; }
    }

    public int pMaxlife
    {
        get { return maxLife; }
    }

    public int pCurrentLives
    {
		get { return currentLives; }
    }

	public bool pIsImmortal
	{
		get { return isImmortal; }
	}

	protected override void Initialize ()
	{
		base.Initialize ();
        maxLife     = data.maxLevel;
		currentLives = data.currentLevel;
		isImmortal = true;
		immortalTime = immortalTimeWhenSpawn;
		StartCoroutine ("RunImmortality", ImmortalType.SPAWNING);
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
