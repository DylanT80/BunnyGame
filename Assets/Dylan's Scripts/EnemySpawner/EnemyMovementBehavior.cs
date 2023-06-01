using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehavior : MonoBehaviour
{
    private GameObject MyTarget = null;
    public float MySpeed = 20f;
    public float turnRate = 0.1f;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public float rotateSpeedRange = 0f;
    public bool rotateActive;
    public bool randomActive = false;
    private float rotateSpeed;
    private Vector3 currentDirection;

    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        MyTarget = GameObject.FindGameObjectWithTag("Player");
        rotateSpeed = Random.Range(-rotateSpeedRange, rotateSpeedRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (!MyTarget) return;
        var step = MySpeed * Time.deltaTime;

        if (Vector3.Distance(MyTarget.transform.position, transform.position) >= distanceToStop)
        {
            transform.position = Vector3.MoveTowards(transform.position, MyTarget.transform.position, step);
            
        }
        if (Vector3.Distance(MyTarget.transform.position, transform.position) <= distanceToStop)
        {
            transform.position = Vector3.MoveTowards(transform.position, MyTarget.transform.position, -step);
        }

        
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
