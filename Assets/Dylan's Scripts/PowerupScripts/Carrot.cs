using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public int value;  // Amount of currency you receive
    private ShopSystem ShopSystem;
    // Start is called before the first frame update
    void Start()
    {
        value = 1;
        ShopSystem = GameObject.Find("ShopSystem").GetComponent<ShopSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            ShopSystem.balance += value;
            Destroy(gameObject);
        }
    }
}
