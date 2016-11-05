using UnityEngine;
using System.Collections;

public enum WaveType : uint
{
   
    KILL    = 0,
    DODGE   = 1,
    INVALID = 2,
}
public class GameManager : MonoBehaviour {

    protected EnemiesSpawnManager enemiesSpawnManager;
	protected int waveNumber;
    // Use this for initialization
    void Start ()
    {
		waveNumber = 1;
        enemiesSpawnManager = GetComponentInChildren<EnemiesSpawnManager>();
		StartCoroutine ("Run");
    }
    

	IEnumerator Run()
	{
		yield return new WaitForSeconds (3.0f);
		Debug.Log ("waveNumber: " + waveNumber);
		enemiesSpawnManager.StartNewWave (getWaveType ());

		while(!isGameOver())
		{
			// if spawner is done with spawning
			if (enemiesSpawnManager.isFinished () && isEnemiesCleared()) 
			{
				yield return new WaitForSeconds (10.0f);
 				enemiesSpawnManager.Cleanup ();
				enemiesSpawnManager.EndWave ();
				waveNumber++;
				Debug.Log ("waveNumber: " + waveNumber);
				enemiesSpawnManager.StartNewWave (getWaveType ());
			}
			yield return null;
		}
	}

	bool isGameOver()
	{
		return false;
	}
	WaveType getWaveType()
	{
		return WaveType.KILL;
	}

    // Update is called once per frame
    void Update ()
    {
    
    }

	bool isEnemiesCleared()
	{
		return GameObject.FindGameObjectsWithTag ("Enemy").Length == 0;
	}
}
