using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    //audio
    public AudioSource src;
    public AudioClip clip;
    //Movement Variables
    public static float moveSpeed = 10;
    public Rigidbody2D rb2d;
    private Vector2 movement;
    public static bool canDash = false;
    public static bool onWater = false;

    //Dash Variables
    public float dashSpeed = 20;
    public static float activeMoveSpeed;
    public float dashLength = .5f, dashCooldown = 1f;

    public float dashCounter;
    public float dashCoolCounter;
    
    //animator
    public Animator dashAnimation;
    public Animator animator;
    public static bool holdWeapon;

    //camera variables
    private Camera mainCam;
    private PlayerInfo playerInfo;
    bool facingRight = true;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        activeMoveSpeed = moveSpeed;
        holdWeapon = false;
    }
    void Update()
    {
        FaceTarget();
        Movement();
        Dash();
        HoldWeapon();
    }
    void Movement()
    {
        
        //WSAD and Arrow movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb2d.velocity = movement * activeMoveSpeed;
        
        Dash();
        
    }
    void Dash()
    {

    
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                src.PlayOneShot(clip);
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                Instantiate(dashAnimation, transform.position, Quaternion.identity);
                BunnyCamScript.dashShake = true;
            }
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
       
    }
    void FaceTarget()
    {
        //Interacts with the animator, Moves his little feet when movement is detected.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Dash Restriction
        if(movement.sqrMagnitude <= 0 || movement.sqrMagnitude <= 0 && onWater == true) 
        { 
            canDash = false; 
            BunnyCamScript.dashShake = false;
        }
        else if(movement.sqrMagnitude > 0.1 && onWater == false) 
        { 
            canDash = true; 
            BunnyCamScript.dashShake = true;
        }
        


        //Face where the mouse target is (Horizontal based)    
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

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
    void HoldWeapon()
    {
            animator.SetBool("HoldWeapon", holdWeapon);
            return;
    }
        
}

    
       


    

