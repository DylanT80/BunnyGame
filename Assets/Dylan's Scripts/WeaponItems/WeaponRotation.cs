using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Renderer for sorting layers
    public SpriteRenderer WeaponRenderer;
    public SpriteRenderer CharacterRenderer;
    public Animator animator;
    public float delay = 0.3f;
    private bool AttackBlocked;
    private Camera Camera;  // For cursor position
    public Transform CircleOrigin;
    public float radius;
    public float damage;
    public void ResetAttacking() {
        IsAttacking = false;
    }
    private void Start() {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update() {
        // Dont move if attacking
        if (IsAttacking) {
            return;
        }
        // Rotating
        Vector2 direction = (Camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = direction;
    
        // Flipping
        Vector2 scale = transform.localScale;
        if (direction.x < 0) {
            scale.y = -1;
        }
        else if (direction.x > 0) {
            scale.y = 1;
        }
        transform.localScale = scale;

        // Sort order back when rotating at top
        if (transform.eulerAngles.z > 30 && transform.eulerAngles.z < 150) {
            WeaponRenderer.sortingOrder = CharacterRenderer.sortingOrder - 1;
        }
        else {
            WeaponRenderer.sortingOrder = CharacterRenderer.sortingOrder + 1;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Attack();
        }
    }
    
    
    public bool IsAttacking { get; private set; }
    public void Attack() {
        if (AttackBlocked) {
            return;
        }
        animator.SetTrigger("Attack");
        AttackBlocked = true;
        IsAttacking = true;
        StartCoroutine(DelayAttacked());
    }

    private IEnumerator DelayAttacked() {
        yield return new WaitForSeconds(delay);
        AttackBlocked = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 pos = CircleOrigin == null ? Vector3.zero : CircleOrigin.position;
        Gizmos.DrawWireSphere(pos, radius);
    }

    public void DetectColliders() {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(CircleOrigin.position, radius)) {
            if (collider.gameObject.CompareTag("Enemy")) {
                collider.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
            }
        }
    }
}
