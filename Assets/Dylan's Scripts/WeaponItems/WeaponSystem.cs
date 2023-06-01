using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// System that controls which items are active in the player and also houses all the avaliable items using a weapon number as index
public class WeaponSystem : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;
    public GameObject[] weapons;
    public GameObject[] WeaponsHeld;
    private int slotSelection = 0;
    public bool shopping = false;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        WeaponsHeld = new GameObject[4];
    }
    public void Update() {
        slotSelection = inventory.slot;

        // If shopping, make all weapons avaliable to target
        if (shopping) {
            for (int i = 0; i < WeaponsHeld.Length; i++) {
                if (WeaponsHeld[i]) {
                    WeaponsHeld[i].SetActive(true);
                }
            }
        }
        else {
            for (int i = 0; i < WeaponsHeld.Length; i++) {
                // Selected and has weapon
                if (slotSelection == i && WeaponsHeld[i]) {
                    WeaponsHeld[i].SetActive(true);
                    BunnyMovement.holdWeapon = true;
                }
                // Selected and has no weapon
                else if (slotSelection == i) {
                    BunnyMovement.holdWeapon = false;
                }
                // Not selected and has weapon
                else if (WeaponsHeld[i]) {
                    if (WeaponsHeld[i].activeSelf && WeaponsHeld[i].GetComponent<WeaponInfo>())
                        WeaponsHeld[i].GetComponent<WeaponInfo>().reloading = false;
                    WeaponsHeld[i].SetActive(false);
                }  
            }
        }
    }
}
