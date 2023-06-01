using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Different from enemy combat, this is just for the visual healthbar
public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    private float healthAmount;
    private float healthMax;

    
    // Start is called before the first frame update
    void Start()
    {
        healthMax = transform.parent.gameObject.GetComponent<EnemyCombat>().health;
    }

    public void TakeDamage(float damage) {
        healthBar.fillAmount = transform.parent.gameObject.GetComponent<EnemyCombat>().health / healthMax;
    }

    public void Heal(float healingAmount) {
        // healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / healthMax;
    }
}
