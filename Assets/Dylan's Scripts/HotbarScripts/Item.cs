using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Item contains information such as
//  - Hotbar icon
//  - The weapon the player uses through weapon number
//  - If already existing, stores that game object that was dropped
public class Item : MonoBehaviour
{
    public GameObject Icon;
    public int WeaponNumber;    // To get new weapon
    public GameObject ExistingWeapon;   // Stores existing weapon
    private Inventory inventory;
    private GameObject player;
    private WeaponSystem WS;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        WS = GameObject.Find("InventorySystem").GetComponent<WeaponSystem>();
        // Start disappearing
        Disappear();
    }
    // Destroy when disappeared
    public void Disappear() {
        StartCoroutine(Fade());
    }
    // Gradually fade away, - 0.1 increments every 1 second = 10 seconds
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(8);
        Color c = GetComponent<SpriteRenderer>().material.color;
        for (float time = 1f; time >= 0; time -= 0.3f)
        {
            yield return new WaitForSeconds(1);
            GetComponent<SpriteRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(1);
            GetComponent<SpriteRenderer>().material.color = c; 
        }
        // Destory when alpha is 0
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other) {
        // Player picks up object that's not picked
        if (other.tag == "Player") {
            // Weapon pickup
            if (tag == "Weapon") {
                // If picked up same type of weapon, add ammo to it
                foreach (GameObject weapon in WS.WeaponsHeld)
                {
                    if (!weapon)
                        continue;
                    if (weapon.name == WS.weapons[WeaponNumber].name + "(Clone)") {
                        Debug.Log("picked up same weapon!");
                        weapon.GetComponent<WeaponInfo>().AmmoCount = weapon.GetComponent<WeaponInfo>().AmmoCountSize;
                        weapon.GetComponent<WeaponInfo>().InReserve = weapon.GetComponent<WeaponInfo>().InReserveSize;
                        weapon.GetComponent<WeaponInfo>().UpdateUI();
                        Destroy(gameObject);
                        return;
                    }
                }

                if (ExistingWeapon) {
                    inventory.AddItem(Icon, ExistingWeapon, WeaponNumber, true);
                }
                else
                    inventory.AddItem(Icon, WS.weapons[WeaponNumber], WeaponNumber, false);
                Destroy(gameObject);
            }              
            // Items not weapon destroy itself later if needed                                            
        }
    }
}
