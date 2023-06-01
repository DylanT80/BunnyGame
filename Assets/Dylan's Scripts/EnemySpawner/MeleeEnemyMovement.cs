using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    private GameObject MyTarget = null;
    public float MySpeed = 2f;
    public float turnRate = 0.5f;
    public float attackTurnRate = 0.1f;
    public float distanceToAttack = 10f;
    public float attackSpeed = 10f;
    public float attackStagger = 0.5f;
    private float nextAttack = 0f;
    private bool attacking = false;
    private Vector3 attackDirection;
    private Animator zomb;

    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        MyTarget = GameObject.FindGameObjectWithTag("Player");
        zomb = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MyTarget) return;
        var step = MySpeed * Time.deltaTime;
        var attackStep = attackSpeed * Time.deltaTime;
        if (!attacking)
        {
            if (Vector3.Distance(MyTarget.transform.position, transform.position) >= distanceToAttack)
            {
                zomb.speed = 1f;
                transform.position = Vector3.MoveTowards(transform.position, MyTarget.transform.position, step);
            
            }
            if (Vector3.Distance(MyTarget.transform.position, transform.position) <= distanceToAttack)
            {
                attacking = true;
                nextAttack = Time.time + attackStagger;
                attackDirection = MyTarget.transform.position;
            }
        }
        else if (Time.time > nextAttack && attacking == true) {
            zomb.speed = 5f;
                if (Vector3.Distance(MyTarget.transform.position, transform.position) >= distanceToAttack) 
                {
                    attacking = false;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, attackDirection, attackStep);
                }
            
        }

        //face target
        if (transform.position.x > MyTarget.transform.position.x && facingRight)
        {
            flip();
        }
        else if (transform.position.x < MyTarget.transform.position.x && !facingRight)
        {
            flip();
        }
    }
     void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    
}
