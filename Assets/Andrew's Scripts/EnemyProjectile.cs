using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float distance;
    public LayerMask whatIsSolid;
    private Vector3 shootDir;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", 10);
    }

    // Update is called once per frame
    void Update()
    {        
        // Keep moving projectile forward direction
        transform.position += transform.up * (speed * Time.smoothDeltaTime);   
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    public void UpdateMoveSpeed(float speed) {
        this.speed = speed;
    }

    void DestroyProjectile() {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("enemy projectile player hit!");
            other.gameObject.GetComponent<PlayerInfo>().SetHealth(-damage);
        }
        if (!other.CompareTag("LevelHazard") && !other.CompareTag("EnemyProjectile")) {
        DestroyProjectile();
        }
    }
}
