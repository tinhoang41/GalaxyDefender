using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

    public GameObject enemiesSpawn;
    public GameObject itemsSpawn;

    public int deBugWaveNumber;
    public List<WaveData> waveInfo;

    protected int currentWave;
    protected EnemiesSpawnManager enemiesSpawnManager;
    protected WaveType currentWaveType;
    protected HUDManager hudManager;
    public WaveType pCurrentWaveType
    {
        get { return currentWaveType; }
    }
    // Use this for initialization
    void Start () {

        currentWave         = deBugWaveNumber;
        currentWaveType     = WaveType.INVALID;
        hudManager          = GlobalControl.Instance.pHUD.GetComponent<HUDManager>();

        enemiesSpawnManager = enemiesSpawn.GetComponent<EnemiesSpawnManager>();
        enemiesSpawnManager.Initialize (waveInfo);
        enemiesSpawnManager.InitWaveNumber (currentWave);
    }
    

    public IEnumerator HandleRunningWave()
    {
        // if spawners are done with spawning
        if (isCurrentWaveFinished())
        {
            hudManager.ResetHint();
            yield return new WaitForSeconds(1.0f);
            EndWave();
            StartWave();
        }
        UpdateHUD();
        yield return null;
    }

    protected bool isCurrentWaveFinished()
    {
        return enemiesSpawnManager.isFinished() && GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    protected WaveType GetWaveType()
    {
        return currentWave % 2 == 0 ? WaveType.DODGE : WaveType.KILL;
    }

    protected void StartWave()
    {
        currentWave++;
        currentWaveType = GetWaveType();
        Debug.Log("currentWave: " + currentWave + " wave type: " + currentWaveType);
        enemiesSpawnManager.StartNewWave(currentWaveType);
    }

    public void EndWave()
    {
        enemiesSpawnManager.Cleanup();
        enemiesSpawnManager.EndWave();
    }

    protected void UpdateHUD()
    {
        hudManager.UpdateHintText(enemiesSpawnManager.GetCurrentWaveHint(currentWaveType));
        hudManager.UpdateCurrentWave(currentWave);
    }
   
}
