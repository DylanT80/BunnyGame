using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AfterWave : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;
    private SpawnManager SM;
    private bool Spawned = false;
    public Button StartWaveButton;
    public GameObject StartWaveButtonUI;
    public GameObject EnemyCounterUI;
    private int WaveNumber = 0;
    private GameObject Spawner;
    private SpawnManager[] SMs;
    // How many enemies to defeat
    [System.NonSerialized] public int EnemyCount;
    public int EnemiesToKill;
    public int EnemiesKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        SM = GetComponent<SpawnManager>();
        StartWaveButton.onClick.AddListener(StartWave);
        StartWaveButtonUI.SetActive(true);    // The start wave button
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
        SMs = Spawner.GetComponents<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn only if...
        if (!Spawned && EnemiesKilled == EnemiesToKill) {
            Spawned = true;
            ObjectsToSpawn[0].SetActive(true);
            ObjectsToSpawn[1].SetActive(true);  
            StartWaveButtonUI.SetActive(true); 
            EnemyCounterUI.SetActive(false);
        }
        
        if (EnemiesKilled != EnemiesToKill) {
            Spawned = false;
        }
        
        if (EnemyCounterUI.GetComponent<EnemyCounter>())
            EnemyCounterUI.GetComponent<EnemyCounter>().counter = EnemyCount;
    }

    private void StartWave() {
        ObjectsToSpawn[0].SetActive(false); 
        ObjectsToSpawn[1].SetActive(false);
        StartWaveButtonUI.SetActive(false);
        EnemyCounterUI.SetActive(true);
        EnemiesKilled = 0;
        EnemyCount = 0;
        EnemyCounterUI.GetComponent<EnemyCounter>().counter = EnemyCount;
        WaveNumber++;
        // No upgraded enemies in first wave
        SpawnManager[] SMs = Spawner.GetComponents<SpawnManager>();

        // Count to only target enemy 1 and 2, prefabs save the progress !!
        int count = 0;
        foreach (SpawnManager SM in SMs)
        {
            SM.EnemyCount = 0;
            if (count > 1) {
                continue;
            }
            SM.UpdateEnemies(WaveNumber > 1 && WaveNumber % 3 == 0);  // Every third wave, upgrade the strength of enemies
            count++;
        }
        
    }
}
