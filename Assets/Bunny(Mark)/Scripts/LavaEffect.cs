using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaEffect : MonoBehaviour
{
    
    public float damagePerSecond = 3.0f;
    public static bool playerOnLava = false;
    private GameObject player;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(playerOnLava)
        {
            damagePerSecond -= Time.deltaTime;
            if(damagePerSecond <= 0f)
            {
                StartCoroutine(DPS());
                damagePerSecond = 3.0f;
            }
        } else { damagePerSecond = 3.0f; }

    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //slow effect
            BunnyMovement.activeMoveSpeed = 5;
            BunnyMovement.moveSpeed = 5;
            BunnyMovement.onWater = true;
            BunnyMovement.canDash = false;

            //damagepersecond
            StartCoroutine(DPS());
            playerOnLava = true;
        }

    }
    IEnumerator DPS()
    {
        player.GetComponent<PlayerInfo>().SetHealth(-1f);
        yield return null;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //slow effect
            BunnyMovement.activeMoveSpeed = 10;
            BunnyMovement.moveSpeed = 10;
            BunnyMovement.onWater = false;
            BunnyMovement.canDash = true;

            //damagepersecond
            playerOnLava = false;
        }
    }
}
