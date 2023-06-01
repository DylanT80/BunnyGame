using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public DeathScreenScript DeathScreen;
    public static int recordedScore;
    public float health = 3f;
    [System.NonSerialized] public float maxHealth = 3f;
    public float speed = 5f;
    public float strength = 0f;

    //HP indicator
    public GameObject lowHP;
    public GameObject onFire;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Upon death
        if (health <= 0) {
            Destroy(gameObject);
            DeathScreen.Set(recordedScore);
            Debug.Log("You're dead!");
        }
        //LowHP
        if(health <= 1){ lowHP.SetActive(true); }
        else { lowHP.SetActive(false); }
           
        if(LavaEffect.playerOnLava && health > 1) {onFire.SetActive(true);}
        else {onFire.SetActive(false); }
          
    }

    public void SetSpeed(float speedAdjustment) {
        speed += speedAdjustment;
    }

    public void SetStrength(float strengthAdjustment) {
        strength += strengthAdjustment;
    }

    public void SetHealth(float healthAdjustment) {
        // Avoid overflowing health
        if (health + healthAdjustment > maxHealth) {
            return;
        }
        // Overkill just brings health to 0
        if (health + healthAdjustment < 0) {
            health = 0;
            return;
        }
        
        health += healthAdjustment;
    }

    public void SetMaxHearts(float num) {
        maxHealth += num;
    }
}
