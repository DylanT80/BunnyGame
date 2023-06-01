using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 20;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    public float offset;
    private GameObject player;
    private EdgeCollider2D edgeCollider;
    private float damage;
    Transform m_transform;
    bool facingRight = true;
    private WeaponInfo weaponInfo;

    public bool canFire = false;
    private float timer;
    public float FireCooldown = 0.5f;

    //laser animation
    private Animator laser;


    private void Awake() {
        m_transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        edgeCollider = GetComponent<EdgeCollider2D>();
        weaponInfo = GetComponent<WeaponInfo>();
        laser = GetComponent<Animator>();
    }

    private void Update() {
        GetComponent<BoxCollider2D>().enabled = false;
        if (!weaponInfo.selected && weaponInfo.dropped) {
            setActive(false);
            weaponInfo.audio.Stop();
            return;
        }
        //Movement flip
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if (mousePos.x < player.transform.position.x && facingRight)
        // {
        //     flip();
        // }
        // else if (mousePos.x > player.transform.position.x && !facingRight)
        // {
        //     flip();
        // }
        
    
        // Rotating the weapon
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // direction = destination (cursor) - origin (weapon)
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        
        SetEdgeCollider(m_lineRenderer);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > FireCooldown)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Laser!
        if (Input.GetMouseButton(0) && canFire && weaponInfo.selected) {
            Debug.Log(weaponInfo.AmmoCount);
            if (weaponInfo.Shoot()) {
                laser.SetTrigger("Fire");
                Debug.Log("laer shoooting");
                // Enable both the laser line and edge collisions
                setActive(true);
                Draw2DRay(laserFirePoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            else {
                setActive(false);
            }
        }
        // Not firing
        if (Input.GetMouseButtonUp(0)) {
            // Disable visuals, collision, and sound
            setActive(false);
            weaponInfo.audio.Stop();
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R)) {
            weaponInfo.Reload();
        }
    }

    // Laser visuals
    void Draw2DRay(Vector2 startPos, Vector2 endPos) {
        float dist = Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) + Mathf.Pow(endPos.y - startPos.y, 2));
        // Short laser is strong
        if (dist <= defDistanceRay) {
            GetComponent<LineRenderer>().startColor = Color.cyan;
            GetComponent<LineRenderer>().endColor = Color.green;
            
            GetComponent<LineRenderer>().startWidth = 0.25f;
            GetComponent<LineRenderer>().endWidth = 1.0f;

            damage = weaponInfo.damage * 2;
        } else {    // Longer range laser is weak
            GetComponent<LineRenderer>().startColor = Color.yellow;
            GetComponent<LineRenderer>().endColor = Color.red;

            GetComponent<LineRenderer>().startWidth = 0.125f;
            GetComponent<LineRenderer>().endWidth = 0.375f;

            damage = weaponInfo.damage;
        }
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    // Setting the points for the edge collider
    // since using world space, convert coordinates to local space for points
    void SetEdgeCollider(LineRenderer lineRenderer) {
        List<Vector2> edges = new List<Vector2>();
        for (int point = 0; point < lineRenderer.positionCount; point++) {
            edges.Add(transform.InverseTransformPoint(lineRenderer.GetPosition(point)));
        }
        edgeCollider.SetPoints(edges);
    }
    
    // Deal damage overtime
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            // If not selected
            // if (!GetComponent<PickUp>().selected) {  
            //     return;
            // }
            Debug.Log("laser hti enemy");
            other.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage * Time.smoothDeltaTime); // smoothDeltaTime to account for fps
        }
    }

    // void flip()
    // {
    //     facingRight = !facingRight;
    //     transform.Rotate(180f, 0f, 0f);
    // }

    private void setActive(bool active) {
        GetComponent<LineRenderer>().enabled = active;
        GetComponent<EdgeCollider2D>().enabled = active;
    }
}