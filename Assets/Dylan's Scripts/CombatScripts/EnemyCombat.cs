using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyCombat : MonoBehaviour
{
    public float health = 100f;
    public float damage = 1f;
    private GameObject player;
    public GameObject FloatingTextPrefab;
    bool canHit;   // Enemy can hit on a cooldown
    public int cooldown = 10;
    private int currCooldown;
    private GameObject scoreboard;
    public int score = 10;
    public float collisionDamage = 0f;
    private AfterWave AW;
    public GameObject[] drops;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canHit = true;
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard");
        AW = GameObject.FindGameObjectWithTag("Spawner").GetComponent<AfterWave>();
    }

    // Update is called once per frame
    void Update()
    {
        // Upon death
        if (health <= 0) {
            // Someimtes drop item
            int drop = Random.Range(0, 3);
            if (drop <= 1) { // 2/3 chance currently
                // Drop a random item from the list
                int iRandom = Random.Range(0, drops.Length);
                Instantiate(drops[iRandom], transform.position, Quaternion.identity);
            }

            // Add to score
            scoreboard.GetComponentInChildren<ScoreCounter>().RunCo(score);
            AW.EnemyCount--;
            AW.EnemiesKilled++;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg) {
        // Player stats contribute
        float calculatedDmg = dmg + player.GetComponent<PlayerInfo>().strength;
        
        health -= calculatedDmg;
        // Healthbar visuals
        transform.GetChild(0).GetComponent<EnemyHealth>().TakeDamage(calculatedDmg);
        // Floating text visuals
        if (FloatingTextPrefab) {
            ShowFloatingText(calculatedDmg);
        }
    }
    
    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && canHit) {
            Debug.Log("Enemy hit player!");
            canHit = false;
            currCooldown = cooldown;

            player.GetComponent<PlayerInfo>().SetHealth(-damage - collisionDamage);
            // Destroy upon collision option, turn off if wanted
            Destroy(gameObject);
        }
        // Can hit again after some time
        else if (other.gameObject.CompareTag("Player")) {
            currCooldown--;
            if (currCooldown <= 0) {
                canHit = true;
                currCooldown = cooldown;
            }
        }
    }

    void ShowFloatingText(float dmg) {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text = dmg.ToString();
    }
}
