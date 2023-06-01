using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public GameObject[] StatBars;
    public Button BuyButton;
    private int current = 0;    // stat counter
    public int UpgradeType;
    private GameObject player;
    private PlayerInfo PI;
    private ShopSystem ShopSystem;
    private WeaponSystem WeaponSystem;
    public int CostToUpgrade;
    public bool Misc;
    private void Start() {
        BuyButton.onClick.AddListener(Upgrade);
        player = GameObject.FindGameObjectWithTag("Player");
        PI = player.GetComponent<PlayerInfo>();
        ShopSystem = GameObject.Find("ShopSystem").GetComponent<ShopSystem>();
        WeaponSystem = GameObject.Find("InventorySystem").GetComponent<WeaponSystem>();
    }
    private void Upgrade() {
        int CurrentBalance = ShopSystem.balance;

        // Not enough money
        if (CurrentBalance < CostToUpgrade) {
            return;
        }
        // Max upgrades
        if (!Misc && current == StatBars.Length) {
            return;
        }

        ShopSystem.balance -= CostToUpgrade;

        // Stat bar update, if not misc
        if (!Misc) {
            StatBars[current].GetComponent<Image>().color = new Color32(85, 238, 62, 255);
            current++;
        }
        StatUpgrade();
    }

    // Upgrade player info stats
    private void StatUpgrade() {
        switch (UpgradeType) {
            // Max hearts
            case 0:
                PI.SetMaxHearts(1);
                break;
            // Strength
            case 1:
                PI.SetStrength(1);
                break;    
            // Speed
            case 2:
                PI.SetSpeed(0.5f);
                break;
            // Restore health
            case 3:
                PI.health = PI.maxHealth;
                break;            
            // Restore ammo
            case 4:
                // Set all weapons to active
                WeaponSystem.shopping = true;
                WeaponSystem.Update();
                // Restore ammo in each weapon
                WeaponInfo[] weapons = player.GetComponentsInChildren<WeaponInfo>();
                foreach (WeaponInfo weapon in weapons)
                {
                    weapon.AmmoCount = weapon.AmmoCountSize;
                    weapon.InReserve = weapon.InReserveSize;
                    weapon.UpdateUI();
                }
                // Set all weapons to false
                WeaponSystem.shopping = false;
                WeaponSystem.Update();
                break;    
        }
    }
}
