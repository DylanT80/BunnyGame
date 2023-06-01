using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //camera variables
    private Camera mainCam;
    private Vector3 mousePos;
    
    //Bullet variables
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float FireCooldown;

    public bool HasAmmo;
    private WeaponInfo weaponInfo;
    private BunnyCamScript BCS;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        weaponInfo = GetComponent<WeaponInfo>();
        HasAmmo = true;
        BCS = mainCam.GetComponent<BunnyCamScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<WeaponInfo>().selected) {
            BCS.CanShake = false;
            return;
        }
        
        // Aim Rotation using Camera
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        //Bullets
        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > FireCooldown)
            {
                canFire = true;
                timer = 0;
            }
        }
        // Only fire bullet if cooldown is gone
        if(Input.GetMouseButton(0) && canFire)
        {
            canFire = false;

            // Reduce number of bullets
            HasAmmo = weaponInfo.Shoot();
            // Only create bullet object if there is ammo
            if (HasAmmo) {
                GameObject go = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                go.GetComponent<BulletScript>().damage = GetComponent<WeaponInfo>().damage;
            }

            // Can shake screen since has ammo, can't if no ammo
            BCS.CanShake = HasAmmo;
        }
        
        // Reload
        if (Input.GetKey(KeyCode.R)) {
            // True if there is ammo, false if there is no ammo
            weaponInfo.Reload();
        }
    }
}
