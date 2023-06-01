using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float BulletSpeed;
    public float damage;
    public LayerMask whatIsSolid;
    private PlayerInfo playerInfo;
    private WeaponSystem weaponSystem;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * BulletSpeed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        Physics2D.IgnoreCollision(playerInfo.gameObject.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());  // Ignore player collision
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("BunnyFeet").GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());  // Ignore player feet collision
        weaponSystem = GameObject.Find("InventorySystem").GetComponent<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<Renderer>().isVisible) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);   // weapon dmg from weapon info
            DestroyProjectile();
        }
    }

    void DestroyProjectile() {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
