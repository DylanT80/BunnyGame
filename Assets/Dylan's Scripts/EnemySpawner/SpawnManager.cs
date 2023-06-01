using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public float waveTime = 180.0F;
    private float nextWave = 0.0F;
    private float spawnCooldown;
    private float nextSpawn = 0.0F;
    public bool activeWave = false;
    private float waveCounter = 0f;
    public bool bossSpawner;

    public GameObject[] thingsToSpawn;
    public GameObject[] specialSpawn;
    private int objectNum;
    private int EnemiesToSpawnWave;
    public int EnemyCount;
    public float EnemiesUntilBoss = 10f;
    private float temp;
    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = Random.Range(6, 9f);
        temp = EnemiesToSpawnWave;
        EnemiesToSpawnWave = GetComponent<AfterWave>().EnemiesToKill / 4;
    }

    
    // Update is called once per frame
    void Update()
    {
        
        // Don't start wave if...
        ConditionsBeforeSpawn();

        if (activeWave)
        {
            if (Time.time > nextSpawn)
            {
                if (EnemiesUntilBoss != 0) {
                    nextSpawn = Time.time + spawnCooldown;
                    SpawnEnemy();
                    if (specialSpawn.Length != 0) {
                        EnemiesUntilBoss--;
                    }
                }
                else {
                    nextSpawn = Time.time + spawnCooldown;
                    SpawnSpecial();
                    EnemiesUntilBoss = temp;
                }
                GetComponent<AfterWave>().EnemyCount++;
                EnemyCount++;
            }
        }
    }

    public void SpawnEnemy()
    {
        Debug.Log("spawned enemy");
        InstructScript.active = false;
        objectNum = Random.Range(0, thingsToSpawn.Length);

        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 
                Random.Range(-size.y / 2, size.y / 2), 0);
        Instantiate(thingsToSpawn[objectNum], pos, Quaternion.identity);
    }

    public void SpawnSpecial()
    {
        Debug.Log("spawned boss");
        InstructScript.active = false;
        objectNum = Random.Range(0, specialSpawn.Length);

        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 
                Random.Range(-size.y / 2, size.y / 2), 0);
        Instantiate(specialSpawn[objectNum], pos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    private void ConditionsBeforeSpawn() {
        // Won't spawn if...
        if (GameObject.FindGameObjectWithTag("Shop") || EnemyCount >= EnemiesToSpawnWave) {    
            activeWave = false;
        }
        else {
            activeWave = true;
        }
    }

    public void UpdateEnemies(bool strength) {
        foreach (GameObject enemy in thingsToSpawn) {
            enemy.GetComponent<EnemyCombat>().health += 10;
            if (strength) {
                enemy.GetComponent<EnemyCombat>().damage += 1;
            }
        }
    }
}