﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    public GameObject enemiesSpawn;
    public GameObject itemsSpawn;
	public int deBugWaveNumber;
    public List<WaveData> waveInfo;

    protected int currentWave;
    protected EnemiesSpawnManager enemiesSpawnManager;

    // Use this for initialization
    void Start () {

		currentWave = deBugWaveNumber;
        enemiesSpawnManager = enemiesSpawn.GetComponent<EnemiesSpawnManager>();
		enemiesSpawnManager.Initialize (waveInfo);
		enemiesSpawnManager.InitWaveNumber (currentWave);
    }
    

    public IEnumerator HandleRunningWave()
    {
        // if spawner is done with spawning
        if (isCurrentWaveFinished())
        {
            yield return new WaitForSeconds(1.0f);
            EndWave();
            StartWave();
        }
        yield return null;
    }

    protected bool isCurrentWaveFinished()
    {
        return enemiesSpawnManager.isFinished() && GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    protected WaveType GetWaveType()
    {
		return WaveType.DODGE; //currentWave % 2 == 1 ? WaveType.DODGE : WaveType.KILL;
    }

    protected void StartWave()
    {
        currentWave++;
        Debug.Log("currentWave: " + currentWave);
        enemiesSpawnManager.StartNewWave(GetWaveType());
    }

    protected void EndWave()
    {
        enemiesSpawnManager.Cleanup();
        enemiesSpawnManager.EndWave();
    }


}
