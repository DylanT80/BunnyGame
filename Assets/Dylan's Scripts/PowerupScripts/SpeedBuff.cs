using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    [SerializeField] private float speedIncreaseAmt = 1;

    private GameObject effect;
    public GameObject speedEffect;

    private void Update() {
        // Effect follows player if activated
        if (effect) {
            effect.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerInfo pi = other.gameObject.GetComponent<PlayerInfo>();
            StartCoroutine(PowerupSequence(pi));
        }
    }

    private IEnumerator PowerupSequence(PlayerInfo pi) {
        // Soft destroy
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        
        // Activate
        pi.SetSpeed(speedIncreaseAmt);
        effect = Instantiate(speedEffect, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        // Delay
        yield return new WaitForSeconds(3f);

        // Deactivate
        // pi.SetSpeed(-speedIncreaseAmt);
        Destroy(effect);

        yield return null;
        
        Destroy(gameObject);
    }
}
