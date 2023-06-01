using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;                             //Speed override to customize bullet speed for each enemy type
    public int burstCount;                          //Ammount to fire in each wave of bullets
    public float timeBetweenBursts;                 //Fire rate
    public float restTime = 1f;                     //Time after each wave of bullets
    private GameObject target;
    [Range(0, 359)] public float angleSpread;       //Spread/Cone of influence
    public float startingDistance = 5f;             //Projectile spawn distance from object (so it doesn't spawn ontop of enemy)
    public int projectilesPerBurst;                 //How many bullets are in one burst
    public float staggerTime = 0f;
    
    private Animator boss;
    private bool isShooting = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (staggerTime != 0) {
            StartCoroutine(StaggerRoutine());
        }
        boss = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }
    public void Attack() {
        if (!isShooting) {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator StaggerRoutine () {
        
        isShooting = true;
        yield return new WaitForSeconds(staggerTime);
        isShooting = false;
    }

    private IEnumerator ShootRoutine() {
        boss.SetTrigger("attack");
        isShooting = true;

        float startAngle, currentAngle, angleStep;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        for (int i=0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.up = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out EnemyProjectile projectile)) {
                    projectile.UpdateMoveSpeed(speed);
                }

                currentAngle += angleStep;
            }
        
            currentAngle = startAngle;

            yield return new WaitForSeconds(timeBetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
                Vector2 targetDirection = target.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        float endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;

        if (angleSpread != 0) {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x, y);
        return pos;

    }
}
