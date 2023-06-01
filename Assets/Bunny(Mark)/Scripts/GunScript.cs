using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Animator animator;
    bool facingRight = true;
    private WeaponInfo weaponInfo;

    private void Start() {
        
    }
    void Update()
    {
        GunAnimation();

        //Movement flip
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && facingRight)
        {
            flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            flip();
        }
    }

    void GunAnimation()
    {
        // Only fire when it has ammo
        weaponInfo = transform.parent.gameObject.GetComponent<WeaponInfo>();
        if(Input.GetMouseButton(0) && weaponInfo.AmmoCount > 0)
        {
            animator.SetTrigger("Fire");
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(180f, 0f, 0f);
    }
}
